
using System;
using System.Collections.Generic;
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
                    volunteer.Roles.Add(new RoleVolunteer
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
            //This is not the right way.
            var db = new Database();
            var dbVolunteer = db.Volunteers.Include(v => v.Roles).First(v => v.Username == volunteer.Username);

            db.RoleVolunteers.Where(rv => rv.Volunteer.Username == volunteer.Username).ForEach(rv => db.RoleVolunteers.Remove(rv));
            db.Roles.Include(r => r.Volunteers)
            .Where(r => volunteer.Roles.Contains(r.ID)).ForEach(r =>
            {
                r.Volunteers = r.Volunteers ?? new List<RoleVolunteer>();
                r.Volunteers.Add(new RoleVolunteer
                    {
                        Role = r,
                        Volunteer = dbVolunteer
                    });
            });

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                var TT = Convert.ChangeType(e, e.GetType());
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
                            id = r.Role.ID,
                            name = r.Role.RoleName,
                            description = r.Role.RoleDescription
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
