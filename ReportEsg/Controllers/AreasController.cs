using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReportEsg.Data;
using ReportEsg.Models;

namespace ReportEsg.Controllers
{
    public class AreasController : Controller
    {
        private readonly DatabaseContext _context;

        public AreasController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Areas
        public async Task<IActionResult> Index(int? applicationId)
        {
            if (applicationId == null)
            {
                return NotFound();
            }
            var databaseContext = _context.Areas.Include(a => a.Application).Include(a => a.Pillar);
            ViewData["ApplicationId"] = applicationId;
            return View(await databaseContext.ToListAsync());
        }

        // GET: Areas/Create
        public IActionResult Create(int? applicationId)
        {
            if (applicationId == null)
            {
                return NotFound();
            }
            ViewBag.ApplicationId = applicationId;
            ViewData["PillarId"] = new SelectList(_context.Pillars, "Id", "Description");
            return View();
        }

        // POST: Areas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,PillarId,ApplicationId")] Area area, int? applicationId)
        {
            if (ModelState.IsValid)
            {
                _context.Add(area);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { applicationId = applicationId });
            }
            ViewData["ApplicationId"] = applicationId;
            ViewData["PillarId"] = new SelectList(_context.Pillars, "Id", "Description", area.PillarId);
            return View(area);
        }

        // GET: Areas/Edit/5
        public async Task<IActionResult> Edit(int? id, int? applicationId)
        {
            if (id == null || applicationId == null)
            {
                return NotFound();
            }

            var area = await _context.Areas.FindAsync(id);
            if (area == null)
            {
                return NotFound();
            }
            ViewData["ApplicationId"] = applicationId;
            ViewData["PillarId"] = new SelectList(_context.Pillars, "Id", "Description", area.PillarId);
            return View(area);
        }

        // POST: Areas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,PillarId,ApplicationId")] Area area, int? applicationId)
        {
            if (id != area.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(area);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AreaExists(area.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { applicationId = applicationId });
            }
            ViewData["ApplicationId"] = applicationId;
            ViewData["PillarId"] = new SelectList(_context.Pillars, "Id", "Description", area.PillarId);
            return View(area);
        }

        // GET: Areas/Delete/5
        public async Task<IActionResult> Delete(int? id, int? applicationId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await _context.Areas
                .Include(a => a.Application)
                .Include(a => a.Pillar)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (area == null)
            {
                return NotFound();
            }
            ViewData["ApplicationId"] = applicationId;
            return View(area);
        }

        // POST: Areas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int? applicationId)
        {
            var area = await _context.Areas.FindAsync(id);
            _context.Areas.Remove(area);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { applicationId = applicationId });
        }

        private bool AreaExists(int id)
        {
            return _context.Areas.Any(e => e.Id == id);
        }
    }
}
