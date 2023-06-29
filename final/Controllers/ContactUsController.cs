using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using final.Models;
using Microsoft.AspNetCore.Http;

namespace final.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly CarsContext _context;

        public ContactUsController(CarsContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Search(string sear)
        {
            var x = _context.ContactUs.Where(m => m.Name.Contains(sear)).ToList();
            return View("Index", x);
        }
        // GET: ContactUs
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("userid") == null)
            {
                return RedirectToAction("Login", "UserAccounts");
            }
            else
            {
                return View(await _context.ContactUs.ToListAsync());
            }
            
        }      
        // GET: ContactUs/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("userid") == null)
            {
                return RedirectToAction("Login", "UserAccounts");
            }
            else
            {
                return View();
            }
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,Message,Email,Subject")] ContactUs contactUs)
        {
            
                _context.Add(contactUs);
                await _context.SaveChangesAsync();
                return RedirectToAction("catalogue", "Cars");
           
        }

       
        // GET: ContactUs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("userid") == null)
            {
                return RedirectToAction("Login", "UserAccounts");
            }
            else
            {
                var contactUs = await _context.ContactUs
                .FirstOrDefaultAsync(m => m.id == id);
            return View(contactUs);
            }
            
        }

        // POST: ContactUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactUs = await _context.ContactUs.FindAsync(id);
            _context.ContactUs.Remove(contactUs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactUsExists(int id)
        {
            return _context.ContactUs.Any(e => e.id == id);
        }
    }
}
