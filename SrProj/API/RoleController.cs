using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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

            List<EnumViewModel> roles = new List<EnumViewModel>();
            foreach (RoleID role in Enum.GetValues(typeof(RoleID)))
            {
                roles.Add(new EnumViewModel
                {
                    id = (int)role,
                    name = role.GetEnumAttribute<EnumDecorators.Name>().name,
                    description = role.GetEnumAttribute<EnumDecorators.Description>().desc
                });
            }

            response.data = new { roles = roles };

            return response.GenerateResponse(HttpStatusCode.OK);
        }
    }
}