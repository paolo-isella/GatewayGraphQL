namespace Accounts.Data;

public static class Repo
{
    private static User[] _users =
    [
        new()
        {
            Id = 1,
            Name = "Ada Lovelace",
            DateTime = new DateTime(),
            Username = "ada",
        },
        new()
        {
            Id = 2,
            Name = "Alan Turing",
            DateTime = new DateTime(),
            Username = "alan-turing",
        }
    ];
    
    public static User[] Users => _users;
}