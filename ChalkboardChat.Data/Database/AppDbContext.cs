using ChalkboardChat.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ChalkboardChat.Data.Database
{
	public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
	{
		public DbSet<MessageModel> Messages { get; set; }
	}
}
