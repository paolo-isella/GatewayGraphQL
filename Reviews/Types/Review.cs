using Microsoft.EntityFrameworkCore;

namespace Reviews.Types;

public class Review
{
    public int Id { get; set; }
    public string? Body { get; set; }
    public int Stars { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

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
}

internal static class ProductDataLoader
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
}
