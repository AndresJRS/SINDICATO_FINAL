using Sindicato_v1.Filters;
using Sindicato_v1.Models;
using Sindicato_v1.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Sindicato_v1.Controllers
{
    public class CompaniaController : Controller
    {
        // GET: Compania
        [AuthorizeUser(permiso: 1, tusu: 3)]
        public ActionResult Mant_Compania()
        {
            ViewData["Nombre"] = AccesoController.nombre;
            ViewData["Apellido"] = AccesoController.apellido;

            List<SelectCompaniaViewModel> lst;
            using (SII_Entities db = new SII_Entities())
            {
                lst = (from d in db.Tbl_Compania
                       where d.estado == 1 || d.estado == 3
                       select new SelectCompaniaViewModel
                       {
                           id_Comp = d.id_Compania,
                           razon_Soc = d.razon_Social,
                           cedula_Jud = d.cedula_Juridica,
                           nombre_Comp = d.nom_Compania,
                           direc = d.direccion,
                           tel = d.telefono,
                           correo = d.correo_Electronico,
                           estado = d.estado
                       }).ToList();
            }
            return View(lst);
        }

        [AuthorizeUser(permiso: 1, tusu: 3)]
        public ActionResult Nueva_Compania()
        {
            ViewData["Nombre"] = AccesoController.nombre;
            ViewData["Apellido"] = AccesoController.apellido;
            return View();
        }

        [AuthorizeUser(permiso: 1, tusu: 3)]
        [HttpPost]
        public ActionResult Nueva_Compania(AddCompaniaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (SII_Entities db = new SII_Entities())
                    {
                        var obj_Comp = new Tbl_Compania();
                        obj_Comp.razon_Social = model.razon_Soc;
                        obj_Comp.cedula_Juridica = model.cedula_Jud;
                        obj_Comp.nom_Compania = model.nombre_Comp;
                        obj_Comp.direccion = model.direc;
                        obj_Comp.telefono = model.tel;
                        obj_Comp.correo_Electronico = model.correo;
                        obj_Comp.estado = 1;
                        db.Tbl_Compania.Add(obj_Comp);
                        db.SaveChanges();
                    }
                    return Redirect("/Compania/Mant_Compania");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [AuthorizeUser(permiso: 1, tusu: 3)]
        public ActionResult Editar_Compania(int ID)
        {
            ViewData["Nombre"] = AccesoController.nombre;
            ViewData["Apellido"] = AccesoController.apellido;

            AddCompaniaViewModel model = new AddCompaniaViewModel();
            using (SII_Entities db = new SII_Entities())
            {
                var obj_Comp = db.Tbl_Compania.Find(ID);

                model.razon_Soc = obj_Comp.razon_Social;
                model.cedula_Jud = obj_Comp.cedula_Juridica;
                model.nombre_Comp = obj_Comp.nom_Compania;
                model.direc = obj_Comp.direccion;
                model.tel = obj_Comp.telefono;
                model.correo = obj_Comp.correo_Electronico;
                model.id_Comp = obj_Comp.id_Compania;
            }
            return View(model);
        }

        [AuthorizeUser(permiso: 1, tusu: 3)]
        [HttpPost]
        public ActionResult Editar_Compania(AddCompaniaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (SII_Entities db = new SII_Entities())
                    {
                        var obj_Comp = db.Tbl_Compania.Find(model.id_Comp);
                        obj_Comp.razon_Social = model.razon_Soc;
                        obj_Comp.cedula_Juridica = model.cedula_Jud;
                        obj_Comp.nom_Compania = model.nombre_Comp;
                        obj_Comp.direccion = model.direc;
                        obj_Comp.telefono = model.tel;
                        obj_Comp.correo_Electronico = model.correo;
                        db.Entry(obj_Comp).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("/Compania/Mant_Compania");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [AuthorizeUser(permiso: 1, tusu: 3)]
        [HttpGet]
        public ActionResult Eliminar_Compania(int id)
        {
            using (SII_Entities db = new SII_Entities())
            {
                var obj_Comp = db.Tbl_Compania.Find(id);

                if (obj_Comp.estado == 1)
                {
                    obj_Comp.estado = 0;
                }
                db.Entry(obj_Comp).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Redirect("/Compania/Mant_Compania");
        }
    }
}