using Ui.HandShort;
using Core.IdentityEntity;
using Core.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {

        #region Constructor

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #endregion

        #region Login

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = Extension.ErrorsModel(ModelState);
                return View(loginViewModel);
            }

            var result = await _signInManager.PasswordSignInAsync
                (loginViewModel.Email, loginViewModel.Password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("Login", "ایمیل یا رمز نادرست است");
                return View(loginViewModel);
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        #endregion

        #region Register

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("PersonName,Email,Phone,Password,ConfirmPassword")] RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = Extension.ErrorsModel(ModelState);
                return View(registerViewModel);
            }

            ApplicationUser user = new ApplicationUser()
            {
                PersonName = registerViewModel.PersonName,
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email,
                PhoneNumber = registerViewModel.Phone
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerViewModel.Password);
            if (!result.Succeeded)
            {
                foreach (IdentityError item in result.Errors)
                {
                    ModelState.AddModelError("Register", item.Description);
                }
                var errors = Extension.ErrorsModel(ModelState);
                return View(registerViewModel);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        #endregion

        #region Logout

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        #endregion

    }
}