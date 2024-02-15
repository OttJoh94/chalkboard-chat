using Microsoft.AspNetCore.Identity;

namespace ChalkboardChat.App
{
	public class AppUserManager(SignInManager<IdentityUser> signInManager)
	{
		private readonly SignInManager<IdentityUser> signInManager = signInManager;

		public async Task<bool> RegisterUserAsync(string username, string password)
		{
			bool createUserSucceeded = false;

			if (username == null || password == null) return createUserSucceeded;

			IdentityUser newUser = new() { UserName = username };

			var createUserResult = await signInManager.UserManager.CreateAsync(newUser, password);

			if (createUserResult.Succeeded)
			{
				createUserSucceeded = true;
			}

			return createUserSucceeded;
		}

		public async Task<bool> SignInUserAsync(string username, string password)
		{
			bool successfullSignIn = false;

			if (username == null || password == null) return successfullSignIn;

			IdentityUser? userToSignIn = await signInManager.UserManager.FindByNameAsync(username);

			if (userToSignIn == null) return successfullSignIn;

			var signInResult = await signInManager.PasswordSignInAsync(userToSignIn, password, false, false);
			if (signInResult.Succeeded)
			{
				successfullSignIn = true;
			}

			return successfullSignIn;
		}
	}
}
