using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EShopMVC.Data;
using EShopMVC.Models;


namespace EShopMVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.OrderByDescending(u => u.CreatedDate).ToListAsync();
            return View(users);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Email,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Username == user.Username))
                {
                    ModelState.AddModelError("Username", "Username already exists.");
                }
                if (_context.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Email already exists.");
                }

                if (ModelState.IsValid)
                {
                    user.CreatedDate = DateTime.Now;
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "User created successfully!";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewBag.CurrentPassword = user.Password;
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Email,Password")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Username == user.Username && u.Id != user.Id))
                {
                    ModelState.AddModelError("Username", "Username already exists.");
                }
                if (_context.Users.Any(u => u.Email == user.Email && u.Id != user.Id))
                {
                    ModelState.AddModelError("Email", "Email already exists.");
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        var existingUser = await _context.Users.FindAsync(id);
                        if (existingUser == null)
                        {
                            return NotFound();
                        }

                        existingUser.Username = user.Username;
                        existingUser.Email = user.Email;

                        if (!string.IsNullOrWhiteSpace(user.Password))
                        {
                            existingUser.Password = user.Password;
                        }

                        _context.Update(existingUser);
                        await _context.SaveChangesAsync();
                        TempData["EditMessage"] = "User updated successfully!";
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UserExists(user.Id))
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
            }
            return View(user);
        }


        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                TempData["DeleteMessage"] = "User deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
