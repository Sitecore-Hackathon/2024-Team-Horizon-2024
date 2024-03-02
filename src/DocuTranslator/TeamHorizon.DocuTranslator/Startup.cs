using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TeamHorizon.DocuTranslator.Interfaces;
using TeamHorizon.DocuTranslator.Logging;
using TeamHorizon.DocuTranslator.Services;

[assembly: FunctionsStartup(typeof(TeamHorizon.DocuTranslator.Startup))]
namespace TeamHorizon.DocuTranslator
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
                 .SetBasePath(Environment.CurrentDirectory)
                 .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                 .AddEnvironmentVariables()
                 .Build();

            builder.Services.AddSingleton<IConfigService, ConfigService>();
            builder.Services.AddTransient(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
            builder.Services.AddScoped<IBatchDocuTranslationService, BatchDocuTranslationService>();
        }
    }
}
