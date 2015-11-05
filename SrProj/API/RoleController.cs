using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using DataAccess.Contexts;
using Models;
using SrProj.API.Responses;
using 
namespace SrProj.API
{
    public class RoleController : ApiController
    {
        /// <summary>
        /// Create a user role for the volunteer
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage CreateRole(Role role)
        {
            ApiResponse response = new ApiResponse(Request);
            var roleContext = new RoleContext();
            roleContext.Roles.Add(role);
            roleContext.SaveChanges();

            response.data = response.DefaultSuccessResponse;
            return response.GenerateResponse(HttpStatusCode.Created);
        }

        [HttpGet]
        public IEnumerable<Role> GetallRoles()
        {
            
        }

    }
}
