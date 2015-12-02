
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
                    volunteer.Roles.Add(new Role{ ID = (int)RoleID.Volunteer });
                    volunteerContext.Volunteers.Add(volunteer);
                    volunteerContext.SaveChanges();

                    response.data = ApiResponse.DefaultSuccessResponse;
                    return response.GenerateResponse(HttpStatusCode.Created);
                }
            }
            catch (Exception e)
            {
                response.errors.Add(new InvalidVolunteer {source = e});
                return response.GenerateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [AuthorizableAction]
        public HttpResponseMessage ModifyVolunteer([FromBody] Volunteer volunteer)
        {
            //This is not the right way.
            var db = new Database();
            var vol = db.Volunteers.First(v => v.Username == volunteer.Username);
            vol.Roles = volunteer.Roles;
            try
            {
                db.Volunteers.AddOrUpdate(vol);
                db.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                
            }

            return new ApiResponse(Request)
            {
                data = ApiResponse.DefaultSuccessResponse
            }
            .GenerateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [AuthorizableAction]
        public HttpResponseMessage GetVolunteers()
        {
            var databaseContext = new Database();

            var volunteers =
                from v in databaseContext.Volunteers
                orderby v.Username descending
                select new
                {
                    username = v.Username,
                    roles = (
                        from r in v.Roles
                        select new
                        {
                            id = r.ID,
                            name = r.RoleName,
                            description = r.RoleDescription
                        }
                    )
                };

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
