using Api.utils;
using Carter;
using Utils.Definations;

namespace Api.Endpoints.Home;

public class HomeModule : CarterModule
{
    public HomeModule() : base("/Home")
    {

    }
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("", () => "Hello World!" + " " + EnvValueGetter.Get<string>(EnvVarriableNamesMapping.GetStringValue(EnvVarriableNames.HelloWorld)));
    }
}
