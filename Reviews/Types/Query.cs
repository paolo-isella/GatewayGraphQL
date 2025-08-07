namespace Reviews.Types;

public class Query
{
    public static Review? GetReviewById(int id, ReviewDbContext context)
        => context.Reviews.FirstOrDefault(r => r.Id == id);
    
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Review> GetReviews(ReviewDbContext context)
        => context.Reviews.AsQueryable();
    
    public User? GetUserById(int id, ReviewDbContext context)
        => context.Users.FirstOrDefault(u => u.Id == id);
    
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<User>? GetUsersById(int[] ids, ReviewDbContext context)
        => context.Users.Where(u => ids.Contains(u.Id)).AsQueryable();
    
    // questo provare a mettere e togliere dopo avere creato i db context per vedere l'effetto sulle query
    // potrebbe andare ad interferire mala con l'equivalente di Accounts
    // [UseProjection]
    // [UseFiltering]
    // [UseSorting]
    // public IQueryable<User>? GetUsers()
    //     => Repo.Users.AsQueryable();
}