using System;
using TeamHorizon.DocuTranslator.Interfaces;

namespace TeamHorizon.DocuTranslator.Services
{
    public class ConfigService : IConfigService
    {
        private readonly ILoggerAdapter<ConfigService> _logger;
        public ConfigService(ILoggerAdapter<ConfigService> logger)
        {
            _logger = logger;
            GetEnvironmentVariables();
        }

        /// <summary>
        /// your Azure AI services document translation endpoint
        /// </summary>
        public string DocuTranslationEndpoint { get; set; }
        /// <summary>
        /// Your Azure AI services document translation key
        /// </summary>
        public string AzureKey { get; set; }
        /// <summary>
        /// The URL for the source container containing documents to be translated.
        /// </summary>
        public string DocuSourceUri { get; set; }
        /// <summary>
        /// The URL for the target container to which the translated documents are written.
        /// </summary>
        public string DocuTargetUri { get; set; }
        /// <summary>
        /// The language code for the translated documents
        /// https://learn.microsoft.com/en-us/azure/ai-services/translator/language-support
        /// </summary>
        public string DocuTargetLang { get; set; }
        public string SlotName { get; set; }

        public string GetEnvironmentVariable(string name)
        {
            return Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
        }

        public void LogConfigValues()
        {
            _logger.LogInformation($"Environment: APPSETTING_WEBSITE_SLOT_NAME={SlotName}");
            _logger.LogInformation($"Config: docu_translation_endpoint='{DocuTranslationEndpoint}'");
            _logger.LogInformation($"Config: azure_key='{AzureKey}'");
            _logger.LogInformation($"Config: docu_source_uri='{DocuSourceUri}'");
            _logger.LogInformation($"Config: docu_target_uri='{DocuTargetUri}'");
            _logger.LogInformation($"Config: docu_target_lang='{DocuTargetLang}'");
        }
        private void GetEnvironmentVariables()
        {
            SlotName = GetEnvironmentVariable("APPSETTING_WEBSITE_SLOT_NAME");
            DocuTranslationEndpoint = GetEnvironmentVariable("docu_translation_endpoint");
            AzureKey = GetEnvironmentVariable("azure_key");
            DocuSourceUri = GetEnvironmentVariable("docu_source_uri");
            DocuTargetUri = GetEnvironmentVariable("docu_target_uri");
            DocuTargetLang = GetEnvironmentVariable("docu_target_lang");;
        }
    }
}
