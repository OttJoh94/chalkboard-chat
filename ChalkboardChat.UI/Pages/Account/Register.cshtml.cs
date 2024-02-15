using ChalkboardChat.App;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkboardChat.UI.Pages.Account
{
	public class RegisterModel(SignInManager<IdentityUser> signInManager) : PageModel
	{
		private readonly AppUserManager userManager = new(signInManager);
		[BindProperty]
		public string? Username { get; set; }
		[BindProperty]
		public string? Password { get; set; }
		public string ErrorMessage { get; set; }
		public bool RegisterSuccess { get; set; } = false;



		public void OnGet()
		{
		}

		public async Task<IActionResult> OnPost()
		{
			bool registerSuccess = await userManager.RegisterUserAsync(Username!, Password!);
			if (registerSuccess)
			{
				//Sign in
				bool signInSuccess = await userManager.SignInUserAsync(Username!, Password!);
				if (signInSuccess)
				{
					return RedirectToPage("/Message/Index");
				}
			}

			ErrorMessage = "Couldn't register user, try something else";
			RegisterSuccess = true;

			return Page();
		}
	}
}
