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
				//Dessa ska ju egentligen inte h�rdkodas utan h�mtas fr�n en <form>
				Username = "Otto",
				Message = "Funkar den h�r ocks�?",
				//Date beh�vs inte f�r den s�tts till tiden den skapas automatiskt
			};

			//L�gg till message i databasen
			await repo.AddMessageAsync(myMessage);

			//H�mta alla messages i databasen
			var allMessages = await repo.GetAllAsync();

			myMessage.Message = "Nu uppdaterar jag message till n�tt annat";

			//Uppdatera en message i databasen
			await repo.UpdateMessageAsync(myMessage);

			//Tar bort n�r jag har hela messaget
			//await repo.DeleteMessageAsync(myMessage);

			//Tar bort med enbart int id.
			await repo.DeleteMessageAsync(myMessage.Id);

		}
	}
}
