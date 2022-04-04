﻿using Laserfiche.Oauth.Api.Client;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Laserfiche.OAuth.Client.ClientCredentials.UnitTest
{
    [TestClass]
    public class ClientCredentialsHandlerTest
    {
        private const string ACCOUNT_ID = "fake.account.id";
        private const string DOMAIN = "fake.domain";
        private const string CLIENT_ID = "fake.client.id";
        private const string SERVICE_PRINCIPAL_KEY = "fake.sp.key";
        private const string ACCESS_KEY = @"{
	            ""kty"": ""EC"",
                ""crv"": ""P-256"",
                ""use"": ""sig"",
	            ""kid"": ""YbcQaVGKoqiSmD2LwIrNRWk2y10oLYqDN5rymQyafwc"",
	            ""x"": ""oO6bmvSrJmSVzw72aJdKdH08Rw3LOKBsbN8-p9e-i2I"",
	            ""y"": ""TSg5da4l2ThYI__W34_3rLoUyoAZ-atb4cCELHTcstM"",
	            ""d"": ""Q2J9YzSI_p98uMlt-MvFAi5VkzcFzQ-ThE2VRtv1g-Y""
            }";

        IClientCredentialsHandler handler;
        Mock<IHttpClientFactory> mockHttpClientFactory;
        Mock<HttpMessageHandler> mockHttpMessageHandler;
        Mock<IClientCredentialsOptions> mockConfig;
        HttpClient httpClient;
        JsonWebKey accessKey;

        [TestInitialize]
        public void Setup()
        {
            mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockConfig = new Mock<IClientCredentialsOptions>();
            httpClient = new HttpClient(mockHttpMessageHandler.Object);
            accessKey = new JsonWebKey(ACCESS_KEY);

            // Populate the handler configuration with fake information
            mockConfig.Setup(mock => mock.AccountId).Returns(ACCOUNT_ID);
            mockConfig.Setup(mock => mock.Domain).Returns(DOMAIN);
            mockConfig.Setup(mock => mock.ClientId).Returns(CLIENT_ID);
            mockConfig.Setup(mock => mock.ServicePrincipalKey).Returns(SERVICE_PRINCIPAL_KEY);
            mockConfig.Setup(mock => mock.AccessKey).Returns(accessKey);

            // When called, it gives a stubbed HttpClient
            mockHttpClientFactory.Setup(mock => mock.CreateClient(It.IsAny<string>())).Returns(httpClient);
        }

        [TestMethod]
        public void GetAccessTokenAsync_Success()
        {
            var accessTokenResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent("{\"access_token\":\"fake.access.token\",\"expires_in\":1001,\"token_type\":\"bearer\"}")
            };

            // Accommodate the request to get access token:
            // We expect the path is for token request.
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(accessTokenResponse);

            handler = new ClientCredentialsHandler(mockConfig.Object, mockHttpClientFactory.Object);
            Assert.IsNotNull(handler.GetAccessTokenAsync());
        }

        [TestMethod]
        public async Task GetAccessTokenAsync_ExceptionResponse()
        {
            var statusCode = System.Net.HttpStatusCode.BadRequest;
            var responseContent = new OAuthProblemDetails()
            {
                Type = "invalid_client",
                Title = "The client_id is invalid or authentication failed.",
                Status = (int)statusCode,
                Instance = "/Token",
                OperationId = "fake.operation.id",
            };
            var accessTokenResponse = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(JsonConvert.SerializeObject(responseContent))
            };

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(accessTokenResponse);

            handler = new ClientCredentialsHandler(mockConfig.Object, mockHttpClientFactory.Object);
            var exception = await Assert.ThrowsExceptionAsync<Exception>(async () => await handler.GetAccessTokenAsync());
            Assert.AreEqual(exception.Data["Type"], responseContent.Type);
            Assert.AreEqual(exception.Data["Title"], responseContent.Title);
            Assert.AreEqual(exception.Data["Status"], ((int)statusCode).ToString());
            Assert.AreEqual(exception.Data["Instance"], responseContent.Instance);
            Assert.AreEqual(exception.Data["OperationId"], responseContent.OperationId);
        }

        [TestMethod]
        public void RefreshAccessTokenAsync_Success()
        {
            var accessTokenResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent("{\"access_token\":\"fake.access.token\",\"expires_in\":1001,\"token_type\":\"bearer\"}")
            };

            // Accommodate the request to get access token:
            // We expect the path is for token request.
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(accessTokenResponse);

            handler = new ClientCredentialsHandler(mockConfig.Object, mockHttpClientFactory.Object);
            Assert.IsNotNull(handler.RefreshAccessTokenAsync(""));
        }

        [TestMethod]
        public async Task RefreshAccessTokenAsync_ExceptionResponse()
        {
            var statusCode = System.Net.HttpStatusCode.BadRequest;
            var responseContent = new OAuthProblemDetails()
            {
                Type = "invalid_client",
                Title = "The client_id is invalid or authentication failed.",
                Status = (int)statusCode,
                Instance = "/Token",
                OperationId = "fake.operation.id",
            };
            var accessTokenResponse = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(JsonConvert.SerializeObject(responseContent))
            };

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(accessTokenResponse);

            handler = new ClientCredentialsHandler(mockConfig.Object, mockHttpClientFactory.Object);
            var exception = await Assert.ThrowsExceptionAsync<Exception>(async () => await handler.RefreshAccessTokenAsync(""));
            Assert.AreEqual(exception.Data["Type"], responseContent.Type);
            Assert.AreEqual(exception.Data["Title"], responseContent.Title);
            Assert.AreEqual(exception.Data["Status"], ((int)statusCode).ToString());
            Assert.AreEqual(exception.Data["Instance"], responseContent.Instance);
            Assert.AreEqual(exception.Data["OperationId"], responseContent.OperationId);
        }
    }
}
