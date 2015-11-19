using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using DataAccess.Contexts;
using Models;
using SrProj.API.Responses;
using SrProj.API.Responses.Errors;


namespace SrProj.API
{
    public class VisitController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage CreateVisit(Visit visit)
        {
            ApiResponse response = new ApiResponse(Request);
            try
            {
                var visitContext = new Database();
                visitContext.Visits.Add(visit);
                visitContext.SaveChanges();

                response.data = response.DefaultSuccessResponse;
                return response.GenerateResponse(HttpStatusCode.Created);

            }
            catch (Exception)
            {

                return response.GenerateResponse(HttpStatusCode.BadRequest);
            }
        }

        public IEnumerable<Visit> Get()
        {
            using (var db = new VisitContext())
            {
                return db.Visits.ToList();
            }
        }
        public Visit GetVisit([FromUri] int visitId)
        {
            return new VisitContext().Visits.Find(visitId);
        }

    }

}