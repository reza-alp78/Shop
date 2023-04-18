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

        //private readonly UserManager<ApplicationUser> _userManager;
        //public AccountController(UserManager<ApplicationUser> userManager)
        //{
        //    _userManager = userManager;
        //}

        #endregion

        #region Register

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register([Bind("UserName,Email,Phone,Password,ConfirmPassword")] RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = Extension.ErrorsModel(ModelState);
                return View(registerViewModel);
            }
            ApplicationUser user = new ApplicationUser()
            {
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email,
                PhoneNumber = registerViewModel.Phone
            };
            //IdentityResult result;= await _userManager.CreateAsync(user, registerViewModel.Password);
            if (1 == 1/*result.Succeeded*/)
            {
                return View(); //redirect
            }
            else
            {
                //foreach (IdentityError item in result.Errors)
                //{
                //    ModelState.AddModelError("Register", item.Description);
                //}
                //return View(registerViewModel);
            }
        }

        #endregion
    }
}