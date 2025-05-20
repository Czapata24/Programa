using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HankoSpa.Services.Interfaces;
using HankoSpa.DTOs;
using Microsoft.AspNetCore.Authorization;
using HankoSpa.Controllers.Attributes;

namespace HankoSpa.Controllers
{
    public class ServicioController : Controller
    {
        private readonly IServicioServices _servicioService;

        public ServicioController(IServicioServices servicioService)
        {
            _servicioService = servicioService;
        }

        // GET: Servicio
        [CustomAuthorize(permission: "showSection", module: "Secciones")]
        public async Task<IActionResult> Index()
        {
            var response = await _servicioService.GetAllAsync();

            if (!response.IsSuccess)
            {
                ViewBag.ErrorMessage = response.Message;
                return View(new List<ServiceDTO>());
            }

            return View(response.Result);
        }

        // GET: Servicio/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: Servicio/Edit
        public IActionResult Edit()
        {
            return View();
        }

        // GET: Servicio/Delete
        public IActionResult Delete()
        {
            return View();
        }

        // POST: Servicio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceDTO servicioDTO)
        {
            if (ModelState.IsValid)
            {
                var response = await _servicioService.CreateAsync(servicioDTO);

                if (response.IsSuccess)
                {
                    TempData["MensajeExito"] = "Servicio creado exitosamente.";
                    return RedirectToAction("Index");
                }

                ViewBag.ErrorMessage = response.Message;
            }

            return View(servicioDTO);
        }

        // POST: Servicio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ServiceDTO servicioDTO)
        {
            if (id != servicioDTO.ServicioId) return NotFound();

            if (ModelState.IsValid)
            {
                var response = await _servicioService.EditAsync(servicioDTO);
                if (response.IsSuccess)
                {
                    TempData["MensajeExito"] = "Servicio actualizado exitosamente.";
                    return RedirectToAction("Index");
                }

                ViewBag.ErrorMessage = response.Message;
            }

            return View(servicioDTO);
        }


        // GET: Servicio/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _servicioService.GetOneAsync(id);
            if (!response.IsSuccess || response.Result == null)
            {
                TempData["MensajeError"] = "El servicio no fue encontrado.";
                return RedirectToAction("Index");
            }

            return View(response.Result);
        }

        // GET: Servicio/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var response = await _servicioService.GetOneAsync(id.Value);
            if (!response.IsSuccess || response.Result == null)
            {
                TempData["MensajeError"] = "El servicio no fue encontrado.";
                return RedirectToAction("Index");
            }

            return View(response.Result);
        }

        // POST: Servicio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _servicioService.DeleteAsync(id);
            if (response.IsSuccess)
            {
                TempData["MensajeExito"] = "Servicio eliminado exitosamente.";
            }
            else
            {
                TempData["MensajeError"] = response.Message;
            }

            return RedirectToAction("Index");
        }

    }

}