using System.ComponentModel.DataAnnotations;

namespace ChalkboardChat.Data.Models
{
	public class MessageModel
	{
		[Key]
		public int Id { get; set; }
		public string? Username { get; set; }
		public string? Message { get; set; }
		public DateTime Date { get; set; } = DateTime.Now;
	}
}
