using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using final.Models;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace final.Controllers
{
    public class UserAccountsController : Controller
    {
        private readonly CarsContext _context;

        public UserAccountsController(CarsContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Search(string sear)
        {
            var x = _context.UserAccount.Where(m => m.email.Contains(sear)).ToList();
            return View("Index", x);
        }

        // GET: UserAccounts
        public async Task<IActionResult> Index()

        {
            if (HttpContext.Session.GetString("userid") == null)
            {
                return RedirectToAction("Login", "UserAccounts");
            }
            else
            {
                return View(await _context.UserAccount.Where(m => m.role == "customer").ToListAsync());
            }
            
        }

        
        // GET: UserAccounts/Create
        public IActionResult Create()
        {
            return View();
        }
       
        public IActionResult Logout()
        {

            HttpContext.Session.Clear();

            return RedirectToAction("Login");

        }
        //GET: UserAccounts/Login
        public IActionResult Login()
        {
            return View();
        }
        //POST: UserAccounts/Login
        [HttpPost, ActionName("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> login(string name, string password)
        {
            SqlConnection conn1 = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DELL\\Cars3.mdf;Integrated Security=True;Connect Timeout=30");
            string sql;
            sql = "SELECT * FROM UserAccount where name ='" + name + "' and  password ='" + password + "' ";
            SqlCommand comm = new SqlCommand(sql, conn1);
            conn1.Open();
            SqlDataReader reader = comm.ExecuteReader();

            if (reader.Read())
            {
                string role = (string)reader["role"];
                string id = Convert.ToString((int)reader["id"]);
                HttpContext.Session.SetString("Name", name);
                HttpContext.Session.SetString("Role", role);
                HttpContext.Session.SetString("userid", id);
                

                reader.Close();
                conn1.Close();
                if (role == "customer")
                    return RedirectToAction("catalogue", "Cars");

                else
                    return RedirectToAction("Index", "Cars");

            }
            else
            {
                ViewData["Message"] = "wrong user name password";
                return View();
            }
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,password,email")] UserAccount userAccount)
        {
            userAccount.role = "customer";


                _context.Add(userAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Login));
            
        }

        // GET: UserAccounts/Edit/5
        public async Task<IActionResult> Edit()//take id from session
        {
            if (HttpContext.Session.GetString("userid") == null)
            {
                return RedirectToAction("Login", "UserAccounts");
            }
            else
            {
                int id = Convert.ToInt32(HttpContext.Session.GetString("userid"));
            var userAccount = await _context.UserAccount.FindAsync(id);
            return View(userAccount);
            }
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("id,name,password,role,email")] UserAccount userAccount)
        {
            
                    _context.Update(userAccount);
                    await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Login));
            
        }
        private bool UserAccountExists(int id)
        {
            return _context.UserAccount.Any(e => e.id == id);
        }
    }
}
