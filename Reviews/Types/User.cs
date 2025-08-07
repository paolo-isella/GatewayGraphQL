namespace Reviews.Types;

public class User
{
    public int Id { get; set; }

    public string? Name { get; set; }
}

[ExtendObjectType<User>]
public class UserNode
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Review> GetReviewsAsync([Parent] User user, ReviewDbContext context)
        => context.Reviews.Where(r => r.User.Id == user.Id).AsQueryable();
}