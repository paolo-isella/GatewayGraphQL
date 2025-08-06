using HotChocolate;
using HotChocolate.Data;
using Reviews.Data;
using System.Linq;

namespace Reviews.Types;

public class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    // public IList<Review> Reviews { get; set; }
}

[ExtendObjectType<User>]
public class UserNode
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Review> GetReviewsAsync(
        [Parent] User user,
        int? skip,
        int? take)
        => Repo.Reviews
            .Where(r => r.User.Id == user.Id)
            .OrderByDescending(t => t.Id)
            .Skip(skip ?? 0)
            .Take(take ?? int.MaxValue)
            .AsQueryable();
}