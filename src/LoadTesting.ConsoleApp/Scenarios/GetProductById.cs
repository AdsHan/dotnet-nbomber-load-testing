using NBomber.Contracts;
using NBomber.CSharp;
using NBomber.Http.CSharp;


namespace LoadTesting.ConsoleApp.Scenarios
{
    internal class GetProductById
    {
        public static ScenarioProps CreateScenario(HttpClient httpClient)
        {
            return Scenario.Create("GetProductById", async context =>
            {
                var request = Http.CreateRequest("GET", "http://localhost:5000/api/products/1");

                var response = await Http.Send(httpClient, request);
                return response;
            })
            .WithLoadSimulations(
                Simulation.Inject(rate: 200, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(5))
            )
            .WithWarmUpDuration(TimeSpan.FromSeconds(5));
        }
    }
}
