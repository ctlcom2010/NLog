using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NLog.UnitTests.Mocks
{
    //public class WebRequestMock : WebRequest
    //{
    //    public Uri RequestedAddress { get; set; }

    //    public MemoryStream WrittenStream = new MemoryStream();

    //    #region Overrides of WebRequest

    //    /// <inheritdoc />
    //    public override IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
    //    {
    //        return base.BeginGetResponse(callback, state);
    //    }

    //    /// <inheritdoc />
    //    public override WebResponse EndGetResponse(IAsyncResult asyncResult)
    //    {
    //        return base.EndGetResponse(asyncResult);
    //    }

    //    /// <inheritdoc />
    //    public override IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
    //    {
    //        return base.BeginGetRequestStream(callback, state);
    //    }

    //    /// <inheritdoc />
    //    public override Stream EndGetRequestStream(IAsyncResult asyncResult)
    //    {
    //        return WrittenStream;
    //    }

    //    #endregion
    //}

    class WebRequestMock : WebRequest
    {
        readonly MemoryStream _requestStream = new MemoryStream();
        readonly MemoryStream _responseStream;

        public override string Method { get; set; }
        public override string ContentType { get; set; }
        public override long ContentLength { get; set; }
        public Uri RequestedAddress { get; set; }

        public WebRequestMock(string response)
        {
            _responseStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(response));
        }

        public string ContentAsString()
        {
            return System.Text.Encoding.UTF8.GetString(_requestStream.ToArray());
        }

        public override Stream GetRequestStream()
        {
            return _requestStream;
        }

        public override WebResponse GetResponse()
        {
            return new WebReponseMock(_responseStream);
        }
    }

    class WebReponseMock : WebResponse
    {
        readonly Stream _responseStream;

        public WebReponseMock(Stream responseStream)
        {
            _responseStream = responseStream;
        }

        public override Stream GetResponseStream()
        {
            return _responseStream;
        }
    }

}
