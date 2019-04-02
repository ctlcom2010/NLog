using System;
using System.Net;
using NLog.Internal.NetworkSenders;

namespace NLog.UnitTests.Mocks
{
    class WebRequestFactoryMock : IWebRequestFactory
    {
        #region Implementation of IWebRequestFactory

        private readonly WebRequestMock _mock;

        /// <inheritdoc />
        public WebRequestFactoryMock(WebRequestMock mock)
        {
            _mock = mock;
        }
        
        /// <inheritdoc />
        public WebRequest CreateWebRequest(Uri address)
        {
            _mock.RequestedAddress = address;
            return _mock;
        }

        #endregion
    }
}