namespace Reviews.Types;

public static class Query
{
    [Query]
    public static Review? GetReviewById(int id, ReviewDbContext context) =>
        context.Reviews.FirstOrDefault(r => r.Id == id);

    [Query]
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Review> GetReviews(ReviewDbContext context) =>
        context.Reviews.AsQueryable();

    [Query]
    public static User? GetUserById(int id) => new() { Id = id };

    [Query]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<User>? GetUsersById(int[] ids) =>
        ids.Select(id => new User { Id = id }).AsQueryable();
}
