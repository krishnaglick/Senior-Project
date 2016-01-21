
using System.Net.Http;
using System.Web.Http;

namespace SrProj.API
{
    [AuthorizableController]
    public class ReportingController : ApiController
    {
        public HttpResponseMessage FilterData()
        {
            //I dunno what I want to do with this yet.
            return null;
        }
    }
}