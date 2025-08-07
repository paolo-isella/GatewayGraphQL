namespace Reviews.Types;

public class Product
{
    public int Id { get;  set; }
    public List<Review> Reviews { get; set; }
}