using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FirmaCurierat.Models;
using FirmaCurierat.Services;

namespace FirmaCurierat.Controllers
{
    public class HubsController : Controller
    {
       
        private readonly IHubService _hubService;

        public HubsController(IHubService hubService)
        {
            _hubService = hubService;
        }

        // GET: Hubs 
        public async Task<IActionResult> Index()
        {
            // Controller-ul cere serviciului lista cu toate sediile
            var hubs = await _hubService.GetAllHubsAsync();

            // afiseaza lista
            return View(hubs);
        }
        // GET: Hubs/Details/5 (Afiseaza detaliile unui singur Hub)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var hub = await _hubService.GetHubByIdAsync(id.Value);
            if (hub == null) return NotFound();

            return View(hub);
        }

        // GET: Hubs/Create (Afiseaza formularul gol pentru a adauga un Hub nou)
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hubs/Create (Primeste datele din formular si le salveaza)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_hub,Oras,Capacitate,Id_adresa")] Hub hub)
        {
            if (ModelState.IsValid)
            {
                // executa salvarea prin intermediul Serviciului
                await _hubService.AddHubAsync(hub);
                return RedirectToAction(nameof(Index)); 
            }
            return View(hub);
        }

        // GET: Hubs/Edit/5 (incarcă datele unui Hub existent pentru a fi modificate)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var hub = await _hubService.GetHubByIdAsync(id.Value);
            if (hub == null) return NotFound();

            return View(hub);
        }

        // POST: Hubs/Edit/5 (Salveaza noile modificari)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_hub,Oras,Capacitate,Id_adresa")] Hub hub)
        {
            if (id != hub.Id_hub) return NotFound();

            if (ModelState.IsValid)
            {
                await _hubService.UpdateHubAsync(hub);
                return RedirectToAction(nameof(Index));
            }
            return View(hub);
        }

        // GET: Hubs/Delete/5 (Afisează o pagina de confirmare a stergerii)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var hub = await _hubService.GetHubByIdAsync(id.Value);
            if (hub == null) return NotFound();

            return View(hub);
        }

        // POST: Hubs/Delete/5 (Executa stergerea definitiva)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _hubService.DeleteHubAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}