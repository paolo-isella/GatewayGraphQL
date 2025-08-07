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

[ExtendObjectType<Review>]
public static class ReviewNode
{
    public static User GetUser([Parent] Review review) =>
        new() { Id = review.UserId };
}
