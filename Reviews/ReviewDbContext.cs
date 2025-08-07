using Microsoft.EntityFrameworkCore;
using Reviews.Types;

namespace Reviews;

public class ReviewDbContext(DbContextOptions<ReviewDbContext> opt) : DbContext(opt)
{
    public DbSet<Review> Reviews { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
}