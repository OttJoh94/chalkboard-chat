using ChalkboardChat.Data.Database;
using ChalkboardChat.Data.Models;
using ChalkboardChat.Data.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkboardChat.UI.Pages.Message
{
	public class IndexModel(AppDbContext appContext, AuthDbContext authContext, SignInManager<IdentityUser> signInManager) : PageModel
	{
		private readonly MessageRepository messageRepo = new(appContext, authContext, signInManager);
		private readonly SignInManager<IdentityUser> signInManager = signInManager;

		[BindProperty]
		public string? Message { get; set; }
		public List<MessageModel>? AllMessages { get; set; }
		[BindProperty]
		public IdentityUser? SignedInUser { get; set; }
		[BindProperty]
		public string? NewUsername { get; set; }



		public async Task OnGet()
		{
			AllMessages = await messageRepo.GetAllAsync();
			SignedInUser = await signInManager.UserManager.GetUserAsync(HttpContext.User);
		}

		public async Task<IActionResult> OnPost()
		{
			SignedInUser = await signInManager.UserManager.GetUserAsync(HttpContext.User);

			if (SignedInUser == null || Message == null) return Page();

			string username = SignedInUser.UserName!;
			string message = Message;

			MessageModel newMessage = new() { Username = username, Message = message };

			await messageRepo.AddMessageAsync(newMessage);
			AllMessages = await messageRepo.GetAllAsync();

			return Page();
		}

		public async Task<IActionResult> OnPostSignOut()
		{
			await signInManager.SignOutAsync();

			return RedirectToPage("/Account/Login");
		}

		public async Task<IActionResult> OnPostUpdateUsername()
		{
			SignedInUser = await signInManager.UserManager.GetUserAsync(HttpContext.User);

			await messageRepo.UpdateUsername(SignedInUser.UserName!, NewUsername!);
			AllMessages = await messageRepo.GetAllAsync();
			return Page();

		}

		public async Task<IActionResult> OnPostDeleteUser()
		{
			SignedInUser = await signInManager.UserManager.GetUserAsync(HttpContext.User);

			await messageRepo.DeleteUserAsync(SignedInUser.UserName!);

			await signInManager.SignOutAsync();

			return RedirectToPage("/Account/Login");
		}
	}
}
