using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NLog.UnitTests.Mocks
{
    public class WebRequestMock : WebRequest
    {
        public Uri RequestedAddress { get; set; }

        public MemoryStream WrittenStream = new MemoryStream();



        #region Overrides of WebRequest

        ///// <inheritdoc />
        //public override IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
        //{
        //    return new AsyncResultMock(state);
        //}

        /// <inheritdoc />
        public override WebResponse EndGetResponse(IAsyncResult asyncResult)
        {
            var responseStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes("new response 1"));
            return new WebReponseMock(responseStream);
        }

        /// <inheritdoc />
        public override IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
        {
          
            var result = new AsyncResultMock(state);
            callback(result);
            return result;
        }

        /// <inheritdoc />
        public override Stream EndGetRequestStream(IAsyncResult asyncResult)
        {
            return WrittenStream;
        }


        public override IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
        {
            var result = new AsyncResultMock(state);
            callback(result);
            return result;
            //Task<WebResponse> f = Task<WebResponse>.Factory.StartNew(
            //    c =>
            //    {
            //        var responseStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes("new response 2"));
            //        return new WebReponseMock(responseStream);
            //    },
            //    state
            //);

            //if (callback != null) f.ContinueWith((res) => callback(f));
            //return f;
        }
        //public override WebResponse EndGetResponse(IAsyncResult asyncResult)
        //{
        //    return ((Task<WebResponse>)asyncResult).Result;
        //}

        /// <inheritdoc />
        public override string Method { get; set; }

        #endregion

        public string GetRequestContentAsString()
        {
            return System.Text.Encoding.UTF8.GetString(WrittenStream.ToArray());
        }
        public string GetResponseContentAsString()
        {
            return "todo";
            //return System.Text.Encoding.UTF8.GetString(_responseStream.ToArray());
        }
    }

    class AsyncResultMock : IAsyncResult
    {
        public AsyncResultMock(object state)
        {
            AsyncState = state;
        }

        #region Implementation of IAsyncResult

        /// <inheritdoc />
        public object AsyncState { get; set; }

        /// <inheritdoc />
        public WaitHandle AsyncWaitHandle { get; set; }

        /// <inheritdoc />
        public bool CompletedSynchronously { get; set; }

        /// <inheritdoc />
        public bool IsCompleted { get; set; }

        #endregion
    }

    //class WebRequestMock : WebRequest
    //{
    //    readonly MemoryStream _requestStream = new MemoryStream();
    //    readonly MemoryStream _responseStream;

    //    public override string Method { get; set; }
    //    public override string ContentType { get; set; }
    //    public override long ContentLength { get; set; }
    //    public Uri RequestedAddress { get; set; }

    //    public WebRequestMock(string response)
    //    {
    //        _responseStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(response));
    //    }

    //    public string GetRequestContentAsString()
    //    {
    //        return System.Text.Encoding.UTF8.GetString(_requestStream.ToArray());
    //    }  
    //    public string GetResponseContentAsString()
    //    {
    //        return System.Text.Encoding.UTF8.GetString(_responseStream.ToArray());
    //    }

    //    public override Stream GetRequestStream()
    //    {
    //        return _requestStream;
    //    }

    //    public override WebResponse GetResponse()
    //    {
    //        return new WebReponseMock(_responseStream);
    //    }
    //}

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
