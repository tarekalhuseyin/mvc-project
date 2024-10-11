using Demo.DAL.Models;
using Demo.pl.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Demo.pl.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplictionUser> _userManager;
		private readonly SignInManager<ApplictionUser> _signInManager;

		public AccountController(UserManager<ApplictionUser> userManager,
            SignInManager<ApplictionUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
       public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = new ApplictionUser()
                {
                    UserName = model.Email.Split('@')[0],
                    Email = model.Email,
                    Fname = model.FName,
                    Lname = model.LName,
                    IsAgree = model.IsAgree
                };
               var Result=await _userManager.CreateAsync(User,model.Password);
                if (Result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach (var error in Result.Errors)
                    {

                        ModelState.AddModelError(string.Empty, error.Description);
                       
                    }
                } 
            }
            return View(model);
       }


        public IActionResult Login()
        {

        return View();
        }
        [HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User =await _userManager.FindByEmailAsync(model.Email);
                if (User is not null)
                {
                    var Flag = await _userManager.CheckPasswordAsync(User, model.Password);
                    if (Flag)
                    {
                      var Result= await _signInManager.PasswordSignInAsync(User, model.Password, model.RemeberMe,false);
                        if (Result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Incorrect password");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email is not exist");
                }
            }
            return View(model);
        }




	 }




}
