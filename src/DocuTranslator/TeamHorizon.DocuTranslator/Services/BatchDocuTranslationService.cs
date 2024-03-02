using Azure.AI.Translation.Document;
using Azure;
using System;
using System.Threading.Tasks;
using TeamHorizon.DocuTranslator.Interfaces;

namespace TeamHorizon.DocuTranslator.Services
{
    public class BatchDocuTranslationService : IBatchDocuTranslationService
    {
        private readonly IConfigService _configService;
        private readonly ILoggerAdapter<BatchDocuTranslationService> _logger;

        public BatchDocuTranslationService(IConfigService configService, ILoggerAdapter<BatchDocuTranslationService> logger)
        {
            _logger = logger;
            _configService = configService;
        }
        public async Task<bool> TranslateDocumentsAsync()
        {
            try
            {
                // create variables for your sourceUrl, targetUrl, and targetLanguageCode
                Uri sourceUri = new Uri(_configService.DocuSourceUri);
                Uri targetUri = new Uri(_configService.DocuTargetUri);
                string targetLanguage = _configService.DocuTargetLang;

                // initialize a new instance  of the DocumentTranslationClient object to interact with the Document Translation feature
                var client = new DocumentTranslationClient(
                    new Uri(_configService.DocuTranslationEndpoint), new AzureKeyCredential(_configService.AzureKey));

                // initialize a new instance of the `DocumentTranslationInput` object to provide source and target locations and target language for the translation operation
                var input = new DocumentTranslationInput(sourceUri, targetUri, targetLanguage);

                // initialize a new instance of the DocumentTranslationOperation class to track the status of the translation operation
                var operation = await client.StartTranslationAsync(input);

                await operation.WaitForCompletionAsync();

                _logger.LogInformation($"Status: {operation.Status}");
                _logger.LogInformation($"Created on: {operation.CreatedOn}");
                _logger.LogInformation($"Last modified: {operation.LastModified}");
                _logger.LogInformation($"Total documents: {operation.DocumentsTotal}");
                _logger.LogInformation($"Succeeded: {operation.DocumentsSucceeded}");
                _logger.LogInformation($"Failed: {operation.DocumentsFailed}");
                _logger.LogInformation($"In Progress: {operation.DocumentsInProgress}");
                _logger.LogInformation($"Not started: {operation.DocumentsNotStarted}");

                await foreach (DocumentStatusResult document in operation.Value)
                {
                    _logger.LogInformation($"Document with Id: {document.Id}");
                    _logger.LogInformation($"Status:{document.Status}");
                    if (document.Status == DocumentTranslationStatus.Succeeded)
                    {
                        _logger.LogInformation($"Translated Document Uri: {document.TranslatedDocumentUri}");
                        _logger.LogInformation($"Translated to language: {document.TranslatedToLanguageCode}.");
                        _logger.LogInformation($"Document source Uri: {document.SourceDocumentUri}");
                    }
                    else
                    {
                        _logger.LogInformation($"Error Code: {document.Error.Code}");
                        _logger.LogInformation($"Message: {document.Error.Message}");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error batch processing document translations");
                return false;
            }
        }
    }
}
