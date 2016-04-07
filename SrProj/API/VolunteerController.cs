
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
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace SrProj.API
{
    [AuthorizableController(new[] { RoleID.Admin })]
    public class VolunteerController : ApiController
    {
        public class CreateVolunteerViewModel : VolunteerViewModel
        {
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
        }

        [HttpPost]
        public HttpResponseMessage CreateVolunteer([FromBody] CreateVolunteerViewModel volunteer)
        {
            ApiResponse response = new ApiResponse(Request);

            if (volunteer.Password != volunteer.ConfirmPassword) {
                response.data = new PasswordMismatch();
                return response.GenerateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                using (var dbContext = new Database())
                {
                    var roles = dbContext.Roles.Where(r => volunteer.Roles.Contains(r.ID));
                    var services = dbContext.ServiceTypes.Where(st => volunteer.Services.Contains(st.ID));

                    var newVolunteer = new Volunteer
                    {
                        FirstName = volunteer.FirstName,
                        LastName = volunteer.LastName,
                        Username = volunteer.Username,
                        Email = volunteer.Email ?? "",
                        Password = volunteer.Password,
                        ServiceTypes = new List<ServiceType>()
                    };

                    roles.ForEach(r => dbContext.RoleVolunteers.Add(
                        new RoleVolunteer
                        {
                            Role = r,
                            Volunteer = newVolunteer
                        })
                    );

                    services.ForEach(s => newVolunteer.ServiceTypes.Add(s));

                    newVolunteer.SecurePassword();

                    dbContext.Volunteers.Add(newVolunteer);

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
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public int[] Roles { get; set; }
            public int[] Services { get; set; }
        }

        [HttpPost]
        public HttpResponseMessage ModifyVolunteer([FromBody] VolunteerViewModel volunteer)
        {
            using (var db = new Database())
            {
                //Get Volunteer
                var dbVolunteer = db.Volunteers
                    .Include(v => v.ServiceTypes)
                    .FirstOrDefault(v => v.Username == volunteer.Username);
                //Get Roles
                var dbRoles = db.Roles.Where(r => volunteer.Roles.Contains(r.ID));
                //Remove all current roles
                db.RoleVolunteers.Where(rv => rv.Volunteer.Username == volunteer.Username)
                    .ForEach(rv => db.RoleVolunteers.Remove(rv));
                //Get Services
                var dbServices = db.ServiceTypes.Where(st => volunteer.Services.Contains(st.ID));
                //Remove all current services
                db.Database.ExecuteSqlCommand(
                    @"DELETE VolunteerServiceType
                      WHERE Volunteer_Username = @p0" //EF IS AWFUL
                , volunteer.Username);

                //Associate volunteer to new roles
                dbRoles.ForEach(r => db.RoleVolunteers.Add(new RoleVolunteer
                {
                    Role = r,
                    Volunteer = dbVolunteer
                }));
                //Associate volunteer to new services
                dbVolunteer.ServiceTypes.Clear();
                dbServices.ForEach(s => dbVolunteer.ServiceTypes.Add(s));

                //Set First Name, Last Name, Email
                dbVolunteer.FirstName = volunteer.FirstName;
                dbVolunteer.LastName = volunteer.LastName;
                dbVolunteer.Email = volunteer.Email;

                db.SaveChanges();

                return new ApiResponse(Request)
                {
                    data = ApiResponse.DefaultSuccessResponse
                }
                .GenerateResponse(HttpStatusCode.OK);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetVolunteers()
        {
            using (var db = new Database())
            {
                var volunteers = db.Volunteers
                .Include(v => v.Roles)
                .Select(v => new {
                    username = v.Username,
                    firstName = v.FirstName,
                    lastName = v.LastName,
                    email = v.Email,
                    roles = v.Roles.Select(vr => vr.Role),
                    services = v.ServiceTypes.Select(st => st)
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

        public class ChangePasswordViewModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
        }

        [HttpPost]
        public HttpResponseMessage ChangePassword(ChangePasswordViewModel volunteer)
        {
            using (var db = new Database())
            {
                var response = new ApiResponse(Request);

                if (volunteer.Password != volunteer.ConfirmPassword)
                {
                    response.errors.Add(new PasswordMismatch());
                    return response.GenerateResponse(HttpStatusCode.BadRequest);
                }

                var dbVolunteer = db.Volunteers.FirstOrDefault(v => v.Username == volunteer.Username);
                if (dbVolunteer == null)
                {
                    response.errors.Add(new NoRecordsFound());
                    return response.GenerateResponse(HttpStatusCode.InternalServerError);
                }
                try
                {
                    dbVolunteer.Password = volunteer.Password;
                    dbVolunteer.SecurePassword();
                    //Fuck you EF.
                    db.Database.ExecuteSqlCommand(@"
                        UPDATE Volunteer
                        SET HashedPassword = @p0
                        WHERE Username = @p1
                    ", dbVolunteer.HashedPassword, volunteer.Username);
                }
                catch (Exception e)
                {
                    response.errors.Add(new DatabaseFailure(e));
                    return response.GenerateResponse(HttpStatusCode.InternalServerError);
                }
                response.data = ApiResponse.DefaultSuccessResponse;
                return response.GenerateResponse(HttpStatusCode.OK);
            }
        }
    }
}
