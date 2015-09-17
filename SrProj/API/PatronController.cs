
using System;
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

        [HttpPost]
        public Patron Search(Patron patron)
        {
            return new PatronContext().Patrons.Single(p => p.Equals(patron));
        }

        [HttpPost]
        public HttpResponseMessage Create(Patron patron)
        {
            ApiResponse response = new ApiResponse(Request);
            try
            {
                new PatronContext().Patrons.Add(patron);
                response.data = response.DefaultSuccessResponse;
                return response.GenerateResponse(HttpStatusCode.OK);
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
