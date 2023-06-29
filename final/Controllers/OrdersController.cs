using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using final.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

namespace final.Controllers
{
    public class OrdersController : Controller
    {
        private readonly CarsContext _context;

        public OrdersController(CarsContext context)
        {
            _context = context;
        }
        // GET: Orders
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("userid") == null)
            {
                return RedirectToAction("Login", "UserAccounts");
            }
            else
            {
               var order = await _context.Order.Include(l => l.car).ToListAsync();

               order = await _context.Order.Include(l => l.user).ToListAsync();


            return View(order) ;
            }
            
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("userid") == null)
            {
                return RedirectToAction("Login", "UserAccounts");
            }
            else
            {
             var order = await _context.Order.Include(l=>l.car)
                .FirstOrDefaultAsync(m => m.id == id);
             order = await _context.Order.Include(l => l.user)
                .FirstOrDefaultAsync(m => m.id == id);
            return View(order);
            }

        }

        // GET: Orders/Create
        public async Task<IActionResult> Create(int? id)
        {
            if (HttpContext.Session.GetString("userid") == null)
            {
                return RedirectToAction("Login", "UserAccounts");
            }
            else
            {
                 var Cars = await _context.Cars.FindAsync(id);

            return View(Cars);
            }
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int carid, int quantity)
        {
            Order orders = new Order();
            Cars car1= _context.Cars.Find(carid);
            orders.car = car1;
            
            orders.quantity = quantity;
            int uid = int.Parse(HttpContext.Session.GetString("userid"));
            UserAccount user1 = _context.UserAccount.Find(uid);
            orders.user = user1; 
            orders.dateTime = DateTime.Today;

            
                _context.Add(orders);
                await _context.SaveChangesAsync();
                return RedirectToAction("catalogue", "Cars");
            
        }
        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.id == id);
        }
    }
}
