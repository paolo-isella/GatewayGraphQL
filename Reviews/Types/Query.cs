using Reviews.Data;

namespace Reviews.Types;

[QueryType]
public static class Query
{
    // [UseProjection]
    // [UseFiltering]
    // [UseSorting]
    public static async Task<Review?> GetReviewById(
        int id,
        ReviewByIdDataLoader reviewById,
        CancellationToken cancellationToken)
        => await reviewById.LoadAsync(id, cancellationToken);
    
    // [UsePaging]
    // [UseProjection]
    // [UseFiltering]
    // [UseSorting]
    public static IQueryable<Review> GetReviews()
        => Repo.Reviews.OrderByDescending(t => t.Id).AsQueryable();
    
    // [UseProjection]
    // [UseFiltering]
    // [UseSorting]
    public static async Task<User?> GetUserById(
        int id,
        UserByIdDataLoader userById,
        CancellationToken cancellationToken)
        => await userById.LoadAsync(id, cancellationToken);
    
    // [UsePaging]
    // [UseProjection]
    // [UseFiltering]
    // [UseSorting]
    public static async Task<IQueryable<User>?> GetUsersById(
        int[] ids,
        UserByIdDataLoader userById,
        CancellationToken cancellationToken)
        => (await userById.LoadAsync(ids, cancellationToken)).AsQueryable();
    
    // [UsePaging]
    // [UseProjection]
    // [UseFiltering]
    // [UseSorting]
    public static IQueryable<User>? GetUsers()
        => Repo.Users.OrderBy(t => t.Name).AsQueryable();
}