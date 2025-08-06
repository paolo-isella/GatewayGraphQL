using Accounts;
using Accounts.Types;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddCors(options =>
    {
        //https://studio.apollographql.com/sandbox/explorer
        options.AddDefaultPolicy(b => 
            b.WithOrigins("https://nitro.chillicream.com").AllowAnyHeader().AllowAnyMethod());
    });

    builder.Services.AddDbContextFactory<AccountDbContext>(opt => opt
        .UseNpgsql(builder.Configuration.GetConnectionString("AccountDbContext"))
        .EnableSensitiveDataLogging());
    
    builder.Services.AddGraphQLServer()
        .AddType<Query>()
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