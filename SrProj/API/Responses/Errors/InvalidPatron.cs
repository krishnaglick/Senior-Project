
namespace SrProj.API.Responses.Errors
{
    public class InvalidPatron : JsonError
    {
        //TODO: Find a way to enforce the ID's automatically. May have to go factory or enum here. :/
        public InvalidPatron()
        {
            this.code = "Invalid Input";
            this.detail = "There was something wrong with the provided patron";
            this.id = 0;
        }
    }
}