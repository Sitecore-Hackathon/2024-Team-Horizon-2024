using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NSubstitute;
using System.Text;
using TeamHorizon.DocuTranslator.Interfaces;

namespace TeamHorizon.DocuTranslator.Tests
{
    public class AzureAIDocuTranslationTests
    {
        private readonly AzureAIDocuTranslation _sut;
        private readonly ILoggerAdapter<AzureAIDocuTranslation> _logger = Substitute.For<ILoggerAdapter<AzureAIDocuTranslation>>();
        private readonly IBatchDocuTranslationService _batchDocuTranslationService = Substitute.For<IBatchDocuTranslationService>();
        private readonly IConfigService _configService = Substitute.For<IConfigService>();
        private DefaultHttpContext _httpContext;

        public AzureAIDocuTranslationTests()
        {
            _configService.FuncappApikey = "X-Api-Key";
            _sut = new AzureAIDocuTranslation(_batchDocuTranslationService, _configService, _logger);
            _httpContext = new DefaultHttpContext();
            _httpContext.Request.Headers["X-Api-Key"] = "X-Api-Key";
        }

        [Fact]
        public async void Run_ShouldReturnOkObjectResul_WhenContentHubPublishServiceSucceeds()
        {
            //Arrange
            bool mockResult = true;
            var mockReq = new { };
            _batchDocuTranslationService.TranslateDocumentsAsync().Returns(Task.FromResult(mockResult));
            var byteArray = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(mockReq));
            _httpContext.Request.Body = new MemoryStream(byteArray);
            //Act
            var response = await _sut.Run(_httpContext.Request);

            //Assert
            _logger.Received(1).LogInformation("C# HTTP trigger function started processing a request.");
            var result = response as OkObjectResult;
            result!.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
        [Fact]
        public async Task Post_ShouldLogErrorMessage_WhenDeleteServiceReturnsFalse()
        {
            // Arrange
            bool mockResult = false;
            var mockReq = new { };
            _batchDocuTranslationService.TranslateDocumentsAsync().Returns(Task.FromResult(mockResult));
            var byteArray = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(mockReq));
            _httpContext.Request.Body = new MemoryStream(byteArray);
            var expectedResult = $"This HTTP triggered function executed with errors.";
            // Act
            var response = await _sut.Run(_httpContext.Request);

            // Assert
            _logger.Received(1).LogInformation("C# HTTP trigger function started processing a request.");
            var result = response as OkObjectResult;
            result!.Value.Should().Be(expectedResult);
        }
    }
}