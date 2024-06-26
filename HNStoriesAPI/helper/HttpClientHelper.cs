﻿using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace HackerNews.Tests.helper
{
    public class HttpClientHelper
    {
        public static Mock<HttpMessageHandler> GetResults<T>(T response)
        {
            var httpResponse = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(response)),
                StatusCode = HttpStatusCode.OK
            };
            httpResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            mockHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",

                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
                ).ReturnsAsync(httpResponse);

            return mockHandler;
        }
    }
}
