
using System;

namespace SrProj.API.Responses.Errors
{
    public class InvalidCheckIn : JsonError
    {
        public InvalidCheckIn(Exception e = null)
        {
            this.code = "Invalid Check In";
            this.detail = "There was something wrong with the provided information";
            this.id = 6;
            this.source = e;
        }
    }
}