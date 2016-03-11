
using System;
using System.Globalization;
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
    [AuthorizableController(new [] { RoleID.Volunteer })]
    public class PatronController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage FindPatron(Patron searchData)
        {
            ApiResponse response = new ApiResponse(Request);
            if(searchData == null)
            {
                response.errors.Add(new NullRequest());
                return response.GenerateResponse(HttpStatusCode.BadRequest);
            }
            try
            {
                var patronContext = new Database();
                var patrons = patronContext.Patrons.Where(
                    p =>
                        p.FirstName.ToLower().Contains(searchData.FirstName.ToLower()) ||
                        p.MiddleName.ToLower().Contains(searchData.MiddleName.ToLower()) ||
                        p.LastName.ToLower().Contains(searchData.LastName.ToLower()) ||
                        p.DateOfBirth.ToString(CultureInfo.InvariantCulture).Contains(searchData.DateOfBirth.ToString(CultureInfo.InvariantCulture)));

                response.data = patrons;
                return response.GenerateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                response.errors.Add(new InvalidPatron { source = e });
                return response.GenerateResponse(HttpStatusCode.BadRequest);
            }
        }

        public class CheckInViewModel : Patron
        {

        }

        [HttpPost]
        public HttpResponseMessage CheckIn(CheckInViewModel visit)
        {
            ApiResponse response = new ApiResponse(Request);
            if (visit == null)
            {
                response.errors.Add(new NullRequest());
                return response.GenerateResponse(HttpStatusCode.BadRequest);
            }
            try
            {
                var checkInContext = new Database();
                return response.GenerateResponse(HttpStatusCode.Created);
            }
            catch(Exception e)
            {
                response.errors.Add(new InvalidCheckIn { source = e });
                return response.GenerateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
