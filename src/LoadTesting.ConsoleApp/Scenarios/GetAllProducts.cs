using NBomber.Contracts;
using NBomber.CSharp;
using NBomber.Http.CSharp;


namespace LoadTesting.ConsoleApp.Scenarios
{
    internal class GetAllProducts
    {
        public static ScenarioProps CreateScenario(HttpClient httpClient)
        {
            return Scenario.Create("GetAllProducts", async context =>
            {
                var request = Http.CreateRequest("GET", "http://localhost:5000/api/products");

                var response = await Http.Send(httpClient, request);
                return response;
            })
            .WithLoadSimulations(
                // Primeiro estágio: 1 para 100 em 10s
                Simulation.RampingInject(rate: 100, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(10)),
                // Segundo estágio: Imediatamente saltar para 200 e manter durante 5s
                Simulation.Inject(rate: 200, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(5))
            )
            .WithWarmUpDuration(TimeSpan.FromSeconds(5));
        }
    }
}
