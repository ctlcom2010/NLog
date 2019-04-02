using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
