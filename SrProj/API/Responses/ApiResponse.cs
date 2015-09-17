using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using WebGrease.Css.Extensions;

namespace SrProj.API.Response
{
    public class JsonError
    {
        public int id;
        public string code;
        public string title;
        public string detail;
        public HttpStatusCode status;
        public Exception source;
    }

    public class ApiResponse
    {
        public object DefaultSuccessResponse = new { result = "success" };

        public ApiResponse(HttpRequestMessage Request)
        {
            errors = new List<JsonError>();
            request = Request;
        }

        public List<JsonError> errors { get; set; }
        public object data { get; set; }

        private HttpRequestMessage request;
        public HttpResponseMessage GenerateResponse(HttpStatusCode code, Dictionary<string, string> headers = null)
        {
            HttpResponseMessage res = request.CreateResponse(code, data ?? errors);

            if (headers != null)
            {
                headers.ForEach(header => res.Headers.Add(header.Key, header.Value));
            }

            return res;
        }
    }
}