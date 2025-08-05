using Reviews.Data;

namespace Reviews.Types;

public sealed class Product
{
    public Product(int id)
    {
        Id = id;
    }

    public int Id { get; }

    public IQueryable<Review> GetReviews() => Repo.Reviews.AsQueryable();
}