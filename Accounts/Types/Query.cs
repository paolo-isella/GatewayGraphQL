using Accounts.Data;

namespace Accounts.Types;

public class Query
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<User> GetUsers()
        => Repo.Users.AsQueryable();

    public User? GetUserById(int id)
        => Repo.Users.FirstOrDefault(u => u.Id == id);

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<User> GetUsersById(int[] ids)
        => Repo.Users.Where(u => ids.Contains(u.Id)).AsQueryable();

    public User? GetUserByUsername(string username)
        => Repo.Users.FirstOrDefault(u => u.Username == username);
}
