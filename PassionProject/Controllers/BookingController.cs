using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PassionProject.Models;

namespace PassionProject.Controllers
{
    public class BookingController : Controller
    {


        private readonly UserManager<AppUsers> _userManager;
        private readonly SignInManager<AppUsers> _signInManager;
        private readonly AppDBContext _context;

        public BookingController(AppDBContext context,UserManager<AppUsers> userManager,
                              SignInManager<AppUsers> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        //Get List of all available bookings
        public async Task<IActionResult> Index()
        {
         
            var bookings = await _context.Bookings
                .Include(x=>x.Vendors)
                .ToListAsync();

            return View(bookings);
        }

        //Create or Edit  a booking view controller endpoint
        public async Task<IActionResult> AddOrEdit(int? id)
        {
            ViewBag.PageName = id == null ? "Create Booking" : "Edit Booking";
            ViewBag.IsEdit = id == null ? false : true;

            ViewData["VendorId"] = new SelectList(_context.Vendors, "Id", "VendorName");

            if (id == null)
            {
                return View();
            }
            else
            {
                var bk = await _context.Bookings.FindAsync(id);

                if (bk == null)
                {
                    return NotFound();
                }
                return View(bk);
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Create or Edit  a booking data controller endpoint
        public async Task<IActionResult> AddOrEdit(int id, 
            [Bind("Id,VendorId,CreatedAt")]
        Bookings bkData)
        {
            bool IsVendorExist = false;

            Bookings book = await _context.Bookings.FindAsync(id);

            if (book != null)
            {
                IsVendorExist = true;
            }
            else
            {
                book = new Bookings();
            }

            //if (ModelState.IsValid)
            //{
                try
                {

                var applicationUser = await _userManager.GetUserAsync(User);
                var UId = applicationUser.Id;
                //book.User.Id = UId;
                //book.UserId= Guid.Parse( UId);
                book.UserId = applicationUser.Email;
                book.CreatedAt = bkData.CreatedAt;
                book.VendorId = bkData.VendorId;
                  
                

                    if (IsVendorExist)
                    {
                        _context.Update(book);
                    }
                    else
                    {
                        _context.Add(book);
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


        public async Task<IActionResult> Details(int? bookingId)
        {
            if (bookingId == null)
            {
                return NotFound();
            }
            var bookings = await _context.Bookings
                .Include(x=>x.Vendors)
                .FirstOrDefaultAsync(m => m.Id == bookingId);
            if (bookings == null)
            {
                return NotFound();
            }
            return View(bookings);

        }

        // GET: 
        //Delete a booking view controller endpoint
        public async Task<IActionResult> Delete(int? bookingId)
        {
            if (bookingId == null)
            {
                return NotFound();
            }
            var book = await _context.Bookings
                .Include(x=>x.Vendors)
                .FirstOrDefaultAsync(m => m.Id == bookingId);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST:
        //Delete a booking data controller endpoint
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            var book = await _context.Bookings.FindAsync(Id);
            _context.Bookings.Remove(book);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

       





    }
}
