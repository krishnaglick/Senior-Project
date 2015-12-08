
using System.Net;

namespace SrProj.API.Responses.Errors
{
    public class InvalidToken : JsonError
    {
        public InvalidToken()
        {
            this.code = "Invalid Token";
            this.detail = "The authentication token information provided in the request was incorrect.";
            this.id = 4;
        }
    }
}