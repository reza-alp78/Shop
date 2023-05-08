using Core.IdentityEntity;
using Core.ViewModel.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ui.HandShort;

namespace Ui.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {

        #region Constructor

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RolesController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        #endregion

        #region RoleUser

        #region AddRoleUser

        [HttpGet]
        public async Task<IActionResult> AddRoleUser()
        {
            var models = new Roles();
            var roles = await _roleManager.Roles.ToListAsync();
            foreach (var item in roles)
            {
                models.SelectRoles.Add(new SelectListItem()
                {
                    Text = item.Name,
                    Value = item.Name
                });
            }

            return View(models);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRoleUser([Bind("Email,RoleName")] Roles roles)
        {
            if (string.IsNullOrEmpty(roles.Email))
            {
                TempData["Message"] = Extension.AlertNotValue();
                return RedirectToAction("AddRoleUser");
            }

            var user = await _userManager.FindByEmailAsync(roles.Email);
            if (user == null)
            {
                TempData["Message"] = Extension.AlertNotFound();
                return RedirectToAction("AddRoleUser");
            }

            var ExistUser = await _userManager.IsInRoleAsync(user, roles.RoleName);
            if (ExistUser)
            {
                TempData["Message"] = Extension.AlertDuplicate();
                return RedirectToAction("AddRoleUser");
            }
            var result = await _userManager.AddToRoleAsync(user, roles.RoleName);
            if (!result.Succeeded)
            {
                TempData["Message"] = Extension.AlertUnKnown();
                return RedirectToAction("AddRoleUser");
            }
            TempData["Message"] = Extension.AlertSuccess();
            return RedirectToAction("Index", "Home", new {Areas = "Admin"});
        }

        #endregion

        #region DeleteRoleUser

        [HttpGet]
        public async Task<IActionResult> DeleteRoleUser([Bind("Email")] Roles roles)
        {
            if (string.IsNullOrEmpty(roles.Email))
            {
                return View();
            }

            var user = await _userManager.FindByEmailAsync(roles.Email);
            if (user == null)
            {
                TempData["Message"] = Extension.AlertNotFound();
                return View();
            }

            var UserRoles = await _userManager.GetRolesAsync(user);
            if (UserRoles.Count() == 0)
            {
                TempData["Message"] = "کاربر مورد نظر نقشی ندارد";
                return View();
            }

            //در صورتی که این گزینه انتخاب شود یعنی تمام نقش هایش پاک شود
            roles.SelectRoles.Add(new SelectListItem()
            {
                Text = "حذف تمامی نقش این کاربر",
                Value = "DeleteAllRole"
            });
            foreach (var item in UserRoles)
            {
                roles.SelectRoles.Add(new SelectListItem()
                {
                    Text = item,
                    Value = item
                });
            }

            return View(roles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRoleUserPost([Bind("Email,RoleName")] Roles roles)
        {
            var user = await _userManager.FindByEmailAsync(roles.Email);

            //means remove all roles
            if (roles.RoleName == "DeleteAllRole")
            {
                var AllRoles = await _userManager.GetRolesAsync(user);
                var results = await _userManager.RemoveFromRolesAsync(user, AllRoles.ToArray());
                if (!results.Succeeded)
                {
                    TempData["Message"] = Extension.AlertUnKnown();
                    return RedirectToAction("DeleteRoleUser");
                }

                TempData["Message"] = Extension.AlertSuccess();
                return RedirectToAction("Index", "Home", new {Areas = "Admin"});
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roles.RoleName);
            if (!result.Succeeded)
            {
                TempData["Message"] = Extension.AlertUnKnown();
                return RedirectToAction("DeleteRoleUser");
            }

            TempData["Message"] = Extension.AlertSuccess();
            return RedirectToAction("Index", "Home", new {Areas = "Admin"});
        }

        #endregion

        #endregion

        #region Role

        #region CreateRole

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole([Bind("RoleName")] Roles roles)
        {
            if (string.IsNullOrEmpty(roles.RoleName))
            {
                TempData["Message"] = Extension.AlertNotValue();
                return View(roles);
            }

            var ExistRole = await _roleManager.RoleExistsAsync(roles.RoleName);
            if (ExistRole)
            {
                TempData["Message"] = Extension.AlertDuplicate();
                return View(roles);
            }

            var role = new ApplicationRole() { Name = roles.RoleName };
            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                TempData["Message"] = Extension.AlertUnKnown();
                return View(roles);
            }

            TempData["Message"] = Extension.AlertSuccess();
            return RedirectToAction("Index", "Home", new {Areas = "Admin"});
        }

        #endregion

        #region DeleteRole

        [HttpGet]
        public async Task<IActionResult> DeleteRole()
        {
            var models = new Roles();
            var roles = await _roleManager.Roles.ToListAsync();
            roles.Reverse();
            foreach (var item in roles)
            {
                models.SelectRoles.Add(new SelectListItem()
                {
                    Text = item.Name,
                    Value = item.Name
                });
            }

            return View(models);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRole([Bind("RoleName")] Roles roles)
        {

            var user = await _roleManager.FindByNameAsync(roles.RoleName);
            var result = await _roleManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                TempData["Message"] = Extension.AlertNotFound();
                return RedirectToAction("DeleteRole");
            }

            TempData["Message"] = Extension.AlertSuccess();
            return RedirectToAction("Index", "Home", new {Areas = "Admin"});
        }

        #endregion

        #endregion

    }
}
