using funcapp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace funcapp
{
    public static class Wpe
    {
        [FunctionName("Wpe")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Wpe/Foo")]HttpRequest req, ILogger log)
        {
			return new OkObjectResult(new WpeObject { Title = "A Wpe Object" });
        }
    }
}
