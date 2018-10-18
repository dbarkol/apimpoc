using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace funcapp.Aasp
{
    public static class Auth
    {
        [FunctionName("Auth")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "Aasp/Auth")]HttpRequest req, ILogger log)
        {
            log.LogInformation("Aasp.Auth function processed a request.");

            string clientId = req.Form["ClientId"];
            string clientSecret = req.Form["ClientSecret"];

            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
                return new BadRequestObjectResult("ClientId and ClientSecret must be provided in the body of the message.");

            return new OkObjectResult(TokenHelpers.GenerateToken(Constants.AaspAudience, Constants.Issuer, TimeSpan.FromMinutes(5)));
        }
    }
}
