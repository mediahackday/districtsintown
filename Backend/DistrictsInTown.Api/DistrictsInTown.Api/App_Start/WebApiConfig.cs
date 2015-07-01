using System.Web.Http;

namespace DistrictsInTown.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute
                (
                    "DefaultApi",
                    "api/{controller}/{value}",
                    new {value = RouteParameter.Optional}
                );
        }
    }
}