using System;
using System.Web;
using Moq;

namespace Renterator.Services.Tests.Helpers
{
    public static class MockHelpers
    {
        public static Mock<HttpContextBase> BuildHttpContext(
            Uri requestUrl = null)
        {
            Mock<HttpContextBase> mockHttpContext = new Mock<HttpContextBase>();
            Mock<HttpRequestBase> mockRequest = new Mock<HttpRequestBase>();
            Mock<HttpResponseBase> mockResponse = new Mock<HttpResponseBase>();
            mockHttpContext.Setup(x => x.Request).Returns(mockRequest.Object);
            mockHttpContext.Setup(x => x.Response).Returns(mockResponse.Object);

            HttpCookieCollection cookies = new HttpCookieCollection();
            mockResponse.Setup(x => x.Cookies).Returns(cookies);

            mockRequest.Setup(x => x.Url).Returns(requestUrl);

            return mockHttpContext;
        }
    }
}
