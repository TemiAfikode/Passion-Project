using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PassionProject.Models;

namespace PassionProject.Controllers
{
    public class ServicesController : Controller
    {


        private readonly UserManager<AppUsers> _userManager;
        private readonly SignInManager<AppUsers> _signInManager;
        private readonly AppDBContext _context;

        public ServicesController(AppDBContext context,UserManager<AppUsers> userManager,
                              SignInManager<AppUsers> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        //Get a list of all availble services
        public async Task<IActionResult> Index()
        {
            var applicationUser = await _userManager.GetUserAsync(User);
            var Id = applicationUser.Id;

            string userEmail = applicationUser?.Email; // will give the user's Email
            var serv = await _context.Services
                .Include(X=>X.Vendors).ToListAsync();

            return View(serv);
        }

        //Create or Edit  a service view controller endpoint
        public async Task<IActionResult> AddOrEdit(int? id)
        {
            ViewBag.PageName = id == null ? "Create Service" : "Edit Service";
            ViewBag.IsEdit = id == null ? false : true;


            ViewData["VendorId"] = new SelectList(_context.Vendors, "Id", "VendorName");
      

            if (id == null)
            {
                return View();
            }
            else
            {
                var service = await _context.Services.FindAsync(id);

                if (service == null)
                {
                    return NotFound();
                }
                return View(service);
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Create or Edit  a service data controller endpoint
        public async Task<IActionResult> AddOrEdit(int id, 
            [Bind("Id,VendorId,ServiceName,Fee")]
        Services serviceData)
        {
            bool IsVendorExist = false;

            Services servi = await _context.Services.FindAsync(id);

            if (servi != null)
            {
                IsVendorExist = true;
            }
            else
            {
                servi = new Services();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    servi.CreatedAt = DateTime.Now;
                    servi.VendorId = serviceData.VendorId;
                    servi.ServiceName = serviceData.ServiceName;
                    servi.Fee = serviceData.Fee;
                

                    if (IsVendorExist)
                    {
                        _context.Update(servi);
                    }
                    else
                    {
                        _context.Add(servi);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(serviceData);
        }

        //view a service details view controller endpoint
        public async Task<IActionResult> Details(int? serviceId)
        {
            if (serviceId == null)
            {
                return NotFound();
            }
            var serv = await _context.Services.FirstOrDefaultAsync(m => m.Id == serviceId);
            if (serv == null)
            {
                return NotFound();
            }
            return View(serv);

        }

        // GET: 
        //Delete  a service view controller endpoint
        public async Task<IActionResult> Delete(int? serviceId)
        {
            if (serviceId == null)
            {
                return NotFound();
            }
            var serv = await _context.Services.FirstOrDefaultAsync(m => m.Id == serviceId);

            if (serv == null)
            {
                return NotFound();
            }

            return View(serv);
        }

        // POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Delete  a service data controller endpoint
        public async Task<IActionResult> Delete(int Id)
        {
            var servv = await _context.Services.FindAsync(Id);
            _context.Services.Remove(servv);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

       





    }
}
