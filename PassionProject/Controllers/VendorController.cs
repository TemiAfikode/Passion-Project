using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassionProject.Models;

namespace PassionProject.Controllers
{
    public class VendorController : Controller
    {


        private readonly UserManager<AppUsers> _userManager;
        private readonly SignInManager<AppUsers> _signInManager;
        private readonly AppDBContext _context;

        public VendorController(AppDBContext context,UserManager<AppUsers> userManager,
                              SignInManager<AppUsers> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        //get a list of all availble vendors
        public async Task<IActionResult> Index()
        {
            var applicationUser = await _userManager.GetUserAsync(User);
            var Id = applicationUser.Id;

            string userEmail = applicationUser?.Email; // will give the user's Email
            var vendors = await _context.Vendors.ToListAsync();

            return View(vendors);
        }

        //Create or Edit  a service view controller endpoint
        public async Task<IActionResult> AddOrEdit(int? id)
        {
            ViewBag.PageName = id == null ? "Create Vendor" : "Edit Vendor";
            ViewBag.IsEdit = id == null ? false : true;
            if (id == null)
            {
                return View();
            }
            else
            {
                var vendo = await _context.Vendors.FindAsync(id);

                if (vendo == null)
                {
                    return NotFound();
                }
                return View(vendo);
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Create or Edit  a service data controller endpoint
        public async Task<IActionResult> AddOrEdit(int id, 
            [Bind("Id,VendorName,Emailaddress,VendorDescription")]
        Vendors vendorData)
        {
            bool IsVendorExist = false;

            Vendors vend = await _context.Vendors.FindAsync(id);

            if (vend != null)
            {
                IsVendorExist = true;
            }
            else
            {
                vend = new Vendors();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    vend.CreatedAt = DateTime.Now;
                    vend.Emailaddress = vendorData.Emailaddress;
                    vend.VendorDescription = vendorData.VendorDescription;
                    vend.VendorName = vendorData.VendorName;
                

                    if (IsVendorExist)
                    {
                        _context.Update(vend);
                    }
                    else
                    {
                        _context.Add(vend);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            //}
            //return View(vendorData);
        }

        //get the details of a vendor
        public async Task<IActionResult> Details(int? vendorId)
        {
            if (vendorId == null)
            {
                return NotFound();
            }
            var vend = await _context.Vendors.FirstOrDefaultAsync(m => m.Id == vendorId);
            if (vend == null)
            {
                return NotFound();
            }
            return View(vend);

        }

        // GET: 
        //Delete a service view controller endpoint
        public async Task<IActionResult> Delete(int? vendorId)
        {
            if (vendorId == null)
            {
                return NotFound();
            }
            var vend = await _context.Vendors.FirstOrDefaultAsync(m => m.Id == vendorId);

            if (vend == null)
            {
                return NotFound();
            }

            return View(vend);
        }

        // POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Delete a service data controller endpoint
        public async Task<IActionResult> Delete(int Id)
        {
            var vend = await _context.Vendors.FindAsync(Id);
            _context.Vendors.Remove(vend);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

       





    }
}
