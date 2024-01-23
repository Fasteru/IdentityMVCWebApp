using Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace IdentityMVCWebApp.Controllers
{
    [AllowAnonymous]
    public class UserAccessController : Controller
    {

        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly SignInManager<ApplicationUser> _signinmanager;

        public UserAccessController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signinmanager)
        {
            _usermanager = userManager;
            _signinmanager = signinmanager;
        }

        [Route("UserAccess/Register")]
        [HttpGet]       
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("/Register")]
        [Route("UserAccess/Register")]
        public async Task<IActionResult> Register(UserRegisterModel userRegisterModel)
        {
            if(!ModelState.IsValid)
            {

                return View("Register", userRegisterModel);
            }
            else 
            {
                ApplicationUser applicationUser = new ApplicationUser()
                {
                    Id = Guid.NewGuid(),
                    UserName = userRegisterModel.UserName,
                    Email = userRegisterModel.email                   
                };
                IdentityResult identityResult =  await _usermanager.CreateAsync(applicationUser, userRegisterModel.password);
                if (identityResult.Succeeded)
                {
                    bool isPersisted = userRegisterModel.IsChecked;
                    await _signinmanager.SignInAsync(applicationUser, isPersisted);
                    return RedirectToAction("Index", "Home");
                }
                else
                {                  
                    return View("Register");
                }
                 
            }

            
        }

        [HttpGet]
        [Route("UserAccess/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("UserAccess/Login")]
        public async Task<IActionResult> Login(UserLoginModel userLoginModel)
        {
            if (!ModelState.IsValid)
            {

                return View(userLoginModel);
            }
            else
            {
                var result = await _signinmanager.PasswordSignInAsync(userLoginModel.UserName, userLoginModel.password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(userLoginModel);
                }

            }
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signinmanager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
