
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models;
using SrProj.API.Responses;
using SrProj.API.Responses.Errors;
using Utility.Enum;
using WebGrease.Css.Extensions;
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
                using (var dbContext = new Database())
                {
                    dbContext.RoleVolunteers.Add(new RoleVolunteer
                    {
                        Role = dbContext.Roles.First(r => r.ID == (int)RoleID.Volunteer),
                        Volunteer = volunteer
                    });
                    dbContext.Volunteers.Add(volunteer);
                    dbContext.SaveChanges();

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

        public class VolunteerViewModel
        {
            public string Username { get; set; }
            public int[] Roles { get; set; }
        }

        [HttpPost]
        [AuthorizableAction]
        public HttpResponseMessage ModifyVolunteer([FromBody] VolunteerViewModel volunteer)
        {
            var db = new Database();

            //Get Volunteer
            var dbVolunteer = db.Volunteers.FirstOrDefault(v => v.Username == volunteer.Username);
            //Get Roles
            var dbRoles = db.Roles.Where(r => volunteer.Roles.Contains(r.ID));
            //Remove all current roles
            db.RoleVolunteers.Where(rv => rv.Volunteer.Username == volunteer.Username)
                .ForEach(rv => db.RoleVolunteers.Remove(rv));
            //Associate user to new roles
            dbRoles.ForEach(r => db.RoleVolunteers.Add(new RoleVolunteer
            {
                Role = r,
                Volunteer = dbVolunteer
            }));

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                //var TT = Convert.ChangeType(e, e.GetType());
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
            var db = new Database();

            var volunteers = db.Volunteers
                .Include(v => v.Roles)
                .Select(v => new {
                    volunteer = v,
                    roles = v.Roles.Select(vr => vr.Role)
                })
                .ToList();

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
