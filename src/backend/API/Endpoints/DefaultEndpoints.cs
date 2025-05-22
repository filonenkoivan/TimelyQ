using Hangfire;

namespace API.Endpoints
{
    public static class DefaultEndpoints
    {
        public static int CounterNumber { get; set; }
        public static void MapDefaultEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/", DefaultEndpoint);
        }

        public static void DefaultEndpoint(IBackgroundJobClient client)
        {
            RecurringJob.AddOrUpdate(() => Counter(), Cron.Minutely);

        }

        public static void Counter()
        {
            Console.WriteLine(CounterNumber);
            CounterNumber++;
        }
    }
}
