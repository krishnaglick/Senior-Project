
using System;

namespace SrProj.API.Responses.Errors
{
    public class InvalidTimePeroid : JsonError
    {
        public InvalidTimePeroid(Exception e = null)
        {
            this.code = "Invalid Time Period";
            this.detail = "The provided time period was incorrect!";
            this.source = e;
        }
    }
}