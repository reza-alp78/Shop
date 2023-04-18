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
        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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
            return View(); //redirect
        }

        #endregion

    }
}