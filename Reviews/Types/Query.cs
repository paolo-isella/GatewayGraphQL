using HotChocolate;
using HotChocolate.Data;
using Reviews.Data;
using System.Linq;

namespace Reviews.Types;

public class Query
{
    // [UseProjection]
    // [UseFiltering]
    // [UseSorting]
    public static Review? GetReviewById(int id)
        => Repo.Reviews.FirstOrDefault(r => r.Id == id);
    
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Review> GetReviews(int? skip, int? take)
        => Repo.Reviews
            .OrderByDescending(t => t.Id)
            .Skip(skip ?? 0)
            .Take(take ?? int.MaxValue)
            .AsQueryable();
    
    public User? GetUserById(int id)
        => Repo.Users.FirstOrDefault(u => u.Id == id);
    
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<User>? GetUsersById(int[] ids, int? skip, int? take)
        => Repo.Users.Where(u => ids.Contains(u.Id))
            .OrderBy(t => t.Name)
            .Skip(skip ?? 0)
            .Take(take ?? int.MaxValue)
            .AsQueryable();
    
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<User>? GetUsers(int? skip, int? take)
        => Repo.Users
            .OrderBy(t => t.Name)
            .Skip(skip ?? 0)
            .Take(take ?? int.MaxValue)
            .AsQueryable();
}