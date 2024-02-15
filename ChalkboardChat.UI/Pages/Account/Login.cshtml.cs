using ChalkboardChat.App;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkboardChat.UI.Pages.Account
{
	[BindProperties]
	public class LoginModel(SignInManager<IdentityUser> signInManager) : PageModel
	{
		private readonly AppUserManager userManager = new(signInManager);

		public string? Username { get; set; }
		public string? Password { get; set; }

		public void OnGet()
		{
		}

		public async Task<IActionResult> OnPost()
		{
			bool successfullSignIn = await userManager.SignInUserAsync(Username!, Password!);

			if (successfullSignIn)
			{
				return RedirectToPage("/Message/Index");
			}

			return Page();
		}
	}
}
