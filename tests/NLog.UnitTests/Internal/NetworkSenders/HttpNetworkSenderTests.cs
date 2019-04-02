﻿using System;
using System.Linq;
using System.Security.Authentication;
using NLog.Config;
using NLog.Internal.NetworkSenders;
using NLog.Targets;
using NLog.UnitTests.Mocks;
using Xunit;

namespace NLog.UnitTests.Internal.NetworkSenders
{
    public class HttpNetworkSenderTests : NLogTestBase
    {
        [Fact]
        public void HttpHappyPathTest()
        {
            // Arrange
            var networkTarget = new NetworkTarget("target1")
            {
                Address = "http://test.with.mock",
                Layout = "${logger}|${message}|${exception}"
            };
            var senderFactoryWithHttpMocks = new SenderFactoryWithHttpMocks();
            networkTarget.SenderFactory = senderFactoryWithHttpMocks;

            var logFactory = new LogFactory();
            var config = new LoggingConfiguration(logFactory);
            config.AddRuleForAllLevels(networkTarget);
            logFactory.Configuration = config;

            var logger = logFactory.GetLogger("HttpHappyPathTestLogger");

            // Act
            logger.Info("test message1");
            logFactory.Flush();

            // Assert
            var mock = senderFactoryWithHttpMocks.WebRequestMock;
            var requestedString = mock.GetRequestContentAsString();

            Assert.Equal("http://test.with.mock/", mock.RequestedAddress.ToString());
            Assert.Equal("HttpHappyPathTestLogger|test message1|",requestedString);

        }

    }

    internal class SenderFactoryWithHttpMocks : INetworkSenderFactory
    {
        #region Implementation of INetworkSenderFactory

        /// <inheritdoc />
        public NetworkSender Create(string url, int maxQueueSize, SslProtocols sslProtocols, TimeSpan keepAliveTime)
        {
            return new HttpNetworkSender(url)
            {
                WebRequestFactory = new WebRequestFactoryMock(WebRequestMock)
            };
        }

        public WebRequestMock WebRequestMock { get; } = new WebRequestMock();

        #endregion
    }
}
