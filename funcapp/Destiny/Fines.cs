using funcapp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace funcapp.Aasp
{
    public static class Fines
    {
        [FunctionName("Fines")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Destiny/{AppId}/Fines")]HttpRequest req, ILogger log, string appId)
        {
			var authToken = req.Headers["Authorization"];

            if (string.IsNullOrEmpty(authToken))
                return new ObjectResult("Authorization header value is null or empty.");

            try
            {
                TokenHelpers.IsTokenValid(authToken, Constants.Issuer, appId);
            }
            catch (Exception e)
            {
                return new ObjectResult(e.Message) { StatusCode = 401 };
            }

			return new OkObjectResult(GetMockFines());
        }

		static List<Fine> GetMockFines()
		{
			var list = new List<Fine>
			{
				new Fine
				{
					FirstName = "Rob",
					Amount = 0.1,
					DateDue = DateTime.Now.AddDays(20),
					DateIssued = DateTime.Now
				},

				new Fine
				{
					FirstName = "Lynn",
					Amount = 18583.98,
					DateDue = DateTime.Now.AddDays(-1),
					DateIssued = DateTime.Now
				},

				new Fine
				{
					FirstName = "David",
					Amount = 16.54,
					DateDue = DateTime.Now.AddDays(7),
					DateIssued = DateTime.Now
				}
			};

			return list;
		}
	}
}
