
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models;
using SrProj.API.Responses;
using Utility.ExtensionMethod;
using System.Data.Entity;
using SrProj.API.Responses.Errors;
using Database = DataAccess.Contexts.Database;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace SrProj.API
{
  [AuthorizableController]
  public class ReportingController : ApiController
  {
    public class ReportingServiceViewModel
    {
      public string ZipCode { get; set; }

      public bool AndSearch { get; set; }
      public ServiceType[] ServiceTypeSelections { get; set; }

      public string TimePeriod { get; set; }
      public DateTime StartDate { get; set; }
      public DateTime EndDate { get; set; }
    }

    [HttpPost]
    public HttpResponseMessage GetServiceData(ReportingServiceViewModel reportingParams)
    {
      ApiResponse response = new ApiResponse(Request);
      using (var db = new Database())
      {
        if (reportingParams.TimePeriod == "Today")
        {
          reportingParams.StartDate = DateTime.UtcNow.AbsoluteStart();
          reportingParams.EndDate = DateTime.UtcNow.AbsoluteEnd();
        }
        else if (reportingParams.TimePeriod == "Specific Date")
        {
          reportingParams.StartDate = reportingParams.StartDate.ToUniversalTime().AbsoluteStart();
          reportingParams.EndDate = reportingParams.StartDate.ToUniversalTime().AbsoluteEnd();
        }
        else if (reportingParams.TimePeriod == "Date Range")
        {
          reportingParams.StartDate = reportingParams.StartDate.ToUniversalTime().AbsoluteStart();
          reportingParams.EndDate = reportingParams.EndDate.ToUniversalTime().AbsoluteEnd();
        }
        else if (reportingParams.TimePeriod == "All Time")
        {
          reportingParams.StartDate = DateTime.MinValue;
          reportingParams.EndDate = DateTime.MaxValue;
        }
        else
        {
          response.errors.Add(new InvalidTimePeroid());
          return response.GenerateResponse(HttpStatusCode.BadRequest);
        }

        var serviceTypeIDs = reportingParams.ServiceTypeSelections.Select(st => st.ID).ToList();

        var report = db.Visits
        .Include(v => v.Service)
        .Include(v => v.Patron)
        .Include(v => v.Patron.ResidenceStatus)
        .Include(v => v.Patron.MaritalStatus)
        .Include(v => v.Patron.Gender)
        .Include(v => v.Patron.Ethnicity)
        .Include(v => v.Patron.Addresses)
        .Include(v => v.CreateVolunteer)
        .Where(v =>
          v.CreateDate >= reportingParams.StartDate && v.CreateDate <= reportingParams.EndDate &&
          !string.IsNullOrEmpty(reportingParams.ZipCode) ? v.Patron.Addresses.Count(a => a.Zip.Contains(reportingParams.ZipCode) || reportingParams.ZipCode.Contains(a.Zip)) > 0 : true &&
          serviceTypeIDs.Contains(v.Service.ID)
        ).ToList()
        .Select(v => new
        {
          VisitDate = v.CreateDate,
          VolunteerLastName = v.CreateVolunteer.FirstName,
          VolunteerFirstName = v.CreateVolunteer.LastName,
          PatronFirstName = v.Patron.FirstName,
          PatronMiddleName = v.Patron.MiddleName,
          PatronLastName = v.Patron.LastName,
          Service = v.Service.ServiceName,
          MaritalStatus = v.Patron.MaritalStatus.Name,
          Gender = v.Patron.Gender.Name,
          Ethnicity = v.Patron.Ethnicity.Name,
          Addresses =  string.Join(" | ", v.Patron.Addresses.Select(a => $"{a.StreetAddress} {a.City} {a.State} {a.Zip}"))
        });


        response.data = report;

        return response.GenerateResponse(HttpStatusCode.OK);
      }
    }

    public class ReportingPatronViewModel
    {
      public int ID { get; set; }
      public List<ServiceType> ServiceTypeSelections { get; set; }
    }

    [HttpPost]
    public HttpResponseMessage GetPatronData(ReportingPatronViewModel reportingParams)
    {
      using (var db = new Database())
      {
        var response = new ApiResponse(Request);
        try
        {
          var servicesWanted = reportingParams.ServiceTypeSelections.Select(st => st.ID).ToArray();
          var services = db.Visits
            .Include(v => v.Patron)
            .Include(v => v.Service.ID)
            .Where(v => v.Patron.ID == reportingParams.ID && servicesWanted.Contains(v.Service.ID))
            .ToList();

          response.data = services;
          return response.GenerateResponse(HttpStatusCode.OK);
        }
        catch (Exception e)
        {
          response.errors.Add(new DatabaseFailure(e));
          return response.GenerateResponse(HttpStatusCode.InternalServerError);
        }
      }
    }
  }
}