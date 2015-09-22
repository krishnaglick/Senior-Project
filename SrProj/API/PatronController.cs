
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SrProj.API.Response;
using SrProj.Models;

namespace SrProj.API
{
    public class PatronController : ApiController
    {
        [HttpGet]
        public Patron Get([FromUri] int patronID)
        {
            return new PatronContext().Patrons.Find(patronID);
        }

        [HttpGet]
        public Patron[] Search([FromUri] string namePartial)
        {
            namePartial = namePartial.ToLower();
            return
                new PatronContext().Patrons.Where(
                    p => p.FirstName.ToLower().Contains(namePartial) || p.LastName.ToLower().Contains(namePartial)).ToArray();
        }

        [HttpPost]
        public HttpResponseMessage Create(Patron patron)
        {
            ApiResponse response = new ApiResponse(Request);
            try
            {
                patron.CreateDate = DateTime.Now;
                var patronContext = new PatronContext();
                patronContext.Patrons.Add(patron);
                patronContext.SaveChanges();

                response.data = response.DefaultSuccessResponse;
                return response.GenerateResponse(HttpStatusCode.Created);
            }
            catch(Exception e)
            {
                //TODO: Put this in its own error.
                response.errors.Add(new JsonError
                {
                    code = "Invalid Input",
                    detail = "There was something wrong with the given patron",
                    id = 0,
                    source = e
                });
                return response.GenerateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
