
using System.Net;

namespace SrProj.API.Responses.Errors
{
    public class NoAccess : JsonError
    {
        public NoAccess()
        {
            this.code = "Unauthorized Route or Controller";
            this.detail = "You don't have proper authorization to access the target route or controller.";
            this.id = 3;
            this.status = HttpStatusCode.Unauthorized;
        }
    }
}