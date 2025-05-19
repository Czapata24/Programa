using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HankoSpa.Services.Interfaces;
using HankoSpa.DTOs;
using HankoSpa.Models;

namespace HankoSpa.Controllers
{
    public class CitasController : Controller
    {
        private readonly ICitaServices _citaService;
        private readonly IServicioServices _servicioService;

        public CitasController(ICitaServices citaService, IServicioServices servicioService)
        {
            _citaService = citaService;
            _servicioService = servicioService;
        }

        // GET: Citas
        public async Task<IActionResult> Index()
        {
            var response = await _citaService.GetAllAsync();

            if (!response.IsSuccess)
            {
                ViewBag.ErrorMessage = response.Message;
                return View(new List<Cita>());
            }

            var citas = response.Result.Select(dto => new Cita
            {
                CitaId = dto.CitaId,
                FechaCita = dto.FechaCita,
                HoraCita = dto.HoraCita,
                EstadoCita = dto.EstadoCita,
                UsuarioID = dto.UsuarioID,
                ServicioId = dto.ServicioID
            }).ToList();

            return View(citas);
        }

        // GET: Citas/Create
        public async Task<IActionResult> Create()
        {
            await CargarServiciosAsync();
            return View();
        }

        // GET: Citas/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _citaService.GetOneAsync(id);
            if (!response.IsSuccess || response.Result == null)
            {
                TempData["MensajeError"] = "La cita no fue encontrada.";
                return RedirectToAction("Index");
            }

            return View(response.Result);
        }
                

        // POST: Citas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CitaDTO citaDTO)
        {
            if (ModelState.IsValid)
            {
                var response = await _citaService.CreateAsync(citaDTO);

                if (response.IsSuccess)
                {
                    TempData["MensajeExito"] = "Cita creada exitosamente.";
                    return RedirectToAction("Index");
                }

                ViewBag.ErrorMessage = response.Message;
            }

            await CargarServiciosAsync(citaDTO.ServicioID);
            return View(citaDTO);
        }

        // GET: Citas/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _citaService.GetOneAsync(id);
            if (!response.IsSuccess || response.Result == null)
            {
                TempData["MensajeError"] = "La cita no fue encontrada.";
                return RedirectToAction("Index");
            }

            await CargarServiciosAsync(response.Result.ServicioID);
            return View(response.Result);
        }

        // POST: Citas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CitaDTO citaDTO)
        {
            if (id != citaDTO.CitaId) return NotFound();

            if (ModelState.IsValid)
            {
                var response = await _citaService.EditAsync(citaDTO);
                if (response.IsSuccess)
                {
                    TempData["MensajeExito"] = "Cita actualizada exitosamente.";
                    return RedirectToAction("Index");
                }

                ViewBag.ErrorMessage = response.Message;
            }

            await CargarServiciosAsync(citaDTO.ServicioID);
            return View(citaDTO);
        }

        // GET: Citas/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var response = await _citaService.GetOneAsync(id.Value);
            if (!response.IsSuccess || response.Result == null)
            {
                TempData["MensajeError"] = "La cita no fue encontrada.";
                return RedirectToAction("Index");
            }

            return View(response.Result);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _citaService.DeleteAsync(id);
            if (response.IsSuccess)
            {
                TempData["MensajeExito"] = "Cita eliminada exitosamente.";
            }
            else
            {
                TempData["MensajeError"] = response.Message;
            }

            return RedirectToAction("Index");
        }

        // Método auxiliar para cargar servicios
        private async Task CargarServiciosAsync(int? seleccionado = null)
        {
            var servicios = await _servicioService.GetAllAsync();
            ViewBag.Servicios = new SelectList(servicios.Result, "ServicioId", "NombreServicio", seleccionado);
        }
    }
}
