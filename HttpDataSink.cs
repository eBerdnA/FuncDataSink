using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading;

namespace FuncDataSink
{
    public class HttpDataSink
    {
        public HttpDataSink()
        {
            
        }
        [FunctionName("HttpDataSink")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string delay = req.Query["delay"];
            if (!string.IsNullOrEmpty(delay))
            {
                int delayInt = Convert.ToInt32(delay);
                Thread.Sleep(delayInt);
            }

            string statusCodeStr = req.Query["statuscode"];
            if (!string.IsNullOrEmpty(statusCodeStr))
            {
                int errorCodeInt = Convert.ToInt32(statusCodeStr);
                var res = new ContentResult();
                StreamReader reader = new StreamReader(req.Body); 
                string reqBody = reader.ReadToEnd();
                res.Content = reqBody;
                res.StatusCode = errorCodeInt;
                return res;
            }

            return new OkObjectResult(req.Body);
        }
    }
}
