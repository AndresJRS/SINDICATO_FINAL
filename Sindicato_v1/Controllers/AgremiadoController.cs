using Sindicato_v1.Filters;
using Sindicato_v1.Models;
using Sindicato_v1.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Sindicato_v1.Controllers
{
    public class AgremiadoController : Controller
    {
        // GET: Agremiado
        [AuthorizeUser(permiso: 1, tusu: 3)]
        public ActionResult Agremiados(int cedula = 0)
        {
            ViewData["Nombre"] = AccesoController.nombre;
            ViewData["Apellido"] = AccesoController.apellido;

            using (SII_Entities db = new SII_Entities())
            {
                var lista = new List<SelectAgremiadosViewModel>();

                if (cedula != 0)
                {
                    var agremiados = from a in db.Tbl_Agremiado
                                     join u in db.Tbl_Usuario on a.id_Usuario equals u.id_Usuario
                                     join p in db.Tbl_Persona on u.id_Persona equals p.id_Persona
                                     where p.cedula == cedula
                                     select new { p.cedula, p.nombre, p.primer_Apellido, p.segundo_Apellido, p.telefono, p.correo_Electronico, a.puesto, a.profesion };

                    foreach (var agremiado in agremiados.ToList())
                    {
                        var modelo = new SelectAgremiadosViewModel();

                        modelo.cedula = agremiado.cedula;
                        modelo.nombre = agremiado.nombre;
                        modelo.primer_Apellido = agremiado.primer_Apellido;
                        modelo.segundo_Apellido = agremiado.segundo_Apellido;
                        modelo.telefono = agremiado.telefono;
                        modelo.correo_Electronico = agremiado.correo_Electronico;
                        modelo.puesto = agremiado.puesto;
                        modelo.profesion = agremiado.profesion;
                        lista.Add(modelo);
                    }
                }
                else
                {
                    var agremiados = from a in db.Tbl_Agremiado
                                     join u in db.Tbl_Usuario on a.id_Usuario equals u.id_Usuario
                                     join p in db.Tbl_Persona on u.id_Persona equals p.id_Persona
                                     select new { p.cedula, p.nombre, p.primer_Apellido, p.segundo_Apellido, p.telefono, p.correo_Electronico, a.puesto, a.profesion };

                    foreach (var agremiado in agremiados.ToList())
                    {
                        var modelo = new SelectAgremiadosViewModel();

                        modelo.cedula = agremiado.cedula;
                        modelo.nombre = agremiado.nombre;
                        modelo.primer_Apellido = agremiado.primer_Apellido;
                        modelo.segundo_Apellido = agremiado.segundo_Apellido;
                        modelo.telefono = agremiado.telefono;
                        modelo.correo_Electronico = agremiado.correo_Electronico;
                        modelo.puesto = agremiado.puesto;
                        modelo.profesion = agremiado.profesion;
                        lista.Add(modelo);
                    }
                }
                return View(lista);
            }
        }

        [AuthorizeUser(permiso: 1, tusu: 3)]
        public ActionResult Editar(int id)
        {

            SelectAgremiadosViewModel model = new SelectAgremiadosViewModel();

            using (SII_Entities db = new SII_Entities())
            {
                var agremiado = db.Tbl_Agremiado.Find(id);
                model.profesion = agremiado.profesion;
                model.puesto = agremiado.puesto;
                model.correo_Electronico = agremiado.Tbl_Usuario.Tbl_Persona.correo_Electronico;
                model.telefono = agremiado.Tbl_Usuario.Tbl_Persona.telefono;
            }

            return View(model);
        }

        [AuthorizeUser(permiso: 1, tusu: 3)]
        [HttpPost]
        public ActionResult Editar(SelectAgremiadosViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (SII_Entities db = new SII_Entities())
                    {
                        var agremiado = db.Tbl_Agremiado.Find(model.id_Agre);
                        agremiado.profesion = model.profesion;
                        agremiado.Tbl_Usuario.Tbl_Persona.telefono = model.telefono;
                        agremiado.puesto = model.puesto;
                        agremiado.Tbl_Usuario.Tbl_Persona.correo_Electronico = model.correo_Electronico;
                        db.Entry(agremiado).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("/Agremiados/Agremiados");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}



