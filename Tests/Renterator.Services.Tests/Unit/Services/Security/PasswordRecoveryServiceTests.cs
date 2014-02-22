using System.Linq;
using System.Text.RegularExpressions;
using Moq;
using NUnit.Framework;
using Renterator.Services.AppServices.Common;
using Renterator.Services.AppServices.Security;
using Renterator.Services.Infrastructure;
using Renterator.Services.Tests.Helpers;

namespace Renterator.Services.Tests.Unit.Services.Security
{
    [TestFixture]
    public class PasswordRecoveryServiceTests
    {
        private InMemoryDataAccessor database;
        private InMemoryCache cache;

        [SetUp]
        public void SetUp()
        {
            database = new InMemoryDataAccessor();
            cache = new InMemoryCache();

            MappingHelper.InitializeMappings();
        }

        [Test]
        public void GenerateForgotPasswordLink_ShouldGenerateEmailWithSamePartsAsCurrentUrlAndCacheGuid()
        {
            // Setup
            const string email = "dflor003@gmail.com";
            Mock<IEmailService> mockEmailService = new Mock<IEmailService>();
            PasswordRecoveryService service = new PasswordRecoveryService(database, mockEmailService.Object, cache, new FakeRuntimeHelper());

            // Call into it
            string linkUrl = service.GenerateForgotPasswordLink(email);

            // Should have cached url token
            Assert.AreEqual(1, cache.Count);
            var cachedValue = cache.Single();
            Assert.IsTrue(cachedValue.Key.StartsWith("ForgotPassword."));
            Assert.AreEqual(email, cachedValue.Value);

            // Should have valid url with token
            Assert.IsNotNull(linkUrl);
            Assert.IsTrue(Regex.IsMatch(linkUrl, "http://mymachine/MySite/Home/PasswordReset/\\?token=[A-Za-z0-9-]{36}"));
        }

        [Test]
        public void GenerateForgotPasswordEmailBody_ShouldRenderEmailTemplate()
        {
            // Setup
            Mock<IEmailService> mockEmailService = new Mock<IEmailService>();
            PasswordRecoveryService service = new PasswordRecoveryService(database, mockEmailService.Object, cache, new FakeRuntimeHelper());

            // Call into it
            string result = service.GenerateForgotPasswordEmailBody("http://mymachine/MySite/Home/PasswordReset?token=1234");

            // Should render
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Contains(@"href=""http://mymachine/MySite/Home/PasswordReset?token=1234"""));
        }

        [Test]
        public void SendForgotPasswordEmail_WhenInvalidEmail_ReturnsErrorResult()
        {
            // Setup
            Mock<IEmailService> mockEmailService = new Mock<IEmailService>();
            PasswordRecoveryService service = new PasswordRecoveryService(database, mockEmailService.Object, cache, new FakeRuntimeHelper());

            // Call into it
            Result result = service.SendForgotPasswordEmail("invalid@email");

            // Should have error message
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsSuccess);
            Assert.IsNotNull(result.Messages);
            Assert.AreEqual(1, result.Messages.Count);

            LogMessage message = result.Messages.ElementAt(0);
            Assert.AreEqual(MessageType.Error, message.Type);
            Assert.IsNotNullOrEmpty(message.Message);
        }

        private class FakeRuntimeHelper : IHttpRuntimeHelper
        {
            public string MakeAbsolutePath(string path)
            {
                return path.Replace("~", "http://mymachine/MySite");
            }
        }
    }
}
