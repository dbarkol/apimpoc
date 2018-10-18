
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using funcapp.Models;

namespace funcapp
{
    public static class Wpe
    {
        [FunctionName("Wpe")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "aasp/Wpe")]HttpRequest req, TraceWriter log)
        {
			return new OkObjectResult(new WpeObject { Title = "A Wpe Object" });
        }
    }
}
