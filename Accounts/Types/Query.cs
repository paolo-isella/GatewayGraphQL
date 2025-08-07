using Accounts.Models;

namespace Accounts.Types;

public class Query
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<User> GetUsers(AccountDbContext context)
        => context.Users.AsQueryable();

    public User? GetUserById(int id, AccountDbContext context)
        => context.Users.FirstOrDefault(u => u.Id == id);

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<User> GetUsersById(int[] ids, AccountDbContext context)
        => context.Users.Where(u => ids.Contains(u.Id)).AsQueryable();

    public User? GetUserByUsername(string username, AccountDbContext context)
        => context.Users.FirstOrDefault(u => u.Username == username);
}
