
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Ajax.Utilities;
using Models;
using Newtonsoft.Json;
using SrProj.API.Responses;
using SrProj.API.Responses.Errors;
using Utility.Enum;
using Utility.ExtensionMethod;
using Database = DataAccess.Contexts.Database;

namespace SrProj.API
{
    [AuthorizableController(new [] { RoleID.Volunteer })]
    public class PatronController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage FindPatron(dynamic data)
        {
            DateTime PatronDoB = DateTime.MinValue;
            try
            {
                DateTime.TryParse(data.dateOfBirth, out PatronDoB);
            }
            catch(Exception e) { }
            Patron searchData = new Patron
            {
                FirstName = data.firstName,
                MiddleName = data.middleName,
                LastName = data.lastName,
                DateOfBirth = PatronDoB
            };
            ApiResponse response = new ApiResponse(Request);
            try
            {
                var patronContext = new Database();
                var patrons = patronContext.Patrons.Where(
                    p =>
                        p.FirstName.ToLower().Contains(searchData.FirstName.ToLower()) ||
                        p.MiddleName.ToLower().Contains(searchData.MiddleName.ToLower()) ||
                        p.LastName.ToLower().Contains(searchData.LastName.ToLower()) ||
                        p.DateOfBirth.ToString().Contains(searchData.DateOfBirth.ToString()));

                response.data = patrons;
                return response.GenerateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                response.errors.Add(new InvalidPatron { source = e });
                return response.GenerateResponse(HttpStatusCode.BadRequest);
            }
        }

        public class PatronViewModel : Patron
        {
            public bool NecessaryPaperwork { get; set; }
            public int ServiceSelection { get; set; }

            public MaritalStatusID maritalStatusID { get; set; }
            public GenderID genderID { get; set; }
            public EthnicityID ethnicityID { get; set; }
            public ResidenceStatusID residenceStatusID { get; set; }
        }

        [HttpPost]
        public HttpResponseMessage CheckIn(PatronViewModel checkIn)
        {
            ApiResponse response = new ApiResponse(Request);
            if (checkIn == null)
            {
                response.errors.Add(new NullRequest());
                return response.GenerateResponse(HttpStatusCode.BadRequest);
            }
            try
            {
                var database = new Database();
                var volunteerName = Request.Headers.GetHeaderValue("username");
                var volunteer = database.Volunteers.FirstOrDefault(v => v.Username == volunteerName);
                var serviceType = database.ServiceTypes.FirstOrDefault(st => st.ID == checkIn.ServiceSelection);
                var dumb = JsonConvert.DeserializeObject<Patron>(JsonConvert.SerializeObject(checkIn));
                dumb.Ethnicity = database.Ethnicities.FirstOrDefault(et => et.ID == (int) checkIn.ethnicityID);
                dumb.Gender = database.Genders.FirstOrDefault(gt => gt.ID == (int) checkIn.genderID);
                dumb.Residence = database.ResidenceStatuses.FirstOrDefault(rt => rt.ID == (int) checkIn.residenceStatusID);
                dumb.MaritalStatus = database.MaritalStatuses.FirstOrDefault(mt => mt.ID == (int) checkIn.maritalStatusID);
                
                database.Patrons.AddOrUpdate(dumb);
                database.SaveChanges();
                Visit visit = new Visit
                {
                    CreateVolunteer = volunteer,
                    Service = serviceType,
                    Patron = database.Patrons.FirstOrDefault(p =>
                        p.FirstName == checkIn.FirstName &&
                        p.LastName == checkIn.LastName &&
                        p.MiddleName == checkIn.MiddleName &&
                        p.DateOfBirth == checkIn.DateOfBirth
                    )
                };
                database.Visits.Add(visit);
                database.SaveChanges();
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
