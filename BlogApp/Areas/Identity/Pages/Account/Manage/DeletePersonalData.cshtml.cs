using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BusinessLogicLibrary.PostBLL;
using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BlogApp.Areas.Identity.Pages.Account.Manage
{
    public class DeletePersonalDataModel : PageModel
    {
        private readonly UserManager<BlogAppUser> _userManager;
        private readonly SignInManager<BlogAppUser> _signInManager;
        private readonly ILogger<DeletePersonalDataModel> _logger;
        private readonly IPostBLL _postBLL;

        public DeletePersonalDataModel(
            UserManager<BlogAppUser> userManager,
            SignInManager<BlogAppUser> signInManager,
            ILogger<DeletePersonalDataModel> logger,
            IPostBLL postBLL)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _postBLL = postBLL;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public bool RequirePassword { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Nie można znaleźć użytkownika z ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Nie można znaleźć użytkownika z ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            if (RequirePassword)
            {
                if (!await _userManager.CheckPasswordAsync(user, Input.Password))
                {
                    ModelState.AddModelError(string.Empty, "Błędne hasło.");
                    return Page();
                }
            }
            
            var userId = await _userManager.GetUserIdAsync(user);
            _postBLL.DeleteUserPosts(userId);
            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Nastąpił niespodziewany błąd podczas usuwania użytkownika z ID '{userId}'.");
            }

            await _signInManager.SignOutAsync();

            _logger.LogInformation("Użytkownik z ID '{UserId}' został usunięty.", userId);

            return Redirect("~/");
        }
    }
}
