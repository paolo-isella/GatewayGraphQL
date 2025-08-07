namespace Reviews.Types;

public class Review
{
    public int Id { get; set; }
    public string? Body { get; set; }
    public int Stars { get; set; }
    
    public int ProductId { get; set; }
    public Product Product { get; set; }
    
    public int UserId { get; set; }
    public User? User { get; set; }
}

[ExtendObjectType<Review>(IgnoreProperties = [nameof(Review.UserId)])]
public static class ReviewNode
{
    public static Task<User> GetUserAsync([Parent] Review review)
        => Task.FromResult(review.User!);
}
