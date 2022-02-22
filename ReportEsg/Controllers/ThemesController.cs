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
    public class ThemesController : Controller
    {
        private readonly DatabaseContext _context;

        public ThemesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Themes
        public async Task<IActionResult> Index(int? applicationId)
        {
            if (applicationId == null)
            {
                return NotFound();
            }
            var databaseContext = _context.Themes.Include(t => t.Application).Include(t => t.Area);
            ViewData["ApplicationId"] = applicationId;
            return View(await databaseContext.ToListAsync());
        }



        // GET: Themes/Create
        public IActionResult Create(int? applicationId)
        {
            if (applicationId == null)
            {
                return NotFound();
            }
            ViewData["ApplicationId"] = applicationId;
            ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "Description");
            return View();
        }

        // POST: Themes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,AreaId,ApplicationId")] Theme theme, int? applicationId)
        {
            if (ModelState.IsValid)
            {
                _context.Add(theme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { applicationId = applicationId });
            }
            ViewData["ApplicationId"] = applicationId;
            ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "Description", theme.AreaId);
            return View(theme);
        }

        // GET: Themes/Edit/5
        public async Task<IActionResult> Edit(int? id, int? applicationId)
        {
            if (id == null || applicationId == null)
            {
                return NotFound();
            }

            var theme = await _context.Themes.FindAsync(id);
            if (theme == null)
            {
                return NotFound();
            }
            ViewData["ApplicationId"] = applicationId;
            ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "Description", theme.AreaId);
            return View(theme);
        }

        // POST: Themes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,AreaId,ApplicationId")] Theme theme, int? applicationId)
        {
            if (id != theme.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(theme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThemeExists(theme.Id))
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
            ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "Description", theme.AreaId);
            return View(theme);
        }

        // GET: Themes/Delete/5
        public async Task<IActionResult> Delete(int? id, int? applicationId)
        {
            if (id == null || applicationId == null)
            {
                return NotFound();
            }

            var theme = await _context.Themes
                .Include(t => t.Application)
                .Include(t => t.Area)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (theme == null)
            {
                return NotFound();
            }
            ViewData["ApplicationId"] = applicationId;
            return View(theme);
        }

        // POST: Themes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int? applicationId)
        {
            var theme = await _context.Themes.FindAsync(id);
            _context.Themes.Remove(theme);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { applicationId = applicationId });
        }

        private bool ThemeExists(int id)
        {
            return _context.Themes.Any(e => e.Id == id);
        }
    }
}
