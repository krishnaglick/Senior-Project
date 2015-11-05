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
            try
            {
                var response = new ApiResponse(Request);
                var roleContext = new RoleContext();
                roleContext.Roles.Add(role);
                roleContext.SaveChanges();

                response.data = response.DefaultSuccessResponse;
                return response.GenerateResponse(HttpStatusCode.Created);
            }
            catch (Exception)
            {
                var response = new ApiResponse(Request);
                return response.GenerateResponse(HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Retrieves all the roles from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Role> GetallRole()
        {
            return new RoleContext().Roles.ToList();
        }

        /// <summary>
        /// Retrieves a specific role by id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]
        public Role GetRole(int roleId)
        {
            return new RoleContext().Roles.Find(roleId);
        }

    }
}
