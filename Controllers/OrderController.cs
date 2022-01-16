using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proje.Data;
using Proje.Models;

namespace Proje.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Order
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Order.Include(o => o.ApplicationUser).Include(o => o.Menu);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.ApplicationUser)
                .Include(o => o.Menu)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Order/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.ApplicationUser, "Id", "UserName");
            ViewData["FoodId"] = new SelectList(_context.Menu, "ID", "FoodName");
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FoodId,CustomerId,Amount,TotalPrice,OrderStatus")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.ApplicationUser, "Id", "Id", order.CustomerId);
            ViewData["FoodId"] = new SelectList(_context.Menu, "ID", "ID", order.FoodId);
            return View(order);
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.ApplicationUser, "Id", "Id", order.CustomerId);
            ViewData["FoodId"] = new SelectList(_context.Menu, "ID", "ID", order.FoodId);
            return View(order);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FoodId,CustomerId,Amount,TotalPrice,OrderStatus")] Order order)
        {
            if (id != order.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.ID))
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
            ViewData["CustomerId"] = new SelectList(_context.ApplicationUser, "Id", "Id", order.CustomerId);
            ViewData["FoodId"] = new SelectList(_context.Menu, "ID", "ID", order.FoodId);
            return View(order);
        }

        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.ApplicationUser)
                .Include(o => o.Menu)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.ID == id);
        }
        public JsonResult Order(string FoodName, string ImageUrl, int Price)

        {

            var musteriId = (from s in _context.Users
                             where s.UserName == "adem.kepce@ogr.sakarya.edu.tr"
                             select new
                             {
                                 s.Id
                             }
                              ).SingleOrDefault();

            var musteri = (from s in _context.Menu
                           where s.FoodName == FoodName
                           select new
                           {
                               s.ID,

                           }).SingleOrDefault();
            var musteritutar = (from s in _context.Order
                                where s.CustomerId == musteriId.Id
                                select
                                    s.TotalPrice

                                ).Sum();



            Order ord = new Order();
            ord.FoodId = musteri.ID;
            ord.CustomerId = musteriId.Id;
            ord.TotalPrice += Price + musteritutar;
            _context.Add(ord);
            _context.SaveChanges();
            return Json("Siparişiniz eklendi.");
            //burada gelen verileri dbye kaydettir view dönder gitsin erkan 
        }

        public JsonResult Order2(string FoodName, string ImageUrl, int Price)

        {

            var musteriId = (from s in _context.Users
                             where s.UserName == "adem.kepce@ogr.sakarya.edu.tr"
                             select new
                             {
                                 s.Id
                             }
                              ).FirstOrDefault();

            var musteri = (from s in _context.Menu
                           where s.FoodName == FoodName
                           select new
                           {
                               s.ID,

                           }).FirstOrDefault();
            var musteritutar = (from s in _context.Order
                                where s.CustomerId == musteriId.Id
                                select
                                    s.TotalPrice

                                ).Sum();

            var promationalPrice = (from s in _context.Discount
                                where s.FoodId == musteri.ID
                                select new
                                {
                                    s.PromotionalPrice

                                }).FirstOrDefault();



            Order ord = new Order();

            ord.FoodId = musteri.ID;
            ord.CustomerId = musteriId.Id;
            ord.TotalPrice = Convert.ToInt32(promationalPrice.PromotionalPrice);
            _context.Add(ord);
            _context.SaveChanges();
            return Json("Siparişiniz eklendi.");
            //burada gelen verileri dbye kaydettir view dönder gitsin erkan 
        }
    }
}
