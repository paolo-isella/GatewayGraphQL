using Accounts.Data;

namespace Accounts.Types;

[QueryType]
public static class Query
{
    public static IQueryable<User> GetUsers()
        => Repo.Users.OrderByDescending(t => t.Id).AsQueryable();

    public static User? GetUserById(int id)
        => Repo.Users.FirstOrDefault(u => u.Id == id);

    public static IQueryable<User> GetUsersById(int[] ids)
        => Repo.Users.Where(u => ids.Contains(u.Id)).AsQueryable();

    public static User? GetUserByUsername(string username)
        => Repo.Users.FirstOrDefault(u => u.Username == username);
}
