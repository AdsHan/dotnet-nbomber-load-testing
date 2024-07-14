using NBomber.Contracts;
using NBomber.CSharp;
using NBomber.Http.CSharp;
using System.Text;


namespace LoadTesting.ConsoleApp.Scenarios
{
    internal class PostProduct
    {
        public static ScenarioProps CreateScenario(HttpClient httpClient)
        {
            var jsonString = @"
            {
                ""title"": ""Sandalia"",
                ""description"": ""Sandália Preta Couro Salto Fino"",
                ""price"": 249.50,
                ""quantity"": 100
            }";

            return Scenario.Create("PostProduct", async context =>
            {
                var request = Http.CreateRequest("POST", "http://localhost:5000/api/products")
                    .WithHeader("Content-Type", "application/json")
                    .WithBody(new StringContent(jsonString, Encoding.UTF8, "application/json"));

                var response = await Http.Send(httpClient, request);
                return response;
            })
            .WithLoadSimulations(
                Simulation.RampingInject(rate: 20, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(10))
            )
            .WithWarmUpDuration(TimeSpan.FromSeconds(5));
        }
    }
}
