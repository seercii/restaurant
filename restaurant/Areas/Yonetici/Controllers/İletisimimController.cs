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

    public class İletisimimController : Controller
    {
        private readonly ApplicationDbContext _context;

        public İletisimimController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Yonetici/İletisimim
        public async Task<IActionResult> Index()
        {
            return View(await _context.İletisimims.ToListAsync());
        }

        // GET: Yonetici/İletisimim/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var İletisimim = await _context.İletisimims
                .FirstOrDefaultAsync(m => m.id == id);
            if (İletisimim == null)
            {
                return NotFound();
            }

            return View(İletisimim);
        }

        // GET: Yonetici/İletisimim/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Yonetici/İletisimim/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,EMail,Telefon,Adres")] İletisimim İletisimim)
        {
            if (ModelState.IsValid)
            {
                _context.Add(İletisimim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(İletisimim);
        }

        // GET: Yonetici/İletisimim/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var İletisimim = await _context.İletisimims.FindAsync(id);
            if (İletisimim == null)
            {
                return NotFound();
            }
            return View(İletisimim);
        }

        // POST: Yonetici/İletisimim/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,EMail,Telefon,Adres")] İletisimim İletisimim)
        {
            if (id != İletisimim.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(İletisimim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!İletisimimExists(İletisimim.id))
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
            return View(İletisimim);
        }

        // GET: Yonetici/İletisimim/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var İletisimim = await _context.İletisimims
                .FirstOrDefaultAsync(m => m.id == id);
            if (İletisimim == null)
            {
                return NotFound();
            }

            return View(İletisimim);
        }

        // POST: Yonetici/İletisimim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var İletisimim = await _context.İletisimims.FindAsync(id);
            _context.İletisimims.Remove(İletisimim);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool İletisimimExists(int id)
        {
            return _context.İletisimims.Any(e => e.id == id);
        }
    }
}
