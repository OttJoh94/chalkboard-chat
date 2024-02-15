using ChalkboardChat.Data.Database;
using ChalkboardChat.Data.Models;
using ChalkboardChat.Data.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkboardChat.UI.Pages
{
	public class OttoTestPageModel : PageModel
	{
		private readonly MessageRepository repo;

		public OttoTestPageModel(AppDbContext context)
		{
			repo = new(context);
		}

		public async Task OnGet()
		{
			MessageModel myMessage = new()
			{
				//Dessa ska ju egentligen inte hårdkodas utan hämtas från en <form>
				Username = "Otto",
				Message = "Funkar den här också?",
				//Date behövs inte för den sätts till tiden den skapas automatiskt
			};

			//Lägg till message i databasen
			await repo.AddMessageAsync(myMessage);

			//Hämta alla messages i databasen
			var allMessages = await repo.GetAllAsync();

			myMessage.Message = "Nu uppdaterar jag message till nått annat";

			//Uppdatera en message i databasen
			await repo.UpdateMessageAsync(myMessage);

			//Tar bort när jag har hela messaget
			//await repo.DeleteMessageAsync(myMessage);

			//Tar bort med enbart int id.
			await repo.DeleteMessageAsync(myMessage.Id);

		}
	}
}
