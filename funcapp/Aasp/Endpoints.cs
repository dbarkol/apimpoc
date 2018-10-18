using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace funcapp.Aasp
{
    public static class Endpoints
    {
        private static Dictionary<string, string> EndpointMap = new Dictionary<string, string>()
        {
            { "12345", "10.1.1.1" },
            { "67890", "192.168.0.1" }
        };

        [FunctionName("Endpoints")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Aasp/Endpoints/{appId}")]HttpRequest req, ILogger log, string appId)
        {
            log.LogInformation("Aasp.Endpoints function processed a request.");

            string authToken = req.Headers["Authorization"];

            if (string.IsNullOrEmpty(authToken))
                return new ObjectResult("Authorization header value is null or empty.");

            try
            {
                TokenHelpers.IsTokenValid(authToken, Constants.Issuer, Constants.AaspAudience);
            }
            catch (Exception e)
            {
                return new ObjectResult(e.Message) { StatusCode = 401 };
            }

            if (appId == null)
                return new BadRequestObjectResult("AppId path segment is not present.");

            if (!EndpointMap.ContainsKey(appId))
                return new BadRequestObjectResult($"No endpoint defined for AppId {appId}.");

            return new OkObjectResult(EndpointMap[appId]);
        }
    }
}
