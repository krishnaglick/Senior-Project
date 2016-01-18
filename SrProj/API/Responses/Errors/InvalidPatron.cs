
namespace SrProj.API.Responses.Errors
{
    public class InvalidPatron : JsonError
    {
        public InvalidPatron()
        {
            this.code = "Invalid Input";
            this.detail = "There was something wrong with the provided patron";
            this.id = 0;
        }
    }
}