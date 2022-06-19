using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassionProject.Models;

namespace PassionProject.Controllers
{
    public class UserController : Controller
    {


        private readonly UserManager<AppUsers> _userManager;
        private readonly SignInManager<AppUsers> _signInManager;
        private readonly AppDBContext _context;

        public UserController(AppDBContext context,UserManager<AppUsers> userManager,
                              SignInManager<AppUsers> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
      

        //Returns a list of users
        public async Task<IActionResult> Index()
        {
            var allUsers = await _userManager.Users.ToListAsync();
            return View(allUsers);
        }

       





    }
}
