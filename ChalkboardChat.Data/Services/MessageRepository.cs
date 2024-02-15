using ChalkboardChat.Data.Database;
using ChalkboardChat.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ChalkboardChat.Data.Services
{
	public class MessageRepository(AppDbContext context)
	{
		private readonly AppDbContext context = context;

		public async Task<List<MessageModel>> GetAllAsync()
		{
			return await context.Messages.ToListAsync();
		}

		public async Task<MessageModel?> GetMessageByIdAsync(int id)
		{
			return await context.Messages.FirstOrDefaultAsync(m => m.Id == id);
		}

		public async Task AddMessageAsync(MessageModel messageToAdd)
		{
			await context.Messages.AddAsync(messageToAdd);
			await context.SaveChangesAsync();
		}

		public async Task UpdateMessageAsync(MessageModel updatedMessage)
		{
			MessageModel? messageToUpdate = await GetMessageByIdAsync(updatedMessage.Id);

			if (messageToUpdate != null)
			{
				messageToUpdate.Username = updatedMessage.Username;
				messageToUpdate.Message = updatedMessage.Message;
				messageToUpdate.Date = updatedMessage.Date;

				await context.SaveChangesAsync();
			}
		}

		public async Task DeleteMessageAsync(MessageModel messageToDelete)
		{
			try
			{
				context.Messages.Remove(messageToDelete);
				await context.SaveChangesAsync();
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
					context.Messages.Remove(messageToDelete);
					await context.SaveChangesAsync();
				}
			}
			catch
			{
				//Couldn't find messageToDelete in Db
			}
		}


	}

}
