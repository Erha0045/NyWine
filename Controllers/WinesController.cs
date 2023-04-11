using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWine.Data;
using NyWine.Models;

namespace NyWine.Controllers
{
    public class WinesController : Controller
    {
        private readonly MvcWineContext _context;

        public WinesController(MvcWineContext context)
        {
            _context = context;
        }

        // GET: Wines
        public async Task<IActionResult> Index()
        {
            return View(await _context.Wine.ToListAsync());                        
        }

        // GET: Wines/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wine = await _context.Wine
                .SingleOrDefaultAsync(m => m.ProductGuid == id);
            if (wine == null)
            {
                return NotFound();
            }

            return View(wine);
        }

        // GET: Wines/Create
         public IActionResult Create(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Create), new { id = Guid.NewGuid() });
            }
            return View();
        }

        // POST: Wines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid id, 
        [Bind("Id,Name,Description,Price,Origin,AlcoholPercentage,Year,Image,Size")] Wine wine)
        {
            if (ModelState.IsValid)
            {
                wine.ProductGuid = id;
                var wineId = await _context.Wine
                    .Where(m => m.ProductGuid == id)
                    .Select(m => m.WineId)
                    .SingleOrDefaultAsync();
                if (wineId == 0)
                {
                    _context.Add(wine);
                }
                else
                {
                    wine.WineId = wineId;
                    _context.Update(wine);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wine);
        }

        // GET: Wines/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var wine = await _context.Wine.SingleOrDefaultAsync(m => m.ProductGuid == id);
            if (wine == null)
            {
                return NotFound();
            }
            return View(wine);
        }

        // POST: Wines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,Price,Origin,AlcoholPercentage,Year,Image,Size")] Wine wine)
        {
           wine.ProductGuid = id;

            if (ModelState.IsValid)
            {
              var wineId = await _context.Wine
                    .Where(m => m.ProductGuid == id)
                    .Select(m => m.WineId)
                    .SingleOrDefaultAsync();
                if (wineId == 0)
                {
                    return NotFound();
                }
                else
                {
                    wine.WineId = wineId;
                    _context.Update(wine);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wine);
        }

        // GET: Wines/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wine = await _context.Wine
                .SingleOrDefaultAsync(m => m.ProductGuid == id);
            if (wine == null)
            {
                return NotFound();
            }

            return View(wine);
        }

        // POST: Wines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
           
            var wine = await _context.Wine
                .SingleOrDefaultAsync(m => m.ProductGuid == id);
            _context.Wine.Remove(wine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WineExists(int id)
        {
            return _context.Wine.Any(e => e.WineId == id);
        }
    }
}
