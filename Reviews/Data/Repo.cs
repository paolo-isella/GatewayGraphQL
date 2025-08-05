using Reviews.Types;

namespace Reviews.Data;

public static class Repo
{
    private static User[] _users = [
        new()
        {
            Id = 1,
            Name = "Ada Lovelace"
        },
        new()
        {
            Id = 2,
            Name = "Alan Turing"
        }
    ];

    private static Review[] _reviews = [
        new()
        {
            Id = 1,
            Body = "Love it!",
            Stars = 5,
            ProductId = 1,
            User = _users[0]
        },
        new()
        {
            Id = 2,
            Body = "Too expensive.",
            Stars = 1,
            ProductId = 2,
            User = _users[1]
        },
        new()
        {
            Id = 3,
            Body = "Could be better.",
            Stars = 3,
            ProductId = 3,
            User = _users[0]
        },
        new()
        {
            Id = 4,
            Body = "Prefer something else.",
            Stars = 3,
            ProductId = 2,
            User = _users[1]
        }
    ];
    
    public static User[] Users => _users;
    public static Review[] Reviews => _reviews;
}