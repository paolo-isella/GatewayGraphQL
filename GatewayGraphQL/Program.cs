var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("Fusion");

builder.Services.AddCors(options =>
{
    //https://studio.apollographql.com/sandbox/explorer
    options.AddDefaultPolicy(b => 
        b.WithOrigins("https://nitro.chillicream.com").AllowAnyHeader().AllowAnyMethod());   
});

builder.Services
    .AddFusionGatewayServer()
    .ConfigureFromFile("gateway.fgp")
    .ModifyFusionOptions(x =>
    {
        x.AllowQueryPlan = true;
    });

var app = builder.Build();

app.MapGraphQL();
app.UseCors();

app.Run();