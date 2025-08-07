namespace Reviews.Types;

public class User
{
    public int Id { get; set; }

    public string? Name { get; set; }
}

[ExtendObjectType<User>]
public static class UserNode
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Review> GetReviews([Parent] User user, ReviewDbContext context)
        => context.Reviews.Where(r => r.User.Id == user.Id).AsQueryable();
}