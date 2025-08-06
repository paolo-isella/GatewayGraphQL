using Reviews.Data;

namespace Reviews.Types;

public class Query
{
    public static Review? GetReviewById(int id)
        => Repo.Reviews.FirstOrDefault(r => r.Id == id);
    
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Review> GetReviews()
        => Repo.Reviews.AsQueryable();
    
    public User? GetUserById(int id)
        => Repo.Users.FirstOrDefault(u => u.Id == id);
    
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<User>? GetUsersById(int[] ids)
        => Repo.Users.Where(u => ids.Contains(u.Id)).AsQueryable();
    
    // questo provare a mettere e togliere dopo avere creato i db context per vedere l'effetto sulle query
    // potrebbe andare ad interferire mala con l'equivalente di Accounts
    // [UseProjection]
    // [UseFiltering]
    // [UseSorting]
    // public IQueryable<User>? GetUsers()
    //     => Repo.Users.AsQueryable();
}