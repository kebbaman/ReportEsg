using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReportEsg.Data;
using ReportEsg.Models;

namespace ReportEsg.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ConsultantUsersController : Controller
    {
        private readonly DatabaseContext _context;

        public ConsultantUsersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: ConsultantUsers
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.ConsultantUsers.Include(c => c.Role);
            return View(await databaseContext.ToListAsync());
        }

        // GET: ConsultantUsers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultantUser = await _context.ConsultantUsers
                .Include(c => c.Role)
                .FirstOrDefaultAsync(m => m.Username == id);
            if (consultantUser == null)
            {
                return NotFound();
            }

            return View(consultantUser);
        }

        // GET: ConsultantUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ConsultantUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,Password,RePassword")] ConsultantViewModel consultantViewModel)
        {
            if (ModelState.IsValid)
            {
                var crypto = new SimpleCrypto.PBKDF2();
                ConsultantUser consultantUser = new ConsultantUser();
                consultantUser.Username = consultantViewModel.Username;
                consultantUser.Email = consultantViewModel.Email;
                consultantUser.FirstName = consultantViewModel.FirstName;
                consultantUser.LastName = consultantViewModel.LastName;
                consultantUser.Password = crypto.Compute(consultantViewModel.Password);
                consultantUser.PasswordSalt = crypto.Salt;
                consultantUser.RoleId = _context.Roles.FirstOrDefault(r => r.Description == "Consulente").Id;
                _context.Add(consultantUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(consultantViewModel);
        }

        // GET: ConsultantUsers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultantUser = await _context.ConsultantUsers.FindAsync(id);

            if (consultantUser == null)
            {
                return NotFound();
            }

            ConsultantViewModel editingUser = new ConsultantViewModel();
            editingUser.Email = consultantUser.Email;
            editingUser.Password = consultantUser.Password;
            editingUser.LastName = consultantUser.LastName;
            editingUser.FirstName = consultantUser.FirstName;

            return View(editingUser);
        }

        // POST: ConsultantUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FirstName,LastName,Email,Password,RePassword")] ConsultantViewModel editingUser)
        {
            if (id != editingUser.Username)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var crypto = new SimpleCrypto.PBKDF2();
                try
                {
                    ConsultantUser consultantUser = await _context.ConsultantUsers.FindAsync(id);
                    consultantUser.Username = editingUser.Username;
                    consultantUser.FirstName = editingUser.FirstName;
                    consultantUser.LastName = editingUser.LastName;

                    if (!string.IsNullOrEmpty(editingUser.Password))
                    {
                        consultantUser.Password = crypto.Compute(editingUser.Password);
                        consultantUser.PasswordSalt = crypto.Salt;
                    }
                    _context.Update(consultantUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultantUserExists(editingUser.Username))
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
            return View(editingUser);
        }

        // GET: ConsultantUsers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultantUser = await _context.ConsultantUsers
                .Include(c => c.Role)
                .FirstOrDefaultAsync(m => m.Username == id);
            if (consultantUser == null)
            {
                return NotFound();
            }

            return View(consultantUser);
        }

        // POST: ConsultantUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var consultantUser = await _context.ConsultantUsers.FindAsync(id);
            _context.ConsultantUsers.Remove(consultantUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultantUserExists(string id)
        {
            return _context.ConsultantUsers.Any(e => e.Username == id);
        }
    }
}
