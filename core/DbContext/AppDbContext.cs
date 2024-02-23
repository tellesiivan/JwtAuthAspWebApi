using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthAspWebApi.core.DbContext;

public class AppDbContext(DbContextOptions<AppDbContext> dbContextOptions): IdentityDbContext(dbContextOptions)
{
}