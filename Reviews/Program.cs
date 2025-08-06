using Reviews.Types;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddCors(options =>
    {
        //https://studio.apollographql.com/sandbox/explorer
        options.AddDefaultPolicy(b => 
            b.WithOrigins("https://studio.apollographql.com").AllowAnyHeader().AllowAnyMethod());
    });

    builder.Services.AddGraphQLServer()
        .AddQueryType()
        .AddTypeExtension<Query>()
        .AddTypeExtension<UserNode>()
        .AddTypeExtension<ReviewNode>()
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