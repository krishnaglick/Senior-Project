using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace Utility.ExtensionMethod
{
    public static class HttpRequestHeadersExtentions
    {
        public static string GetHeaderValue(this HttpRequestHeaders headers, string header)
        {
            IEnumerable<string> headerValue;
            headers.TryGetValues(header, out headerValue);
            if (headerValue == null) return string.Empty;
            return headerValue.SingleOrDefault();
        }
    }
}