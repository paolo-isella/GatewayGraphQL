using Microsoft.EntityFrameworkCore;
using Reviews;
using Reviews.Types;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddCors(options =>
    {
        //https://studio.apollographql.com/sandbox/explorer
        options.AddDefaultPolicy(b => 
            b.WithOrigins("https://nitro.chillicream.com").AllowAnyHeader().AllowAnyMethod());
    });
    
    builder.Services.AddDbContextFactory<ReviewDbContext>(opt => opt
        .UseSqlite("Data Source=app.db")
        .EnableSensitiveDataLogging());

    builder.Services.AddGraphQLServer()
        .AddType<Query>()
        .AddTypeExtension<UserNode>()
        .AddTypeExtension<ReviewNode>()
        .AddPagingArguments()
        .AddProjections()
        .AddFiltering()
        .AddSorting();
}

var app = builder.Build();
{
    app.UseCors();
    app.MapGraphQL();
    app.RunWithGraphQLCommands(args);
}