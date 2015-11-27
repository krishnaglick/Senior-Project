
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models;
using SrProj.API.Responses;
using SrProj.API.Responses.Errors;
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
    }
}
