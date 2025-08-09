using GreenDonut.Data;
using HotChocolate.Resolvers;
using Microsoft.EntityFrameworkCore;

namespace Reviews.Types;

public class User
{
    public int Id { get; set; }
}

[ExtendObjectType<User>]
public static class UserNode
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<Review[]> GetReviews(
        [Parent] User user,
        QueryContext<Review> query,
        IReviewsByUserIdDataLoader loader
    ) => await loader.With(query).LoadAsync(user.Id) ?? [];
}

internal static class DataLoaders
{
    [DataLoader]
    public static async Task<Dictionary<int, Review[]>> GetReviewsByUserIdAsync(
        IReadOnlyList<int> userIds,
        QueryContext<Review> query,
        ReviewDbContext context,
        CancellationToken cancellationToken
    )
    {
        var reviews = context.Reviews.Where(t => userIds.Contains(t.UserId));

        if (query.Predicate is not null)
        {
            reviews = reviews.Where(query.Predicate!);
        }

        return await reviews
            .GroupBy(t => t.UserId)
            .ToDictionaryAsync(t => t.Key, t => t.ToArray(), cancellationToken);
    }
}
