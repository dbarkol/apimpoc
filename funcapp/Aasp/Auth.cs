using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace funcapp.Aasp
{
    public static class Auth
    {
        [FunctionName("Auth")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Aasp/Auth")]HttpRequest req, ILogger log)
        {
            log.LogInformation("Aasp.Auth function processed a request.");

            ClientCredentialFlowInfo clientCredentialFlowInfo = new ClientCredentialFlowInfo();

            if (req.HasFormContentType)
            {
                clientCredentialFlowInfo.ClientId = req.Form["ClientId"];
                clientCredentialFlowInfo.ClientSecret = req.Form["ClientSecret"];
            }
            else
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                clientCredentialFlowInfo = JsonConvert.DeserializeObject<ClientCredentialFlowInfo>(requestBody);
            }

            if (string.IsNullOrEmpty(clientCredentialFlowInfo.ClientId) || string.IsNullOrEmpty(clientCredentialFlowInfo.ClientSecret))
                return new BadRequestObjectResult("ClientId and ClientSecret must be provided in the body of the message.");

            return new OkObjectResult(TokenHelpers.GenerateToken(Constants.AaspAudience, Constants.Issuer, TimeSpan.FromMinutes(5)));
        }

        public class ClientCredentialFlowInfo
        {
            public string ClientId { get; set; }
            public string ClientSecret { get; set; }
        }
    }
}
