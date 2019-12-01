using Sindicato_v1.Filters;
using Sindicato_v1.Models;
using Sindicato_v1.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Sindicato_v1
{
    public partial class Deducciones : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private static List<LoadDeduccionesViewModel> list = new List<LoadDeduccionesViewModel>();

        public static string ced_Final;

        protected void btn_Cargar_Click(object sender, EventArgs e)
        {
            /*----------------Se sube el documento y lo lee---------------------*/
            try
            {
                fUpload.SaveAs(Server.MapPath("~/uploads/" + fUpload.FileName));

                string doc = Server.MapPath("~/uploads/fichero.txt");

                /*--------------------Patron regex para eliminar caracteres no numericos------------------------*/
                string patron_cedula_mon = @"(?:- *)?\d+(?:\.\d+)?";
                string patron_nombre = @"0-9?";

                using (StreamReader file = new StreamReader(doc))
                {
                    Regex regex_ced_mon = new Regex(patron_cedula_mon);
                    Regex regex_nom = new Regex(patron_nombre);

                    string ln;

                    while ((ln = file.ReadLine()) != null)
                    {
                        string num_Ced = ln.Substring(7, 12);
                        string ced_Match = Regex.Match(num_Ced, @"\d+").Value;

                        if (ced_Match.Length == 12)
                        {
                            ced_Final = ced_Match.Substring(0, 12);
                        }
                        else
                        {
                            ced_Final = ced_Match.Substring(0, 9);
                        }

                        foreach (Match ced in regex_ced_mon.Matches(ced_Final))
                        {
                            string monto_String = ln.Substring(31, 29);
                            string monto_Match = Regex.Match(monto_String, @"\d+").Value;
                            string monto_Final = monto_Match.Substring(5, 5);

                            foreach (Match mont in regex_ced_mon.Matches(monto_Final))
                            {
                                list.Add(new LoadDeduccionesViewModel { Cedula = ced_Final, Monto = Decimal.Parse(monto_Final), Fecha = DateTime.Now });
                            }
                        }
                    }

                    var duplicates = list.GroupBy(s => s.Cedula)
                                 .Where(g => g.Count() > 1)
                                 .Select(g => g.Key);

                    if (duplicates.Count() == 0)
                    {
                        dgv_Deduc.DataSource = list;
                        dgv_Deduc.DataBind();
                    }
                    else
                    {
                        Response.Write("<script>alert('¡Hay cédulas repetidas! por favor, revise el documento e intente de nuevo.');</script>");
                        list.Clear();
                    }
                    file.Close();
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        List<SelectAgremiadosViewModel> lst;
        protected void btn_Confirmar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SII_Entities db = new SII_Entities())
                {
                    lst = (from a in db.Tbl_Agremiado
                           join u in db.Tbl_Usuario on a.id_Usuario equals u.id_Usuario
                           join p in db.Tbl_Persona on u.id_Persona equals p.id_Persona
                           where a.estado == 1
                           select new SelectAgremiadosViewModel
                           {
                               id_Agre = a.id_Agremiado,
                               cedula = p.cedula
                           }).ToList();
                }

                foreach (var persona in lst)
                {
                    foreach (var dato in list)
                    {
                        if ($"{persona.cedula}".Equals($"{dato.Cedula}"))
                        {
                            try
                            {
                                if (ModelState.IsValid)
                                {
                                    using (SII_Entities db = new SII_Entities())
                                    {
                                        var obj_Ded = new Tbl_Deduccion();

                                        obj_Ded.fecha_Deduccion = dato.Fecha;
                                        obj_Ded.monto = dato.Monto;
                                        obj_Ded.id_Agremiado = persona.id_Agre;
                                        obj_Ded.estado = 1;
                                        db.Tbl_Deduccion.Add(obj_Ded);
                                        db.SaveChanges();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message);
                            }
                        }
                    }
                }
                Response.Redirect(Page.ResolveClientUrl("/Deducciones/BusquedaDeduccion"));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}