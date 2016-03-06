
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SrProj.API.Responses;
using Utility.Enum;
using Utility.ExtensionMethod;

namespace SrProj.API
{
    public class RoleController : ApiController
    {
        [AuthorizableAction]
        public HttpResponseMessage GetRoles()
        {
            var response = new ApiResponse(Request)
            {
                data = new {roles = Enum.GetValues(typeof (RoleID)).ToList().Select(EnumViewModel.ParseEnum)}
            };

            return response.GenerateResponse(HttpStatusCode.OK);
        }
    }
}
