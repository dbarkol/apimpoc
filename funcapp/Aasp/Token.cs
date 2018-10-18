using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace funcapp.Aasp
{
    public static class Token
    {
        [FunctionName("Token")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Aasp/{AppId}/Token")]HttpRequest req, ILogger log, string appId)
        {
            log.LogInformation("Aasp.Token function processed a request.");

            string authToken = req.Headers["Authorization"];

            if (string.IsNullOrEmpty(authToken))
                return new ObjectResult("Authorization header value is null or empty.");

            try
            {
                TokenHelpers.IsTokenValid(authToken, Constants.Issuer, Constants.AaspAudience);
            }
            catch(Exception e)
            {
                return new ObjectResult(e.Message) { StatusCode = 401 };
            }

            string accessToken = TokenHelpers.GenerateToken(appId, Constants.Issuer, TimeSpan.FromMinutes(5));

            return new OkObjectResult(accessToken);
        }
    }
}
