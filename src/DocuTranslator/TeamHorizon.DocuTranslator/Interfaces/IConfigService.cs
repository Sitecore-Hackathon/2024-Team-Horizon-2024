namespace TeamHorizon.DocuTranslator.Interfaces
{
    public interface IConfigService
    {
        string GetEnvironmentVariable(string name);
        void LogConfigValues();  
        string DocuTranslationEndpoint { get; set; }
        string AzureKey { get; set; }
        string DocuSourceUri { get; set; }
        string DocuTargetUri { get; set; }
        string DocuTargetLang { get; set; }
        string SlotName { get; set; }
        string FuncappApikey { get; set; }
    }
}
