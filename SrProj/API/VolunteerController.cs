
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Models;
using SrProj.API.Responses;
using SrProj.API.Responses.Errors;
using Utility.Enum;
using Database = DataAccess.Contexts.Database;

namespace SrProj.API
{
    public class VolunteerController : ApiController
    {
        [HttpPost]
        [AuthorizableAction]
        public HttpResponseMessage CreateVolunteer([FromBody] Volunteer volunteer)
        {
            ApiResponse response = new ApiResponse(Request);

            try
            {
                volunteer.SecurePassword();
                using (var volunteerContext = new Database())
                {
                    volunteer.Roles.Add(new Role{ ID = RoleID.Volunteer });
                    volunteerContext.Volunteers.Add(volunteer);
                    volunteerContext.SaveChanges();

                    response.data = response.DefaultSuccessResponse;
                    return response.GenerateResponse(HttpStatusCode.Created);
                }
            }
            catch (Exception e)
            {
                response.errors.Add(new InvalidVolunteer {source = e});
                return response.GenerateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [AuthorizableAction]
        public HttpResponseMessage GetVolunteers()
        {
            using(var volunteerContext = new Database())
            {
                var volunteers = volunteerContext.Volunteers.Include(v => v.Roles)
                    .Select(v => new {v.Username, v.Roles});
                var response = new ApiResponse(Request);

                if (volunteers.Any())
                {
                    response.data = new {volunteers = volunteers};
                    return response.GenerateResponse(HttpStatusCode.OK);
                }
                else
                {
                    response.errors.Add(new NoRecordsFound());
                    return response.GenerateResponse(HttpStatusCode.InternalServerError);
                }
            }
        }
    }
}
