using Consultorio.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Consultorio.Controllers
{
    public class ParaDoctorController : Controller
    {
        private ConsultorioEntities db = new ConsultorioEntities();

        public ActionResult ListarPacientes()
        {
            return View("ListarPacientes", db.Pacientes.OrderBy(p=>p.Apellido));
        }

        public ActionResult MostrarPaciente(int id)
        {
            Paciente paciente = db.Pacientes.Find(id);
            return View("MostrarPaciente", paciente);
        }


  
        //Ver
        public ActionResult MostrarHistoria(int pId, int? id)
        {
            if (id==null)
            {
                return RedirectToAction("AgregarHistoria", new { idPaciente = pId });
            }
            else
            {
                var historia = db.HistoriaClinicas.Find(id);
               return View("MostrarHistoria", historia); 
            }
        }

        public ActionResult AgregarPaciente()
        {
            ViewBag.Obrasocial = new SelectList(db.ObraSociales, "ID", "Descripcion");
            return View();
        }


        [HttpPost]
        public ActionResult AgregarPaciente(Paciente newPaciente)
        {
            if (ModelState.IsValid)
            {
                db.Pacientes.Add(newPaciente);
                db.SaveChanges();
                return RedirectToAction("ListarPacientes");
            }

            return RedirectToAction("ListarPacientes");

        }


        private static int idPacienteParaHistoria;
        public ActionResult AgregarHistoria(int idPaciente)
        {
            HistoriaClinica historia = new HistoriaClinica();
            idPacienteParaHistoria = idPaciente;
            return View("AgregarHistoria", historia);
        }

        // CREAR STORE PROCEDURE
        [HttpPost]
        public ActionResult AgregarHistoria(HistoriaClinica historia)
        {
            if (ModelState.IsValid)
            {
                db.HistoriaClinicas.Add(historia);
                db.SaveChanges();
                Paciente paciente = db.Pacientes.Find(idPacienteParaHistoria);
                paciente.IdHistoriaClinica = historia.ID;
                db.Entry(paciente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListarPacientes");
            }
            else
            {
                return View("AgregarHistoria", historia);
            }
        }

        public ActionResult ModificarPaciente(int? id)
        {
            ViewBag.Obrasocial = new SelectList(db.ObraSociales, "ID", "Descripcion");

            Paciente paciente = db.Pacientes.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }


        [HttpPost]
        public ActionResult ModificarPaciente(int id, Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paciente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MostrarPaciente", paciente.ID);
            }
            else
            {
                return View("ModificarPaciente", paciente);
            }
        }


        public ActionResult ModificarHistoria(int? id)
        {
            HistoriaClinica historia = db.HistoriaClinicas.Find(id);
            if (historia == null)
            {
                return HttpNotFound();
            }
            return View(historia);
        }


        [HttpPost]
        public ActionResult ModificarHistoria(int id, HistoriaClinica historia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MostrarHistoria", historia.ID);
            }
            else
            {
                return View("ModificarHistoria", historia);
            }
        }
        public ActionResult BorrarPaciente(int? id)
        {
            Paciente paciente = db.Pacientes.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        [HttpPost]
        public ActionResult BorrarPaciente(int id)
        {
            Paciente paciente = db.Pacientes.Find(id);
            try
            {
                db.Pacientes.Remove(paciente);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
                // agregar label para mensaje de error
                throw new Exception("Error de borrar. El mensaje  es {0}" + mensaje);
            }

            return RedirectToAction("ListarPacientes");

        }


        public ActionResult BorrarHistoria(int? id)
        {
            HistoriaClinica historia = db.HistoriaClinicas.Find(id);
            if (historia == null)
            {
                return HttpNotFound();
            }
            return View(historia);
        }

        [HttpPost]
        public ActionResult BorrarHistoria(int id)
        {

            HistoriaClinica historia = db.HistoriaClinicas.Find(id);
            try
            {
                db.HistoriaClinicas.Remove(historia);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
                // agregar label para mensaje de error
                throw new Exception("Error de borrar. El mensaje  es {0}" + mensaje);
            }

            return RedirectToAction("ListarPacientes");

        }
    }
}