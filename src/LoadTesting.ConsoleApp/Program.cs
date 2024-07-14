using LoadTesting.ConsoleApp.Scenarios;
using NBomber.Contracts.Stats;
using NBomber.CSharp;

var httpClient = new HttpClient();

var scenarioGetAllProducts = GetAllProducts.CreateScenario(httpClient);
var scenarioGetProductById = GetProductById.CreateScenario(httpClient);
var scenarioPostProduct = PostProduct.CreateScenario(httpClient);

NBomberRunner
    .RegisterScenarios(scenarioGetAllProducts, scenarioGetProductById, scenarioPostProduct)
    .WithReportFileName("products_report")
    .WithReportFolder("./reports/products")
    .WithReportFormats(ReportFormat.Txt, ReportFormat.Csv, ReportFormat.Html, ReportFormat.Md)
    .Run();