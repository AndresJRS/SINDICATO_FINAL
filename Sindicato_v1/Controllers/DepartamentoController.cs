using Sindicato_v1.Filters;
using Sindicato_v1.Models;
using Sindicato_v1.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace Sindicato_v1.Controllers
{
    public class DepartamentoController : Controller
    {
        // GET: Departamento
        [AuthorizeUser(permiso: 1, tusu: 3)]
        public ActionResult Mant_Departamento()
        {
            ViewData["Nombre"] = AccesoController.nombre;
            ViewData["Apellido"] = AccesoController.apellido;

            List<SelectDepartamentoViewModel> lst;
            using (SII_Entities db = new SII_Entities())
            {
                lst = (from d in db.Tbl_Departamento
                       join c in db.Tbl_Compania
                       on d.id_Compania equals c.id_Compania
                       where d.estado == 1 || d.estado == 3
                       select new SelectDepartamentoViewModel
                       {
                           id_D = d.id_Departamento,
                           depart = d.departamento,
                           ubic = d.ubicacion,
                           id_Comp = d.id_Compania,
                           nom_Comp = c.nom_Compania,
                           est = d.estado
                       }).ToList();
            }
            return View(lst);
        }
        [AuthorizeUser(permiso: 1, tusu: 3)]
        public ActionResult Nuevo_Departamento()
        {
            ViewData["Nombre"] = AccesoController.nombre;
            ViewData["Apellido"] = AccesoController.apellido;

            loadDropDownList();

            return View();
        }
        [AuthorizeUser(permiso: 1, tusu: 3)]
        [HttpPost]
        public ActionResult Nuevo_Departamento(AddDepartamentoViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (SII_Entities db = new SII_Entities())
                    {
                        var obj_Dep = new Tbl_Departamento();
                        obj_Dep.departamento = model.depart;
                        obj_Dep.ubicacion = model.ubic;
                        obj_Dep.id_Compania = model.id_Comp;
                        obj_Dep.estado = 1;
                        db.Tbl_Departamento.Add(obj_Dep);
                        db.SaveChanges();
                    }
                    return Redirect("/Departamento/Mant_Departamento");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [AuthorizeUser(permiso: 1, tusu: 3)]
        public ActionResult Editar_Departamento(int ID)
        {
            ViewData["Nombre"] = AccesoController.nombre;
            ViewData["Apellido"] = AccesoController.apellido;

            loadDropDownList();

            AddDepartamentoViewModel model = new AddDepartamentoViewModel();
            using (SII_Entities db = new SII_Entities())
            {
                var obj_Dep = db.Tbl_Departamento.Find(ID);
                model.depart = obj_Dep.departamento;
                model.ubic = obj_Dep.ubicacion;
                model.id_Comp = obj_Dep.id_Compania;
                model.id_D = obj_Dep.id_Departamento;
            }
            return View(model);
        }
        [AuthorizeUser(permiso: 1, tusu: 3)]
        [HttpPost]
        public ActionResult Editar_Departamento(AddDepartamentoViewModel model)
        {
            loadDropDownList();

            try
            {
                if (ModelState.IsValid)
                {
                    using (SII_Entities db = new SII_Entities())
                    {
                        var obj_Dep = db.Tbl_Departamento.Find(model.id_D);
                        obj_Dep.departamento = model.depart;
                        obj_Dep.ubicacion = model.ubic;
                        obj_Dep.id_Compania = model.id_Comp;

                        db.Entry(obj_Dep).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("/Departamento/Mant_Departamento");
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
        public ActionResult Eliminar_Departamento(int id)
        {
            using (SII_Entities db = new SII_Entities())
            {
                var obj_Dep = db.Tbl_Departamento.Find(id);

                if (obj_Dep.estado == 1)
                {
                    obj_Dep.estado = 0;
                }
                db.Entry(obj_Dep).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Redirect("/Departamento/Mant_Departamento");
        }

        public void loadDropDownList()
        {
            SII_Entities dba = new SII_Entities();
            List<Tbl_Compania> list = dba.Tbl_Compania.ToList();
            ViewBag.CompaniaList = new SelectList(list, "id_Compania", "nom_Compania");
        }
    }

}