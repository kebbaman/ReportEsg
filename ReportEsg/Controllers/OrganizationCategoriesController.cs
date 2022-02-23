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
    public class OrganizationCategoriesController : Controller
    {
        private readonly DatabaseContext _context;

        public OrganizationCategoriesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: OrganizationCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrganizationCategories.ToListAsync());
        }

        // GET: OrganizationCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizationCategory = await _context.OrganizationCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (organizationCategory == null)
            {
                return NotFound();
            }

            return View(organizationCategory);
        }

        // GET: OrganizationCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrganizationCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description")] OrganizationCategory organizationCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(organizationCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(organizationCategory);
        }

        // GET: OrganizationCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizationCategory = await _context.OrganizationCategories.FindAsync(id);
            if (organizationCategory == null)
            {
                return NotFound();
            }
            return View(organizationCategory);
        }

        // POST: OrganizationCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description")] OrganizationCategory organizationCategory)
        {
            if (id != organizationCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(organizationCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganizationCategoryExists(organizationCategory.Id))
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
            return View(organizationCategory);
        }

        // GET: OrganizationCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizationCategory = await _context.OrganizationCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (organizationCategory == null)
            {
                return NotFound();
            }

            return View(organizationCategory);
        }

        // POST: OrganizationCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var organizationCategory = await _context.OrganizationCategories.FindAsync(id);
            _context.OrganizationCategories.Remove(organizationCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrganizationCategoryExists(int id)
        {
            return _context.OrganizationCategories.Any(e => e.Id == id);
        }
    }
}
