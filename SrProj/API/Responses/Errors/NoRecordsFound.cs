
using System;

namespace SrProj.API.Responses.Errors
{
    public class NoRecordsFound : JsonError
    {
        public NoRecordsFound(Exception e = null)
        {
            this.code = "No Records Found";
            this.detail = "No entries found in the system!";
            this.source = e;
        }
    }
}