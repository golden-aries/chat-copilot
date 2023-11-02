using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sk_chatgpt_azure_function
{
    using System.Net;
    using Microsoft.Azure.Functions.Worker;
    using Microsoft.Azure.Functions.Worker.Http;
    using Microsoft.Extensions.Logging;
    using System.Globalization;
    using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;

    namespace MathPlugin
    {
        public class Add
        {
            private readonly ILogger _logger;

            public Add(ILoggerFactory loggerFactory)
            {
                _logger = loggerFactory.CreateLogger<Add>();
            }

            [OpenApiOperation(operationId: "Joke", tags: new[] { "Joke" }, Description = "Generate a funny joke")]
            [OpenApiRequestBody("text/plain", typeof(string), Description = "Joke subject")]
            [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
  
            [Function("Add")]
            public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
            {
                bool result1 = double.TryParse(req.Query["number1"], out double number1);
                bool result2 = double.TryParse(req.Query["number2"], out double number2);

                if (result1 && result2)
                {
                    HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
                    response.Headers.Add("Content-Type", "text/plain");
                    double sum = number1 + number2;
                    response.WriteString(sum.ToString(CultureInfo.CurrentCulture));

                    _logger.LogInformation($"Add function processed a request. Sum: {sum}");

                    return response;
                }
                else
                {
                    HttpResponseData response = req.CreateResponse(HttpStatusCode.BadRequest);
                    response.Headers.Add("Content-Type", "application/json");
                    response.WriteString("Please pass two numbers on the query string or in the request body");

                    return response;
                }
            }
        }
    }
}
