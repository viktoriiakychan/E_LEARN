using _18_E_LEARN.BusinessLogic.Services;
using _18_E_LEARN.DataAccess.Data.Context;
using _18_E_LEARN.DataAccess.Data.Models.Categories;
using _18_E_LEARN.DataAccess.Data.Models.User;
using _18_E_LEARN.DataAccess.Data.ViewModels.User;
using _18_E_LEARN.DataAccess.Validation.Categories;
using _18_E_LEARN.DataAccess.Validation.User;
using AutoMapper;
using FluentValidation;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace _18_E_LEARN.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserService _userService;
        private readonly CategoryService _categoryService;

        public AdminController(UserService userService, IMapper mapper, CategoryService categoryService)
        {
            _userService = userService;
            _mapper = mapper;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Users()
        {
            var user = await _userService.GetAllUsers();
            if (user.Success)
            {
                return View(user.Payload);
            }
            return View();
        }
        public async Task<IActionResult> Categories()
        {
            var result = await _categoryService.GetAllAsync();
            return View(result.Payload);
        }

        public async Task<IActionResult> Profile()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userService.GetUserProfileAsync(userId);
            if (user.Success)
            {
                return View(user.Payload);
            }
            return View();
        }
        [AllowAnonymous]
        //[HttpGet]
        public IActionResult SignIn()
        {
            var user = HttpContext.User.Identity.IsAuthenticated;
            if (user)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignIn(LoginUserVM model)
        {
            var validator = new LoginUserValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);
                if (result.Success)
                {
                    return RedirectToAction("Index", "Admin");
                }
                // write code
                ViewBag.AuthError = result.Message;
                return View(model); // error
            }
            return View(model);
        }

        [AllowAnonymous]
        //[HttpGet]
        public IActionResult SignUp()
        {
            var user = HttpContext.User.Identity.IsAuthenticated;
            if (user)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterUserVM model)
        {
            var validator = new RegisterUserValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                var result = await _userService.RegisterUserAsync(model);
                if (result.Success)
                {
                    return RedirectToAction("SignIn", "Admin");
                }
                ViewBag.AuthError = result.Message;
                return View(model); // error
            }
            ViewBag.AuthError = validationResult.Errors.First();
            return View(model);

        }
        public async Task<IActionResult> LogOut()
        {
            //_signInManager.SignOutAsync();
            //return RedirectToAction("Index", "Home");
            var result = await _userService.LogoutUserAsync();
            if (result.Success)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var result = await _userService.ConfirmEmailAsync(userId, token);
            if (result.Success)
            {
                return RedirectToAction("ConfirmEmailPage", "Admin");
            }
            return View();
        }

        [AllowAnonymous]
        public IActionResult ConfirmEmailPage()
        {
            return View();
        }

        [HttpPost]
        // [ValidateAntiForgeryToken] // secure 
        public async Task<IActionResult> UserSettings(UpdateProfileVM model)
        {
            var validator = new UpdateProfileValidation();
            var validationresult = await validator.ValidateAsync(model);
            if (validationresult.IsValid)
            {
                var result = await _userService.UpdateProfileAsync(model);
                if (result.Success)
                {
                    return RedirectToAction("SignIn", "Admin");
                }
                ViewBag.AuthError = result.Message;
                return View(model);

            }
            return View(model);
        }

        public async Task<IActionResult> UserSettings()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _userService.GetUserForSettingsAsync(userId);
            if (result.Success)
            {
                return View(result.Payload);
            }
            return View();
        }
        public async Task<IActionResult> EditUser(string id)
        {
            var result = await _userService.GetUserIdForEditingAsync(id);
            if (result.Success)
            {
                return View(result.Payload);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserVM model)
        {
            var validator = new EditUserValidation();
            var validationresult = await validator.ValidateAsync(model);
            if (validationresult.IsValid)
            {
                var result = await _userService.EditUserAsync(model);
                if (result.Success)
                {
                    return RedirectToAction("Users", "Admin");
                }
                ViewBag.AuthError = result.Message;
                return View(model);

            }
            return View(model);
        }

        public async Task<IActionResult> EditCategory(int id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            if (result.Success)
            {
                return View(result.Payload);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditCategory(Category model)
        {
            var validator = new EditCategoryValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                _categoryService.EditCategoryAsync(model);
                return RedirectToAction("Categories", "Admin");
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCategory(Category model)
        {
            _categoryService.DeleteIdAsync(model.Id);
            return RedirectToAction("Categories", "Admin");

        }
        public async Task<IActionResult> AddCategory(int id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            if (result.Success)
            {
                return View(result.Payload);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(Category model)
        {
            var validator = new EditCategoryValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                _categoryService.AddCategoryAsync(model);
                return RedirectToAction("Categories", "Admin");
            }
            return View(model);
        }

    }
}