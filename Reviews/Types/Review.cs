using Reviews.Data;

namespace Reviews.Types;

public class Review
{
    public int Id { get; set; }
    public string? Body { get; set; }
    public int Stars { get; set; }
    
    public int ProductId { get; set; }
    
    public int UserId { get; set; }
    public User? User { get; set; }
}

[ExtendObjectType<Review>(IgnoreProperties = new[] { nameof(Review.UserId) })]
public static class ReviewNode
{
    public static async Task<User> GetUserAsync(
        [Parent] Review review,
        IUserByIdDataLoader userDataLoader,
        CancellationToken cancellationToken)
        => await userDataLoader.LoadAsync(review.User.Id, cancellationToken);

    [DataLoader]
    public static async Task<IReadOnlyDictionary<int, Review>> GetReviewByIdAsync(IReadOnlyList<int> ids)
        => Repo.Reviews
            .Where(t => ids.Contains(t.Id))
            .ToDictionary(t => t.Id);

    [DataLoader]
    public static async Task<ILookup<int, Review>> GetReviewsByUserIdAsync(IReadOnlyList<int> ids)
    {
        var reviews = Repo.Reviews
            .Where(r => ids.Contains(r.User.Id))
            .ToList();

        return reviews.ToLookup(t => t.User.Id);
    }
}