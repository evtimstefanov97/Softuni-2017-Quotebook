using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuoteBook.Data;
using QuoteBook.Data.Models;
using QuoteBook.Services.InspiratorService.Models;
using QuoteBook.Services.InspiratorService;
using System.IO;

namespace QuoteBook.Controllers
{
    public class InspiratorsController : Controller
    {
        private readonly QuoteBookDbContext _context;
        private readonly IInspiratorService inspiratorService;
        public InspiratorsController(QuoteBookDbContext context, IInspiratorService inspiratorService)
        {
            _context = context;
            this.inspiratorService = inspiratorService;
        }

        // GET: Inspirators
        public async Task<IActionResult> Index()
        {
            return View(await this.inspiratorService.All());
        }

        // GET: Inspirators/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inspirator = await _context.Inspirators
                .SingleOrDefaultAsync(m => m.Id == id);
            if (inspirator == null)
            {
                return NotFound();
            }

            return View(inspirator);
        }

        // GET: Inspirators/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InspiratorCreateEditServiceModel model)
        {
            if (ModelState.IsValid)
            {
                await this.inspiratorService.CreateInspiratorAsync(model);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Index","Inspirators");
        }

        // GET: Inspirators/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inspirator = await this.inspiratorService.FindInspiratorAsync(id);
            if (inspirator == null)
            {
                return NotFound();
            }
            var model = new InspiratorCreateEditServiceModel()
            {
                Name = inspirator.Name             
            };
            return View(model);
        }

        // POST: Inspirators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id,InspiratorCreateEditServiceModel inspirator)
        {
            if (id != inspirator.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await this.inspiratorService.EditInspiratorAsync(inspirator);
              
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InspiratorExists(inspirator.Id))
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
            return View(inspirator);
        }

        // GET: Inspirators/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inspirator = await _context.Inspirators
                .SingleOrDefaultAsync(m => m.Id == id);
            if (inspirator == null)
            {
                return NotFound();
            }

            return View(inspirator);
        }

        // POST: Inspirators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var inspirator = await _context.Inspirators.SingleOrDefaultAsync(m => m.Id == id);
            _context.Inspirators.Remove(inspirator);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InspiratorExists(string id)
        {
            return _context.Inspirators.Any(e => e.Id == id);
        }
    }
}
