using Accounts.Models;
using Microsoft.EntityFrameworkCore;

namespace Accounts;

public class AccountDbContext(DbContextOptions<AccountDbContext> opt) : DbContext(opt)
{
    public DbSet<User> Users { get; set; }
}