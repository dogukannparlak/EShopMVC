using Microsoft.AspNetCore.Mvc;
using EShopMVC.Data;
using EShopMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace EShopMVC.Controllers
{
    public class CreditCardsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CreditCardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CreditCards
        public async Task<IActionResult> Index()
        {
            var creditCards = _context.CreditCards.OrderByDescending(c => c.CreatedDate);
            return View(await creditCards.ToListAsync());
        }
        // GET: CreditCards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditCard = await _context.CreditCards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (creditCard == null)
            {
                return NotFound();
            }

            return View(creditCard);
        }

        // GET: CreditCards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CreditCards/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CardNumber,CardHolderName,ExpirationDate,CVV")] CreditCard creditCard)
        {
            if (ModelState.IsValid)
            {
                creditCard.CreatedDate = DateTime.Now;
                _context.Add(creditCard);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Credit Card created successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(creditCard);
        }

        // GET: CreditCards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditCard = await _context.CreditCards.FindAsync(id);
            if (creditCard == null)
            {
                return NotFound();
            }
            return View(creditCard);
        }

        // POST: CreditCards/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CardNumber,CardHolderName,ExpirationDate,CVV")] CreditCard creditCard)
        {
            if (id != creditCard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    creditCard.CreatedDate = DateTime.Now;
                    _context.Update(creditCard);
                    await _context.SaveChangesAsync();
                    TempData["EditMessage"] = "Credit Card Updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CreditCardExists(creditCard.Id))
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
            return View(creditCard);
        }

        // GET: CreditCards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditCard = await _context.CreditCards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (creditCard == null)
            {
                return NotFound();
            }

            return View(creditCard);
        }

        // POST: CreditCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var creditCard = await _context.CreditCards.FindAsync(id);
            _context.CreditCards.Remove(creditCard);
            await _context.SaveChangesAsync();
            TempData["DeleteMessage"] = "Credit Card Deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        private bool CreditCardExists(int id)
        {
            return _context.CreditCards.Any(e => e.Id == id);
        }
    }
}
