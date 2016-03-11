
using System;

namespace SrProj.API.Responses.Errors
{
    public class NullRequest : JsonError
    {
        public NullRequest(Exception e = null)
        {
            this.code = "NullRequestError";
            this.detail = "The request did not contain any data!";
            this.source = e;
        }
    }
}