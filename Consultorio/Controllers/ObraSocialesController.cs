using Consultorio.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Consultorio.Controllers
{
    public class ObraSocialesController : Controller
    {
        private ConsultorioEntities db = new ConsultorioEntities();

        public ActionResult Listar()
        {
            return View("Listar", db.ObraSociales.OrderBy(d=>d.Descripcion));
        }

        public ActionResult MostrarDetalles(int id)
        {
            ObraSociale obrasocial = db.ObraSociales.Find(id);
            return View("MostrarDetalles", obrasocial);
        }

        public  ActionResult AgregarOS()
        {
            ObraSociale nueva = new ObraSociale();
            return View("AgregarOS", nueva);
        }

        [HttpPost]
        public ActionResult AgregarOS(ObraSociale nueva)
        {

            // try catch
            if (ModelState.IsValid)
            {
                db.ObraSociales.Add(nueva);
                db.SaveChanges();
                return RedirectToAction("Listar");
            }
            else
            {
                return View("AgregarOS", nueva);
            }
        }

        public ActionResult ModificarOS(int? id)
        {
            ObraSociale obrasocial = db.ObraSociales.Find(id);
            if (obrasocial == null)
            {
                return HttpNotFound();
            }
            return View(obrasocial);
        }

        [HttpPost]
        public ActionResult ModificarOS(ObraSociale obrasocial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(obrasocial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Listar");
            }
            else
            {
                return View("ModificarOS", obrasocial);
            }
        }

        public ActionResult BorrarOS(int? id)
        {
            ObraSociale obrasocial = db.ObraSociales.Find(id);
            if (obrasocial==null)
            {
                return HttpNotFound();
            }
            return View(obrasocial);
        }

        [HttpPost]
        public ActionResult BorrarOs(int id)
        {
            ObraSociale obrasocial = db.ObraSociales.Find(id);
            try
            {
                db.ObraSociales.Remove(obrasocial);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
                // agregar label para mensaje de error
                throw new Exception("Error de borrar. El mensaje  es {0}" + mensaje);
            }

            return RedirectToAction("Listar");

        }

    }
}