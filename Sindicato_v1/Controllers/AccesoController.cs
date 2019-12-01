using Sindicato_v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sindicato_v1.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        public static int pendientes;
        public static string sess_name;
        public static string nombre;
        public static string apellido;

        [HttpPost]
        public ActionResult Login(int user, string contrasenia)
        {
            try
            {
                using (SII_Entities db = new SII_Entities())
                {
                    string encryptedPass = Encrypt.GetSHA256(contrasenia);

                    var usuario = (from u in db.Tbl_Usuario
                                   join p in db.Tbl_Persona on u.id_Persona equals p.id_Persona
                                   join tr in db.Tbl_Rol on u.id_Rol equals tr.id_Rol
                                   join tu in db.Tbl_TipoUsuario on u.id_TipoUsu equals tu.id_TipoUsu
                                   where u.contrasenia == encryptedPass.Trim() && p.cedula == user
                                   select u).FirstOrDefault();


                    var lista_pend = from p in db.Tbl_Persona
                                     where p.estado == 2
                                     select new { p.id_Persona };

                    #region NombreUsuario, Pendientes

                    pendientes = lista_pend.Count();

                    nombre = (from p in db.Tbl_Persona
                               where usuario.id_Persona == usuario.id_Persona
                               select p.nombre).First();

                    apellido = (from p in db.Tbl_Persona
                                where usuario.id_Persona == usuario.id_Persona
                                select p.primer_Apellido).First();

                    #endregion

                    if (usuario == null)
                    {
                        ViewBag.Error = "¡Usuario o contraseña invalidos!";
                        return Login();
                    }

                    if (usuario.id_Rol == 1 || usuario.id_Rol == 2)
                    {
                        sess_name = "User";

                        Session[sess_name] = usuario;
                    }
                    else if(usuario.id_Rol == 4)
                    {
                        sess_name = "Agremiado";

                        Session[sess_name] = usuario;
                    }
                    #region roles
                    //if (lista.Count() == 1)
                    //{
                    //    var idUsuario = (from u in lista
                    //                     select u.id_Usuario).First();

                    //    Session["User"] = idUsuario;

                    //    if (t_Usuario == ("TERCER_NIVEL") && t_Rol == ("ADMINISTRADOR") || t_Usuario == ("TERCER_NIVEL") && t_Rol == ("GESTOR_DEDUCCIONES"))
                    //    {
                    //        return RedirectToAction("Administrador", "Usuario");
                    //    }
                    //    else if (t_Usuario == ("TERCER_NIVEL") && t_Rol == ("USUARIO_GENERAL")|| t_Usuario == ("SEGUNDO_NIVEL") && t_Rol == ("USUARIO_GENERAL") || t_Usuario == ("PRIMER_NIVEL") && t_Rol == ("USUARIO_GENERAL"))
                    //    {
                    //        return RedirectToAction("Usuarios", "Usuario");
                    //    }
                    //    return RedirectToAction("_Login", "Usuario");
                    //}
                    #endregion
                }

                string view = null;

                if (Session[sess_name] == Session["User"])
                {
                    view = "Administrador";
                }
                else if (Session[sess_name] == Session["Agremiado"])
                {
                    view = "Usuarios";
                }
                return RedirectToAction(view, "Usuario");
            }
            catch (Exception)
            {
                return RedirectToAction("_Login", "Usuario");
            }
        }
    }
}