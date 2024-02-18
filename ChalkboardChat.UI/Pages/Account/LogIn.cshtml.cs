using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkboardChat.UI.Pages.Account
{
	[BindProperties]
	public class LogInModel : PageModel
	{
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly UserManager<IdentityUser> _userManager;

		public string Username { get; set; }

		public string Password { get; set; }

		public LogInModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		public void OnGet()
		{
		}

		public async Task<IActionResult> OnPost()
		{

			IdentityUser userToLogin = await _userManager.FindByNameAsync(Username);

			if (userToLogin != null)
			{
				var signInResult = await _signInManager.PasswordSignInAsync(userToLogin, Password, false, false);
				if (signInResult.Succeeded)
				{
					return RedirectToPage("/Account/WelcomeUser");
				}
				else
				{

				}
			}

			return Page();
		}

	}
}
