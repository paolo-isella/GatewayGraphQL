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
        IReviewsByUserIdDataLoader loader
    ) => await loader.LoadAsync(user.Id) ?? [];
}

internal static class DataLoaders
{
    [DataLoader]
    public static async Task<Dictionary<int, Review[]>> GetReviewsByUserIdAsync(
        IReadOnlyList<int> userIds,
        ReviewDbContext context,
        CancellationToken cancellationToken
    ) =>
        await context
            .Reviews.Where(t => userIds.Contains(t.UserId))
            .GroupBy(t => t.UserId)
            .ToDictionaryAsync(t => t.Key, t => t.ToArray(), cancellationToken);
}
