using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using restaurant.Data;
using restaurant.Models;

namespace restaurant.Areas.Yonetici.Controllers
{
    [Area("Yonetici")]
    [Authorize]

    public class İletisimController : Controller
    {
        private readonly ApplicationDbContext _context;

        public İletisimController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Yonetici/İletisim
        public async Task<IActionResult> Index()
        {
            return View(await _context.İletisims.ToListAsync());
        }

        // GET: Yonetici/İletisim/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var İletisim = await _context.İletisims
                .FirstOrDefaultAsync(m => m.Id == id);
            if (İletisim == null)
            {
                return NotFound();
            }

            return View(İletisim);
        }

        // GET: Yonetici/İletisim/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Yonetici/İletisim/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Telefon,Mesaj,Tarih")] İletisim İletisim)
        {
            if (ModelState.IsValid)
            {
                _context.Add(İletisim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(İletisim);
        }

        // GET: Yonetici/İletisim/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var İletisim = await _context.İletisims.FindAsync(id);
            if (İletisim == null)
            {
                return NotFound();
            }
            return View(İletisim);
        }

        // POST: Yonetici/İletisim/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Telefon,Mesaj,Tarih")] İletisim İletisim)
        {
            if (id != İletisim.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(İletisim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!İletisimExists(İletisim.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(İletisim);
        }

        // GET: Yonetici/İletisim/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var İletisim = await _context.İletisims
                .FirstOrDefaultAsync(m => m.Id == id);
            if (İletisim == null)
            {
                return NotFound();
            }

            return View(İletisim);
        }

        // POST: Yonetici/İletisim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var İletisim = await _context.İletisims.FindAsync(id);
            _context.İletisims.Remove(İletisim);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool İletisimExists(int id)
        {
            return _context.İletisims.Any(e => e.Id == id);
        }
    }
}
