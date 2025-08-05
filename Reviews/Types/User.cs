using Reviews.Data;

namespace Reviews.Types;

public class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    // public IList<Review> Reviews { get; set; }
}

[ExtendObjectType<User>]
public static class UserNode
{
    public static async Task<IQueryable<Review>> GetReviewsAsync(
        [Parent] User user,
        IReviewsByUserIdDataLoader reviewsById,
        CancellationToken cancellationToken)
        => (await reviewsById.LoadAsync(user.Id, cancellationToken)).AsQueryable();

    [DataLoader]
    public static async Task<IReadOnlyDictionary<int, User>> GetUserByIdAsync(
        IReadOnlyList<int> ids)
        => Repo.Users
            .Where(t => ids.Contains(t.Id))
            .ToDictionary(t => t.Id);
}