
using System;

namespace SrProj.API.Responses.Errors
{
    public class DatabaseFailure : JsonError
    {
        public DatabaseFailure(Exception e = null)
        {
            this.code = "Database Failure";
            this.detail = "There was an issue when attempting to interact with the database.";
            this.source = e;
        }
    }
}