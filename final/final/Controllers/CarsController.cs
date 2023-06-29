using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using final.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace final.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarsContext _context;

        public CarsController(CarsContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Search(string sear)
        {
            var x =_context.Cars.Where(m => m.Car_Name.Contains(sear)).ToList();
            return View("Index",x);
        }
        [HttpPost]
        public IActionResult Search2(string sear)
        {
            var x = _context.Cars.Where(m => m.Car_Name.Contains(sear)).ToList();
            return View("catalogue", x);
        }
        public async Task<IActionResult> catalogue()
        {
            if (HttpContext.Session.GetString("userid") == null)
            {
                return RedirectToAction("Login", "UserAccounts");
            }
            else
            {
                return View(await _context.Cars.ToListAsync());
            }
        }


        // GET: Cars
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("userid")== null)
            { 
                return RedirectToAction("Login", "UserAccounts");
            }
            else
            {
                return View(await _context.Cars.ToListAsync());
            }
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("userid") == null)
            {
                return RedirectToAction("Login", "UserAccounts");
            }
            else
            {
                var cars = await _context.Cars
                .FirstOrDefaultAsync(m => m.id == id);
            return View(cars);
            }
            
        }

        // GET: Cars/Create
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
        public async Task<IActionResult> Create( IFormFile file,[Bind("id,Car_Name,Price,Color,Passenger_Capacity,Engine,Power,Automatic_Transmission,Cargo_Capacity,Acceleration,City_Fuel_Economy,High_Fuel_Economy")] Cars cars)
        {
            //to upload file
            if (file != null)
            {
                string filename = file.FileName;
                string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images"));
                using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                { await file.CopyToAsync(filestream); }

                cars.Image_File = filename;
            }
                _context.Add(cars);//add cars object to database
                await _context.SaveChangesAsync();//save the changes in database
                return RedirectToAction(nameof(Index));
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("userid") == null)
            {
                return RedirectToAction("Login", "UserAccounts");
            }
            else
            {
                var cars = await _context.Cars.FindAsync(id);
            return View(cars);
            }
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( IFormFile file, int id, [Bind("id,Car_Name,Price,Color,Passenger_Capacity,Engine,Power,Automatic_Transmission,Cargo_Capacity,Acceleration,City_Fuel_Economy,High_Fuel_Economy,Image_File")] Cars cars)
        {
            //iformfile to use external file
            if (file != null)
            {
                string filename = file.FileName;
                string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images"));
                using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                { await file.CopyToAsync(filestream); }

                cars.Image_File = filename;
            }

            _context.Update(cars);//update on database
            await _context.SaveChangesAsync();//save the change in DB                
                return RedirectToAction(nameof(Index));
        }

        // GET: Cars/Delete/5
       
        private bool CarsExists(int id)
        {
            return _context.Cars.Any(e => e.id == id);
        }
    }
}
