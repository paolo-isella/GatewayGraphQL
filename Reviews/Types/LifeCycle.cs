namespace Reviews.Types;

public class LifeCycle
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public Review Review { get; set; }
}