using GreenDonut.Data;
using Microsoft.EntityFrameworkCore;

namespace Reviews.Types;

public class Review
{
    public int Id { get; set; }
    public string? Body { get; set; }
    public int Stars { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    public List<LifeCycle> LifeCycles { get; set; }

    public int UserId { get; set; }
}

[ObjectType<Review>]
public static partial class ReviewNode
{
    public static User GetUser([Parent] Review review) => new() { Id = review.UserId };

    public static Task<Product?> GetProduct(
        [Parent] Review review,
        IProductByIdDataLoader loader
    ) => loader.LoadAsync(review.ProductId);

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<LifeCycle[]> GetLifeCycles(
        [Parent] Review review,
        QueryContext<LifeCycle> query,
        ILifeCycleByIdDataLoader loader
    ) => await loader.With(query).LoadAsync(review.Id) ?? [];
}

internal static class ReviewDataLoader
{
    [DataLoader]
    public static async Task<Dictionary<int, Product>> GetProductByIdAsync(
        IReadOnlyList<int> productIds,
        ReviewDbContext context,
        CancellationToken cancellationToken
    ) =>
        await context
            .Products.Where(t => productIds.Contains(t.Id))
            .ToDictionaryAsync(t => t.Id, cancellationToken);

    [DataLoader]
    public static async Task<Dictionary<int, LifeCycle[]>> GetLifeCycleByIdAsync(
        IReadOnlyList<int> reviewIds,
        QueryContext<LifeCycle> query,
        ReviewDbContext context,
        CancellationToken cancellationToken
    )
    {
        var lifeCycles = context.LifeCycles.Where(l => reviewIds.Contains(l.Review.Id));

        if (query.Predicate is not null)
        {
            lifeCycles = lifeCycles.Where(query.Predicate!);
        }

        return await lifeCycles
            .GroupBy(t => t.Review.Id)
            .ToDictionaryAsync(t => t.Key, t => t.ToArray(), cancellationToken);
    }
}
