using ChalkboardChat.Data.Database;
using ChalkboardChat.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChalkboardChat.Data.Services
{
	public class MessageRepository(AppDbContext appContext, AuthDbContext authContext, SignInManager<IdentityUser> signInManager)
	{
		private readonly AppDbContext appContext = appContext;
		private readonly AuthDbContext authContext = authContext;
		private readonly SignInManager<IdentityUser> signInManager = signInManager;

		public async Task<List<MessageModel>> GetAllAsync()
		{
			return await appContext.Messages.OrderByDescending(m => m.Date).ToListAsync();
		}

		public async Task<MessageModel?> GetMessageByIdAsync(int id)
		{
			return await appContext.Messages.FirstOrDefaultAsync(m => m.Id == id);
		}

		public async Task AddMessageAsync(MessageModel messageToAdd)
		{
			await appContext.Messages.AddAsync(messageToAdd);
			await appContext.SaveChangesAsync();
		}

		public async Task UpdateMessageAsync(MessageModel updatedMessage)
		{
			MessageModel? messageToUpdate = await GetMessageByIdAsync(updatedMessage.Id);

			if (messageToUpdate != null)
			{
				messageToUpdate.Username = updatedMessage.Username;
				messageToUpdate.Message = updatedMessage.Message;
				messageToUpdate.Date = updatedMessage.Date;

				await appContext.SaveChangesAsync();
			}
		}

		public async Task DeleteMessageAsync(MessageModel messageToDelete)
		{
			try
			{
				appContext.Messages.Remove(messageToDelete);
				await appContext.SaveChangesAsync();
			}
			catch
			{
				//Couldn't find messageToDelete in Db
			}
		}

		//Det här bara skapar en override på DeleteMessage, så man kan välja att skicka med en int eller en hel MessageModel som den ovan
		public async Task DeleteMessageAsync(int id)
		{
			try
			{
				MessageModel? messageToDelete = await GetMessageByIdAsync(id);

				if (messageToDelete != null)
				{
					appContext.Messages.Remove(messageToDelete);
					await appContext.SaveChangesAsync();
				}
			}
			catch
			{
				//Couldn't find messageToDelete in Db
			}
		}

		public async Task UpdateUsername(string currentUsername, string newUsername)
		{
			IdentityUser? currentUser = authContext.Users.FirstOrDefault(u => u.UserName == currentUsername);

			if (currentUser == null) return;

			currentUser.UserName = newUsername;
			await authContext.SaveChangesAsync();

			var allMessages = await GetAllAsync();
			foreach (var message in allMessages)
			{
				if (message.Username == currentUsername)
				{
					message.Username = newUsername;
				}
			}

			await appContext.SaveChangesAsync();
		}

		public async Task DeleteUserAsync(string username)
		{
			IdentityUser? currentUser = authContext.Users.FirstOrDefault(u => u.UserName == username);

			if (currentUser == null) return;

			authContext.Users.Remove(currentUser);
			await authContext.SaveChangesAsync();

			var allMessages = await GetAllAsync();
			foreach (var message in allMessages)
			{
				if (message.Username == username)
				{
					message.Username = "{deleted user}";
				}
			}

			await appContext.SaveChangesAsync();
		}



	}

}
