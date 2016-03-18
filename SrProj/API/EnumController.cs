
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SrProj.API.Responses;
using Utility.Enum;
using Utility.ExtensionMethod;

namespace SrProj.API
{
    public class EnumController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetEnums()
        {
            ApiResponse response = new ApiResponse(Request)
            {
                data = new
                {
                    Services = Enum.GetValues(typeof(ServiceTypeID)).ToList().Select(EnumViewModel.ParseEnum),
                    Ethnicities = Enum.GetValues(typeof(EthnicityID)).ToList().Select(EnumViewModel.ParseEnum),
                    Genders = Enum.GetValues(typeof(GenderID)).ToList().Select(EnumViewModel.ParseEnum),
                    MaritalStatuses = Enum.GetValues(typeof(MaritalStatusID)).ToList().Select(EnumViewModel.ParseEnum),
                    ResidenceStatuses = Enum.GetValues(typeof(ResidenceStatusID)).ToList().Select(EnumViewModel.ParseEnum)
                }
            };
            return response.GenerateResponse(HttpStatusCode.OK);
        }
    }
}
