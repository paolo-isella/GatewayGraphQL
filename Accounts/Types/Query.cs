using Accounts.Models;

namespace Accounts.Types;

public static class Query
{
    [Query]
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<User> GetUsers(AccountDbContext context) =>
        context.Users.AsQueryable();

    [Query]
    public static User? GetUserById(int id, AccountDbContext context) =>
        context.Users.FirstOrDefault(u => u.Id == id);

    [Query]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<User> GetUsersById(int[] ids, AccountDbContext context) =>
        context.Users.Where(u => ids.Contains(u.Id)).AsQueryable();

    [Query]
    public static User? GetUserByUsername(string username, AccountDbContext context) =>
        context.Users.FirstOrDefault(u => u.Username == username);
}
