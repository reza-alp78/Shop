using Ui.HandShort;
using Core.IdentityEntity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Core.ViewModel.Authentication;
using System.Net.Mail;
using System.Net;
using Core.ViewModel.EmailSender;
using Microsoft.AspNetCore.Authentication;

namespace UI.Controllers
{
    public class AccountController : Controller
    {

        #region Constructor
        //sendEmail has value and in login value be null
        const string ConfirmCode = "_ConfirmCode";
        const string Email = "_Email";

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
                TempData["Message"] = Extension.AlertErrorsModel(ModelState);
                return View(loginViewModel);
            }
            var result = await _signInManager.PasswordSignInAsync
                (loginViewModel.Email, loginViewModel.Password, isPersistent: false, lockoutOnFailure: false);

            //EmailConfirm
            if (result.IsNotAllowed)
            {
                SendEmail(loginViewModel.Email);
                return RedirectToAction("ConfirmEmail");
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("ورود", "ایمیل یا رمز نادرست است");
                TempData["Message"] = Extension.AlertErrorsModel(ModelState);
                return View(loginViewModel);
            }

            TempData["Message"] = Extension.AlertSuccess();
            HttpContext.Session.SetString(ConfirmCode, "");
            HttpContext.Session.SetString(Email, "");
            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
            HttpContext.Session.SetString("UserGuid", user.Id.ToString());
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
        public async Task<IActionResult> Register([Bind("Name,LastName,Email,Phone,Password,ConfirmPassword")] RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = Extension.AlertErrorsModel(ModelState);
                return View(registerViewModel);
            }

            ApplicationUser user = new ApplicationUser()
            {
                Name = registerViewModel.Name,
                LastName = registerViewModel.LastName,
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email,
                PhoneNumber = registerViewModel.Phone,
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerViewModel.Password);
            if (!result.Succeeded)
            {
                foreach (IdentityError item in result.Errors)
                {
                    ModelState.AddModelError("Register", item.Description);
                }
                TempData["Message"] = Extension.AlertErrorsModel(ModelState);
                return View(registerViewModel);
            }
            SendEmail(user.Email);
            return RedirectToAction("ConfirmEmail");
        }

        #endregion

        #region SendEmail
        
        [HttpGet]
        public void SendEmail(string email)
        {
            try
            {
                var code = Extension.Random(100000, 999999 + 1);

                HttpContext.Session.SetString(ConfirmCode, code.ToString());
                HttpContext.Session.SetString(Email, email);

                MailMessage mail = new MailMessage("AlpShopsIran@gmail.com", email);
                mail.Subject = $"کد تایید شما: {code}";


                mail.Body = $"کد شما : {code} " + "لطفا این کد را به کسی ندهید مدت زمان این کد 2 ساعت میباشد";
                mail.IsBodyHtml = false;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;

                NetworkCredential nc = new NetworkCredential("AlpShopsIran@gmail.com", "litvuipkelgebrxt");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = nc;
                smtp.Send(mail);

                TempData["Message"] = "رمز به ایمیل شما ارسال شد";
               
            }
            catch (Exception)
            {
                TempData["Message"] = Extension.AlertUnKnown();
            }
        }

        #endregion            

        #region ConfirmEmail

        [HttpGet]
        public IActionResult ConfirmEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmEmail([Bind("SendCode")] MailSender code)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = Extension.AlertErrorsModel(ModelState);
                return View(code);
            }
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(ConfirmCode)))
            {
                TempData["Message"] = "رمز منقضی شده است";
                return RedirectToAction("Login");
            }
            else if (HttpContext.Session.GetString(ConfirmCode) != code.SendCode)
            {
                TempData["Message"] = "رمز وارد شده اشتباه است";
                return View();
            }

            var user = await _userManager.FindByEmailAsync(HttpContext.Session.GetString(Email));
            user.EmailConfirmed = true;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (IdentityError item in result.Errors)
                {
                    ModelState.AddModelError("CompleteProfile", item.Description);
                }
                TempData["Message"] = Extension.AlertErrorsModel(ModelState);
                return View();
            }
            TempData["Message"] = "ایمیل شما تایید شد";
            return RedirectToAction("Login");
        }

        #endregion      

        #region CompleteProfile

        [HttpGet("id")]
        [Authorize]
        public async Task<IActionResult> CompleteProfile(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var model = new CompleteProfileViewModel()
            {
                Name = user.Name,
                LastName = user.LastName,
                PhoneNumbe = user.PhoneNumber,
                Province = user.Province,
                City = user.City,
                Adress = user.Adress,
                ZipCode = user.ZipCode,
                Description = user.Description
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompleteProfile([Bind("Id,Name,LastName,PhoneNumbe,Province,City,Adress,ZipCode,Description")] CompleteProfileViewModel completeProfileViewModel)
        {

            if (!ModelState.IsValid)
            {
                TempData["Message"] = Extension.AlertErrorsModel(ModelState);
                return View(completeProfileViewModel);
            }
            try
            {
                var user = await _userManager.FindByIdAsync((completeProfileViewModel.Id).ToString());

                user.Name = completeProfileViewModel.Name;
                user.LastName = completeProfileViewModel.LastName;
                user.PhoneNumber = completeProfileViewModel.PhoneNumbe;
                user.Province = completeProfileViewModel.Province;
                user.City = completeProfileViewModel.City;
                user.Adress = completeProfileViewModel.Adress;
                user.ZipCode = completeProfileViewModel.ZipCode;
                user.Description = completeProfileViewModel.Description;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    foreach (IdentityError item in result.Errors)
                    {
                        ModelState.AddModelError("CompleteProfile", item.Description);
                    }
                    TempData["Message"] = Extension.AlertErrorsModel(ModelState);
                    return View(completeProfileViewModel);
                }
            }
            catch (Exception)
            {
                TempData["Message"] = Extension.AlertUnKnown();
                return View(completeProfileViewModel);
            }
            TempData["Message"] = Extension.AlertSuccess();
            return RedirectToAction("Index", "Home", new { area = "" });

        }

        #endregion

        #region Logout

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["Message"] = Extension.AlertSuccess();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        #endregion

        #region ExistEmail

        [HttpGet]
        public IActionResult ExistEmail()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExistEmail([Bind("Email")] MailSender mailSender)
        {
            if (mailSender.Email == null)
            {
                TempData["Message"] = Extension.AlertNotValue();
                return View();
            }

            var user = await _userManager.FindByEmailAsync(mailSender.Email);

            if (user == null)
            {
                TempData["Message"] = "ایمیل مورد نظر یافت نشد";
                return View(mailSender);
            }
            SendEmail(mailSender.Email);
            return RedirectToAction("ResetPassword");
        }

        #endregion

        #region ResetPassword

        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword([Bind("SendCode,Email,Password,ConfirmPassword")] MailSender mailSender)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(ConfirmCode)))
            {
                TempData["Message"] = "رمز منقضی شده است";
                return RedirectToAction("Login");
            }
            else if (HttpContext.Session.GetString(ConfirmCode) != mailSender.SendCode)
            {
                TempData["Message"] = "تایید کد وارد شده اشتباه است ایمیل خود را مجددا چک کنید";
                return View();
            }
            if (!ModelState.IsValid)
            {
                TempData["Message"] = Extension.AlertErrorsModel(ModelState);
                return View(mailSender);
            }
            var user = await _userManager.FindByEmailAsync(HttpContext.Session.GetString(Email));
            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, mailSender.Password);
            TempData["Message"] = Extension.AlertSuccess();
            return View("Login");
        }

        #endregion

    }
}