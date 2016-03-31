
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models;
using SrProj.API.Responses;
using Utility.ExtensionMethod;
using System.Data.Entity;
using Database = DataAccess.Contexts.Database;

namespace SrProj.API
{
    [AuthorizableController]
    public class ReportingController : ApiController
    {
        public class ReportingServiceViewModel
        {
            public bool AndSearch { get; set; }
            public int ZipCode { get; set; }
            public ServiceType[] ServiceTypeSelections { get; set; }
            public string TimePeriod { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }

        [HttpPost]
        public HttpResponseMessage GetServiceData(ReportingServiceViewModel reportingParams)
        {
            ApiResponse response = new ApiResponse(Request);
            using(var db = new Database())
            {
                if (reportingParams.TimePeriod == "Today")
                {
                    //TODO: Make sure to use UtcNow EVERYWHERE!!
                    reportingParams.StartDate = DateTime.UtcNow.AbsoluteStart();
                    reportingParams.EndDate = DateTime.UtcNow.AbsoluteEnd();
                }

                var serviceTypeIDs = reportingParams.ServiceTypeSelections.Select(st => st.ID).ToList();

                var potato = db.Visits
                    .Include(v => v.Service)
                    .Include(v => v.Patron)
                    .Where(
                    v => serviceTypeIDs.Contains(v.Service.ID)
                        && (v.CreateDate >= reportingParams.StartDate && v.CreateDate < reportingParams.EndDate)
                //&& reportingParams.ZipCode == 0 || (reportingParams.ZipCode > 0 && v.Patron.Addresses.FirstOrDefault(a => a.Zip == reportingParams.ZipCode.ToString()) != null)
                );

                response.data = potato;

                return response.GenerateResponse(HttpStatusCode.OK);
            }
        }
    }
}