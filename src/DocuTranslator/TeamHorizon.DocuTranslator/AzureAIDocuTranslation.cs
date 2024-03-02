using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using TeamHorizon.DocuTranslator.Interfaces;
using System;

namespace TeamHorizon.DocuTranslator
{
    public class AzureAIDocuTranslation
    {
        private readonly ILoggerAdapter<AzureAIDocuTranslation> _logger;
        private readonly IBatchDocuTranslationService _batchDocuTranslationService;
        private readonly IConfigService _configService;

        public AzureAIDocuTranslation(IBatchDocuTranslationService batchDocuTranslationService, IConfigService configService, ILoggerAdapter<AzureAIDocuTranslation> logger)
        {
            _batchDocuTranslationService = batchDocuTranslationService;
            _configService               = configService;
            _logger                      = logger;
        }
        [FunctionName("AzureAIDocuTranslation")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
        {
            try
            {
                _logger.LogInformation("C# HTTP trigger function started processing a request.");
                _configService.LogConfigValues();

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                _logger.LogInformation($"Request body: {requestBody}");
                dynamic data = JsonConvert.DeserializeObject(requestBody);

                bool result = await _batchDocuTranslationService.TranslateDocumentsAsync();
                string responseMessage = $"This HTTP triggered function executed {(result ? "successfully" : "with errors")}.";
                _logger.LogInformation(responseMessage);
                return new OkObjectResult(responseMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "C# HTTP trigger function FAILED processing a request");
                return new NotFoundObjectResult("C# HTTP trigger function FAILED processing a request");
            }
        }
    }
}
