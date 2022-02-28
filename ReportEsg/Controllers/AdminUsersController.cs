using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportEsg.Data;
using ReportEsg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEsg.Controllers
{
    public class AdminUsersController : Controller
    {
        private readonly DatabaseContext _context;

        public AdminUsersController(DatabaseContext context)
        {
            _context = context;
        }
        //[Authorize(Roles = "Admin,Consulente")]
        // GET: AdminUsers
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.AdminUsers.Include(a => a.Role);
            return View(await databaseContext.ToListAsync());
        }

        // GET: AdminUsers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminUser = await _context.AdminUsers
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.Username == id);
            if (adminUser == null)
            {
                return NotFound();
            }

            return View(adminUser);
        }

        // GET: AdminUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,Password,RePassword")] AdminCreateViewModel newAdminUser)
        {
            if (ModelState.IsValid)
            {
                var crypto = new SimpleCrypto.PBKDF2();
                AdminUser adminUser = new AdminUser();
                adminUser.Username = newAdminUser.Username;
                adminUser.Email = newAdminUser.Email;
                adminUser.Password = crypto.Compute(newAdminUser.Password);
                adminUser.PasswordSalt = crypto.Salt;
                adminUser.RoleId = _context.Roles.FirstOrDefault(r => r.Description == "Admin").Id;
                _context.Add(adminUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newAdminUser);
        }

        // GET: AdminUsers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminUser = await _context.AdminUsers.FindAsync(id);

            AdminCreateViewModel editingUser = new AdminCreateViewModel();
            editingUser.Email = adminUser.Email;
            editingUser.Password = adminUser.Password;


            if (adminUser == null)
            {
                return NotFound();
            }
            return View(editingUser);
        }

        // POST: AdminUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Email,Password,RePassword")] AdminCreateViewModel editingUser)
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
                    AdminUser adminUser = await _context.AdminUsers.FindAsync(id);
                    adminUser.Username = editingUser.Username;
                    if (!string.IsNullOrEmpty(editingUser.Password))
                    {
                        adminUser.Password = crypto.Compute(editingUser.Password);
                        adminUser.PasswordSalt = crypto.Salt;
                    }
                    _context.Update(adminUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminUserExists(editingUser.Username))
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

        // GET: AdminUsers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminUser = await _context.AdminUsers
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.Username == id);
            if (adminUser == null)
            {
                return NotFound();
            }

            return View(adminUser);
        }

        // POST: AdminUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var adminUser = await _context.AdminUsers.FindAsync(id);
            _context.AdminUsers.Remove(adminUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminUserExists(string id)
        {
            return _context.AdminUsers.Any(e => e.Username == id);
        }
    }
}
