using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace NyWine.Wines
{
    public class WinesController : Controller
    {
        private readonly WineQueries queries;
        private readonly WineCommands commands;

        public WinesController(WineQueries queries, WineCommands commands)
        {
            this.queries = queries;
            this.commands = commands;
        }

        // GET: Wines
        public async Task<IActionResult> Index()
        {
            return View(await queries.ListWines());
        }

        // GET: Wines/Details/5-sas
        public async Task<IActionResult> Details(Guid id)
        {           
            var wine = await queries.GetWine(id);
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
        [Bind("Id,Name,Description,Price,Origin,AlcoholPercentage,Year,Image,Size")] WineInfo wine)
        {
            if (ModelState.IsValid)
            {
                wine.ProductGuid = id;
                await commands.SaveWine(wine);
                return RedirectToAction(nameof(Index));
            }
            return View(wine);
        }

        // GET: Wines/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var wine = await queries.GetWine(id);
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
        public async Task<IActionResult> Edit(Guid id,
        [Bind("Id,Name,Description,Price,Origin,AlcoholPercentage,Year,Image,Size")] WineInfo wine)
        {
            if (ModelState.IsValid)
            {
                wine.ProductGuid = id;
                await commands.SaveWine(wine);
                return RedirectToAction(nameof(Index));
            }
            return View(wine);
        }

        // GET: Wines/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var wine = await queries.GetWine(id);
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
            await commands.DeleteWine(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
