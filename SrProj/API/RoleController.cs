using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Models;
using SrProj.API.Responses;
using Utility.Attribute;
using Utility.Enum;
using Utility.ExtensionMethod;

namespace SrProj.API
{
    public class RoleController : ApiController
    {
        [AuthorizableAction]
        public HttpResponseMessage GetRoles()
        {
            var response = new ApiResponse(Request);

            List<string> roles = new List<string>();
            foreach (RoleID role in Enum.GetValues(typeof (RoleID)))
            {
                roles.Add(role.GetEnumAttribute<EnumDecorators.Name>().name);
            }

            response.data = roles;

            return response.GenerateResponse(HttpStatusCode.OK);
        }
    }
}