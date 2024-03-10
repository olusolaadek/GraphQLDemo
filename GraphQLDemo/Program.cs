using HotChocolate;

using GraphQLDemo.Schema.Mutations;
using GraphQLDemo.Schema.Queries;
using GraphQLDemo.Schema.Subscriptions;

namespace GraphQLDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>()
                .AddInMemorySubscriptions();

            // builder.Services.AddInMemorySubscriptions();

            var app = builder.Build();


            //  app.MapGet("/", () => "Hello World!");

            app.UseRouting();
            app.UseWebSockets();
            //app.MapGraphQL();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL(path: "/graphql");
            });
            app.Run();
        }
    }
}
