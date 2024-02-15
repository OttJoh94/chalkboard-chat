using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChalkboardChat.Data.Database
{
	public class AuthDbContext(DbContextOptions<AuthDbContext> options) : IdentityDbContext(options)
	{
	}
}
