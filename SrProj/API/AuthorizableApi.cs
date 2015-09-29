using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using SrProj.API.Responses;
using SrProj.Models.Context;

namespace SrProj.API
{
    //TODO: Make this work.
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthorizableRoute : Attribute { }

    public class AuthorizableApi : ApiController
    {
        public override Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            /* Add functionality here to secure the API */
            //TODO: Need to use TryGetValue.
            /*var authToken = new Database().AuthenticationTokens.Find(controllerContext.Request.Headers.GetValues("authToken"));
            if (authToken == null)
            {
                var response = new ApiResponse(controllerContext.Request);
                response.errors.Add(new JsonError{code = "Bad!"});

                response.GenerateResponse(HttpStatusCode.Unauthorized);
            }*/
            return base.ExecuteAsync(controllerContext, cancellationToken);
        }
    }
}