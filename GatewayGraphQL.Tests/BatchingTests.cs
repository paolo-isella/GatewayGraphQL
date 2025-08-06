using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Linq;
using System.IO;
using HotChocolate.Execution;
using HotChocolate.Execution.Serialization;
using HotChocolate.Fusion;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

public class BatchingTests
{
    [Fact]
    public async Task UsersQueryBatchesAccountsRequests()
    {
        // Build Accounts subgraph executor
        var accountsExecutor = await new ServiceCollection()
            .AddGraphQLServer()
            .AddQueryType<Accounts.Types.Query>()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .BuildRequestExecutorAsync();

        // Build Reviews subgraph executor
        var reviewsExecutor = await new ServiceCollection()
            .AddGraphQLServer()
            .AddQueryType<Reviews.Types.Query>()
            .AddTypeExtension<Reviews.Types.UserNode>()
            .AddTypeExtension<Reviews.Types.ReviewNode>()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .BuildRequestExecutorAsync();

        var handler = new SubgraphRouterHandler(accountsExecutor, reviewsExecutor);
        var executor = await CreateGatewayExecutorAsync(handler);

        var query = @"query {
  reviews {
    id
    user {
      id
      name
      username
    }
  }
}";

        var initialResult = await executor.ExecuteAsync(query);
        var initialFormatter = new JsonResultFormatter();
        using (var ms = new MemoryStream())
        {
            await initialFormatter.FormatAsync(initialResult, ms, CancellationToken.None);
            ms.Position = 0;
            using var doc = JsonDocument.Parse(ms);
            Assert.False(doc.RootElement.TryGetProperty("errors", out _));
        }

        Assert.Equal(1, handler.AccountsUsersByIdCalls);
        Assert.NotNull(handler.LastUsersByIdIds);
        Assert.Equal(new[] { 1, 2 }, handler.LastUsersByIdIds!.OrderBy(x => x).ToArray());
    }

    private static async Task<IRequestExecutor> CreateGatewayExecutorAsync(SubgraphRouterHandler handler)
    {
        var services = new ServiceCollection();
        services.AddHttpClient("Fusion").ConfigurePrimaryHttpMessageHandler(() => handler);
        services.AddFusionGatewayServer()
            .ConfigureFromFile(GetGatewayPath());

        var provider = services.BuildServiceProvider();
        var resolver = provider.GetRequiredService<IRequestExecutorResolver>();
        return await resolver.GetRequestExecutorAsync();
    }

    private static string GetGatewayPath()
    {
        var baseDir = AppContext.BaseDirectory;
        return Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "..", "GatewayGraphQL", "gateway.fgp"));
    }

    private sealed class SubgraphRouterHandler : HttpMessageHandler
    {
        private readonly IRequestExecutor _accounts;
        private readonly IRequestExecutor _reviews;

        public int AccountsUsersByIdCalls { get; private set; }
        public int[]? LastUsersByIdIds { get; private set; }

        public SubgraphRouterHandler(IRequestExecutor accounts, IRequestExecutor reviews)
        {
            _accounts = accounts;
            _reviews = reviews;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var body = await request.Content!.ReadAsStringAsync(cancellationToken);
            using var doc = JsonDocument.Parse(body);
            var query = doc.RootElement.GetProperty("query").GetString()!;
            Dictionary<string, object?>? variables = null;
            if (doc.RootElement.TryGetProperty("variables", out var vars) && vars.ValueKind != JsonValueKind.Null)
            {
                variables = new Dictionary<string, object?>();
                foreach (var prop in vars.EnumerateObject())
                {
                    if (prop.Value.ValueKind == JsonValueKind.Array)
                    {
                        variables[prop.Name] = prop.Value.EnumerateArray().Select(e => e.GetInt32()).ToArray();
                    }
                    else if (prop.Value.ValueKind == JsonValueKind.Number)
                    {
                        variables[prop.Name] = prop.Value.GetInt32();
                    }
                    else
                    {
                        variables[prop.Name] = prop.Value.GetString();
                    }
                }
            }

            IRequestExecutor executor;
            if (request.RequestUri!.AbsoluteUri.Contains("5204"))
            {
                executor = _accounts;
                  if (query.Contains("usersById"))
                  {
                      AccountsUsersByIdCalls++;
                      if (variables != null && variables.TryGetValue("ids", out var idsObj) && idsObj is int[] ids)
                      {
                          LastUsersByIdIds = ids;
                      }
                  }
            }
            else
            {
                executor = _reviews;
            }

            var builder = OperationRequestBuilder.New().SetDocument(query);
            if (variables != null)
            {
                builder.SetVariableValues(variables);
            }
            var op = builder.Build();
            var result = await executor.ExecuteAsync(op, cancellationToken);
            var formatter = new JsonResultFormatter();
            using var ms = new MemoryStream();
            await formatter.FormatAsync(result, ms, cancellationToken);
            ms.Position = 0;
            var json = Encoding.UTF8.GetString(ms.ToArray());
            var content = new StringContent(json, Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = content
            };
        }
    }
}
