using Sindicato_v1.Models;
using Sindicato_v1.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace Sindicato_v1.Controllers
{
    public class UsuarioController : Controller
    {
        public static string nom_Per;
        public static string apellido_Per;
        public static string t_Usuario;
        public static string t_Rol;
        public static int pendientes;

        SII_Entities db = new SII_Entities();

        public ActionResult _Login()
        {
            return View();
        }

        public ActionResult Usuarios()
        {
            ViewData["Nombre"] = AccesoController.nombre;
            ViewData["Apellido"] = AccesoController.apellido;
            return View();
        }

        [AllowAnonymous]
        public ActionResult Administrador()
        {
            ViewData["Nombre"] = AccesoController.nombre;
            ViewData["Apellido"] = AccesoController.apellido;
            ViewData["Conteo"] = AccesoController.pendientes;

            return View();
        }

        

        //[HttpPost]
        //public ActionResult Login(int user, string contraseña)
        //{
        //    try
        //    {
        //        using (SII_Entities db = new SII_Entities())
        //        {
        //            var usuario = (from u in db.Tbl_Usuario
        //                           join p in db.Tbl_Persona on u.id_Persona equals p.id_Persona
        //                           join tr in db.Tbl_Rol on u.id_Rol equals tr.id_Rol
        //                           join tu in db.Tbl_TipoUsuario on u.id_TipoUsu equals tu.id_TipoUsu
        //                           where u.contrasenia == contraseña.Trim() && p.cedula == user
        //                           select u).FirstOrDefault();


        //            var lista_pend = from p in db.Tbl_Persona
        //                             where p.estado == 2
        //                             select new { p.id_Persona };

        //            #region NombreUsuario, Pendientes

        //            pendientes = lista_pend.Count();

        //            //nom_Per = (from p in usuario
        //            //           select p.nombre).First();

        //            //apellido_Per = (from p in usuario
        //            //                select p.primer_Apellido).First();

        //            #endregion

        //            #region Tusuario, Trol

        //            //t_Usuario = (from tu in lista
        //            //             select tu.tipo_Usuario).First();

        //            //t_Rol = (from tr in lista
        //            //         select tr.tipo_Rol).First();

        //            #endregion

        //            if (usuario == null)
        //            {
        //                ViewBag.Error = "¡Usuario o contraseña invalidos!";
        //                return Login();
        //            }

        //            Session["User"] = usuario;

        //            #region roles
        //            //if (lista.Count() == 1)
        //            //{
        //            //    var idUsuario = (from u in lista
        //            //                     select u.id_Usuario).First();

        //            //    Session["User"] = idUsuario;

        //            //    if (t_Usuario == ("TERCER_NIVEL") && t_Rol == ("ADMINISTRADOR") || t_Usuario == ("TERCER_NIVEL") && t_Rol == ("GESTOR_DEDUCCIONES"))
        //            //    {
        //            //        return RedirectToAction("Administrador", "Usuario");
        //            //    }
        //            //    else if (t_Usuario == ("TERCER_NIVEL") && t_Rol == ("USUARIO_GENERAL")|| t_Usuario == ("SEGUNDO_NIVEL") && t_Rol == ("USUARIO_GENERAL") || t_Usuario == ("PRIMER_NIVEL") && t_Rol == ("USUARIO_GENERAL"))
        //            //    {
        //            //        return RedirectToAction("Usuarios", "Usuario");
        //            //    }
        //            //    return RedirectToAction("_Login", "Usuario");
        //            //}
        //            #endregion
        //        }
        //        return RedirectToAction("Aministrador", "Usuario");
        //    }
        //    catch (Exception)
        //    {
        //        return RedirectToAction("_Login", "Usuario");
        //        //return Content(ex.ToString());
        //    }
        //}

        /*****************RECUPERAR LA CONTRASEÑA**************/

        #region Recuperar contraseña

        string urlDomain = "http://localhost:51219/";

        [Authorize]
        [AllowAnonymous]
        [HttpGet]
        public ActionResult StarRecovery(string token)
        {
            RecoveryViewModel model = new RecoveryViewModel();
            return View();
        }

        [Authorize]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult StarRecovery(RecoveryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string token = GetSha256(Guid.NewGuid().ToString());

            using (SII_Entities db = new SII_Entities())
            {
                var oUser = db.Tbl_Usuario.Where(d => d.Tbl_Persona.correo_Electronico == model.Email).FirstOrDefault();
                if (oUser != null)
                {
                    oUser.token_recovery = token;
                    token = oUser.token_recovery;
                    db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    //enviar email
                    SendEmail(oUser.Tbl_Persona.correo_Electronico, token);
                }
            }
            return View("Login");
        }

        [Authorize]
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Recovery(string token)
        {
            RecoveryPasswordViewModel model = new RecoveryPasswordViewModel();
            model.token = token;

            using (SII_Entities db = new SII_Entities())
            {

                if (model.token == null || model.token.Trim().Equals(""))
                {
                    return View("Login");
                }
                var oUser = db.Tbl_Usuario.Where(d => d.token_recovery == model.token).FirstOrDefault();
                if (oUser == null)
                {
                    ViewBag.Error = "Session expirada";
                    return View("Login");
                }
            }

            return View(model);
        }

        [Authorize]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Recovery(RecoveryPasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                using (SII_Entities db = new SII_Entities())
                {
                    var oUser = db.Tbl_Usuario.Where(d => d.token_recovery == model.token).FirstOrDefault();

                    if (oUser != null)
                    {
                        oUser.contrasenia = model.Contraseña;
                        oUser.token_recovery = null;
                        db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            ViewBag.Message = "Contraseña modificada con exito";
            return View("Login");
        }

        #region Helpers

        private string GetSha256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }


        private void SendEmail(string EmailDestino, string token)
        {
            string EmailOrigen = "sindicatocr.siice@gmail.com";
            string Contraseña = "Siice.2019";
            string url = urlDomain + "/Usuario/Recovery/?token=" + token;

            MailMessage oMailMessage = new MailMessage(EmailOrigen, EmailDestino, "Recuperacion de contraseña",
                "<p>Correo para recuperacion de contraseña</p><br/>" +
                "<a href='" + url + "'>Click para recuperar<a/>");

            oMailMessage.IsBodyHtml = true;
            SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");

            oSmtpClient.UseDefaultCredentials = false;
            //oSmtpClient.Host = "smtp.gmail.com";
            oSmtpClient.Port = 587;
            oSmtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, Contraseña);
            oSmtpClient.EnableSsl = true;
            oSmtpClient.Send(oMailMessage);
            oSmtpClient.Dispose();
        }
        #endregion

        #endregion

        /*****************************Calendadrio**********************************/
        #region Get Calendar data method.

        /// <summary>
        /// GET: /Home/GetCalendarData
        /// </summary>
        /// <returns>Return data</returns>
        public ActionResult GetCalendarData()
        {
            // Initialization.
            JsonResult result = new JsonResult();

            try
            {
                // Loading.
                List<PublicHoliday> data = this.LoadData();

                // Processing.
                result = this.Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            // Return info.
            return result;
        }

        #endregion

        #region Helpers

        #region Load Data

        /// <summary>
        /// Load data method.
        /// </summary>
        /// <returns>Returns - Data</returns>
        private List<PublicHoliday> LoadData()
        {
            // Initialization.
            List<PublicHoliday> lst = new List<PublicHoliday>();

            try
            {
                // Initialization.
                string line = string.Empty;
                string srcFilePath = "Content/files/PublicHoliday.txt";
                var rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                var fullPath = Path.Combine(rootPath, srcFilePath);
                string filePath = new Uri(fullPath).LocalPath;
                StreamReader sr = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read));

                // Read file.
                while ((line = sr.ReadLine()) != null)
                {
                    // Initialization.
                    PublicHoliday infoObj = new PublicHoliday();
                    string[] info = line.Split(',');

                    // Setting.
                    infoObj.Sr = Convert.ToInt32(info[0].ToString());
                    infoObj.Title = info[1].ToString();
                    infoObj.Desc = info[2].ToString();
                    infoObj.Start_Date = info[3].ToString();
                    infoObj.End_Date = info[4].ToString();

                    // Adding.
                    lst.Add(infoObj);
                }

                // Closing.
                sr.Dispose();
                sr.Close();
            }
            catch (Exception ex)
            {
                // info.
                Console.Write(ex);
            }

            // info.
            return lst;
        }

        #endregion

        #endregion

        public ActionResult close_session()
        {
            Session[AccesoController.sess_name] = null;
            return RedirectToAction("Login", "Acceso");
        }
    }
}