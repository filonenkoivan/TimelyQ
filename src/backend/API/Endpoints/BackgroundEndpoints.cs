using Hangfire;
using System.Runtime.CompilerServices;

namespace API.Endpoints
{
    public static class BackgroundEndpoints
    {
        public static void MapBackgroundEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/timer", Timer);
        }

        public static void Timer(IBackgroundJobClient client) {
            client.Schedule(() => Console.WriteLine("im deleyed"), TimeSpan.FromSeconds(10));
        }
    }
}
