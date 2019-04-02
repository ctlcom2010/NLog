using System;
using System.Linq;
using System.Net;

namespace NLog.Internal.NetworkSenders
{
    internal interface IWebRequestFactory
    {
        WebRequest CreateWebRequest(Uri address);
    }

    class WebRequestFactory : IWebRequestFactory
    {
        public WebRequest CreateWebRequest(Uri address)
        {
            var webRequest = WebRequest.Create(address);
            return webRequest;
        }
    }
}
