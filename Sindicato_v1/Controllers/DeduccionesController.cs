using Sindicato_v1.Filters;
using Sindicato_v1.Models;
using Sindicato_v1.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace Sindicato_v1.Controllers
{
    public class DeduccionesController : Controller
    { 
        [AuthorizeUser(permiso: 1, tusu: 3)] 
        public ActionResult BusquedaDeduccion(int cedula = 0)
        {
            ViewData["Nombre"] = AccesoController.nombre;
            ViewData["Apellido"] = AccesoController.apellido;

            using (SII_Entities db = new SII_Entities())
            {
                var lista = new List<SelectDeduccionesViewModel>();

                if (cedula != 0)
                {
                    var deducciones = from d in db.Tbl_Deduccion
                                      join a in db.Tbl_Agremiado on d.id_Agremiado equals a.id_Agremiado
                                      join u in db.Tbl_Usuario on a.id_Usuario equals u.id_Usuario
                                      join p in db.Tbl_Persona on u.id_Persona equals p.id_Persona
                                      where p.estado == 1 && p.cedula == cedula
                                      select new { p.cedula, p.nombre, p.primer_Apellido, p.segundo_Apellido, p.telefono, p.correo_Electronico, d.fecha_Deduccion, d.monto, a.puesto };

                    foreach (var deduccion in deducciones.ToList())
                    {
                        var modelo = new SelectDeduccionesViewModel();

                        modelo.cedula = deduccion.cedula;
                        modelo.nombre = deduccion.nombre;
                        modelo.primer_apellido = deduccion.primer_Apellido;
                        modelo.segundo_apellido = deduccion.segundo_Apellido;
                        modelo.telefono = deduccion.telefono;
                        modelo.correo = deduccion.correo_Electronico;
                        modelo.fecha_deduccion = deduccion.fecha_Deduccion;
                        modelo.monto = deduccion.monto;
                        modelo.puesto = deduccion.puesto;
                        lista.Add(modelo);
                    }
                }
                else
                {
                    var deducciones = from d in db.Tbl_Deduccion
                                      join a in db.Tbl_Agremiado on d.id_Agremiado equals a.id_Agremiado
                                      join u in db.Tbl_Usuario on a.id_Usuario equals u.id_Usuario
                                      join p in db.Tbl_Persona on u.id_Persona equals p.id_Persona
                                      select new { p.cedula, p.nombre, p.primer_Apellido, p.segundo_Apellido, p.telefono, p.correo_Electronico, d.fecha_Deduccion, d.monto, a.puesto };

                    foreach (var deduccion in deducciones.ToList())
                    {
                        var modelo = new SelectDeduccionesViewModel();

                        modelo.cedula = deduccion.cedula;
                        modelo.nombre = deduccion.nombre;
                        modelo.primer_apellido = deduccion.primer_Apellido;
                        modelo.segundo_apellido = deduccion.segundo_Apellido;
                        modelo.telefono = deduccion.telefono;
                        modelo.correo = deduccion.correo_Electronico;
                        modelo.fecha_deduccion = deduccion.fecha_Deduccion;
                        modelo.monto = deduccion.monto;
                        modelo.puesto = deduccion.puesto;
                        lista.Add(modelo);
                    }
                }

                return View(lista);
            }
        }

        [AuthorizeUser(permiso: 1, tusu: 3)]
        public ActionResult Editar(int id)
        {

            SelectDeduccionesViewModel model = new SelectDeduccionesViewModel();

            using (SII_Entities db = new SII_Entities())
            {
                var deduccion = db.Tbl_Deduccion.Find(id);
                model.fecha_deduccion = deduccion.fecha_Deduccion;
                model.monto = deduccion.monto;

            }

            return View(model);
        }

        [AuthorizeUser(permiso: 1, tusu: 3)]
        [HttpPost]
        public ActionResult Editar(SelectDeduccionesViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (SII_Entities db = new SII_Entities())
                    {
                        var deduccion = db.Tbl_Deduccion.Find(model.id);
                        deduccion.fecha_Deduccion = model.fecha_deduccion;
                        deduccion.monto = model.monto;
                        db.Entry(deduccion).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("/Deducciones/BusquedaDeducciones");
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