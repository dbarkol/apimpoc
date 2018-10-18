
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Collections.Generic;
using funcapp.Models;
using System;

namespace funcapp.Aasp
{
    public static class Destiny_Fines
    {
        [FunctionName("Fines")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "aasp/Fines")]HttpRequest req, TraceWriter log)
        {
			var list = GetMockFines();
			return new OkObjectResult(list);
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
					Amount = 1000,
					DateDue = DateTime.Now.AddDays(3),
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
