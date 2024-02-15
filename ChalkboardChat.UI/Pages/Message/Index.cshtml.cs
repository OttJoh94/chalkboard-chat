using ChalkboardChat.Data.Database;
using ChalkboardChat.Data.Models;
using ChalkboardChat.Data.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkboardChat.UI.Pages.Message
{
	public class IndexModel(AppDbContext context, SignInManager<IdentityUser> signInManager) : PageModel
	{
		private readonly MessageRepository messageRepo = new(context);
		private readonly SignInManager<IdentityUser> signInManager = signInManager;

		[BindProperty]
		public string? Message { get; set; }
		public List<MessageModel>? AllMessages { get; set; }
		[BindProperty]
		public IdentityUser? SignedInUser { get; set; }



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

			return Page();
		}

		public async Task<IActionResult> OnPostSignOut()
		{
			await signInManager.SignOutAsync();

			return RedirectToPage("/Account/Login");
		}
	}
}
