using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace SrProj.API
{
    public class AuthorizableApi : ApiController
    {
        public override Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            /* Add functionality here to secure the API */
            return base.ExecuteAsync(controllerContext, cancellationToken);
        }
    }
}