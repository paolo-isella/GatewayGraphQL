using HotChocolate;
using Accounts.Data;
using HotChocolate.Data;
using System.Linq;

namespace Accounts.Types;

public class Query
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<User> GetUsers(int? skip, int? take)
        => Repo.Users
            .OrderByDescending(t => t.Id)
            .Skip(skip ?? 0)
            .Take(take ?? int.MaxValue)
            .AsQueryable();

    public User? GetUserById(int id)
        => Repo.Users.FirstOrDefault(u => u.Id == id);

    public IQueryable<User> GetUsersById(int[] ids)
        => Repo.Users.Where(u => ids.Contains(u.Id)).AsQueryable();

    public User? GetUserByUsername(string username)
        => Repo.Users.FirstOrDefault(u => u.Username == username);
}
