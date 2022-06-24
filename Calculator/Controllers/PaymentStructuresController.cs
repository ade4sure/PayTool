using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Calculator.Data;
using Calculator.Models.AppModels;

namespace Calculator.Controllers
{
    public class PaymentStructuresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentStructuresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PaymentStructures
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PaymentStructures.Include(p => p.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PaymentStructures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentStructure = await _context.PaymentStructures
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentStructure == null)
            {
                return NotFound();
            }

            return View(paymentStructure);
        }

        // GET: PaymentStructures/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.PayCategorys, "Id", "Id");
            return View();
        }

        // POST: PaymentStructures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CategoryId")] PaymentStructure paymentStructure)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentStructure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.PayCategorys, "Id", "Id", paymentStructure.CategoryId);
            return View(paymentStructure);
        }

        // GET: PaymentStructures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentStructure = await _context.PaymentStructures.FindAsync(id);
            if (paymentStructure == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.PayCategorys, "Id", "Id", paymentStructure.CategoryId);
            return View(paymentStructure);
        }

        // POST: PaymentStructures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CategoryId")] PaymentStructure paymentStructure)
        {
            if (id != paymentStructure.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentStructure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentStructureExists(paymentStructure.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.PayCategorys, "Id", "Id", paymentStructure.CategoryId);
            return View(paymentStructure);
        }

        // GET: PaymentStructures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentStructure = await _context.PaymentStructures
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentStructure == null)
            {
                return NotFound();
            }

            return View(paymentStructure);
        }

        // POST: PaymentStructures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentStructure = await _context.PaymentStructures.FindAsync(id);
            _context.PaymentStructures.Remove(paymentStructure);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentStructureExists(int id)
        {
            return _context.PaymentStructures.Any(e => e.Id == id);
        }
    }
}
