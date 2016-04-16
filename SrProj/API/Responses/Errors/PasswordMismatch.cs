
using System;

namespace SrProj.API.Responses.Errors
{
    public class PasswordMismatch : JsonError
    {
        public PasswordMismatch(Exception e = null)
        {
            this.code = "Passwords Mismatch";
            this.detail = "Provided passwords do not match.";
            this.source = e;
        }
    }
}