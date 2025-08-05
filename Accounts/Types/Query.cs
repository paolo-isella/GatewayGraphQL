using Accounts.Data;

namespace Accounts.Types;

[QueryType]
public static class Query
{
    public static IQueryable<User> GetUsers()
        => Repo.Users.OrderByDescending(t => t.Id).AsQueryable();
    
    public static async Task<User?> GetUserById(int id)
        => Repo.Users.FirstOrDefault(u => u.Id == id);
    
    public static async Task<IQueryable<User>?> GetUsersById(int[] ids)
        => Repo.Users.Where(u => ids.Contains(u.Id)).AsQueryable();
    
    public static async Task<User> GetUsersByUsername(string username)
        => Repo.Users.FirstOrDefault(u => u.Username == username);
}