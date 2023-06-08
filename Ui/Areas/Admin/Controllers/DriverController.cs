using AutoMapper;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Interfaces.DriverRegister;
using Core.Domain.Entity.DriverRegister;
using Core.ViewModel.DriverRegister;
using Ui.HandShort;

namespace Ui.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DriverController : Controller
    {

        #region Constructor

        private readonly IDriver _driver;
        private readonly ISaveChangesAsync _saveChangesAsync;
        private readonly IMapper _mapper;

        public DriverController(IDriver driver, ISaveChangesAsync saveChangesAsync, IMapper mapper)
        {
            _driver = driver;
            _saveChangesAsync = saveChangesAsync;
            _mapper = mapper;
        }

        #endregion

        #region Index

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var driver = await _driver.GetAllDrivers();
            var model = _mapper.Map<List<Driver>, List<DriverViewModel>>(driver);
            return View(model);
        }

        #endregion

        #region AddDriver

        [HttpGet]
        public IActionResult AddDriver()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDriver([Bind("Name,NationalCode,PhoneNumber,Address")] DriverViewModel driverViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = Extension.AlertError();
                return View(driverViewModel);
            }

            var driver = _mapper.Map<DriverViewModel,Driver>(driverViewModel);
            await _driver.AddDriver(driver);
            await _saveChangesAsync.SaveChangesAsync();

            TempData["Message"] = Extension.AlertSuccess();
            return RedirectToAction("Index");
        }

        #endregion

        #region UpdateDriver

        [HttpGet]
        public async Task<IActionResult> UpdateDriver(string id)
        {
            var driver = await _driver.GetDriverById(id);
            var model = _mapper.Map<Driver, DriverViewModel>(driver);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateDriver([Bind("Id,Name,NationalCode,PhoneNumber,Address")] DriverViewModel driverViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = Extension.AlertError();
                return View(driverViewModel);
            }
            var driver = _mapper.Map<DriverViewModel, Driver>(driverViewModel);
            _driver.UpdateDriver(driver);
            await _saveChangesAsync.SaveChangesAsync();

            TempData["Message"] = Extension.AlertSuccess();
            return RedirectToAction("Index");
        }

        #endregion

        #region DeleteDriver

        [HttpGet]
        public async Task<IActionResult> DeleteDriver(string id)
        {
            var model = await _driver.GetDriverById(id);
            if (model is null)
            {
                TempData["Message"] = Extension.AlertError();
                return RedirectToAction("Index");
            }
            _driver.DeleteDriver(model);
            await _saveChangesAsync.SaveChangesAsync();

            TempData["Message"] = Extension.AlertSuccess();
            return RedirectToAction("Index");
        }

        #endregion

    }
}
