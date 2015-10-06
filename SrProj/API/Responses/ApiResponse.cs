using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using WebGrease.Css.Extensions;

namespace SrProj.API.Responses
{
    public class JsonError
    {
        private static Dictionary<int, int> idTracker = new Dictionary<int, int>();
        private int _id = -1;
        public int id
        {
            get { return _id; }
            set
            {
                if (_id == -1)
                {
                    _id = value;
                    if (!idTracker.ContainsKey(value))
                        idTracker[value] = value;
                    else
                        throw new Exception("ID in use!");
                }
            }
        }

        public string code { get; set; }
        public string title { get; set; }
        public string detail { get; set; }
        public HttpStatusCode status { get; set; }
        public Exception source { get; set; }
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