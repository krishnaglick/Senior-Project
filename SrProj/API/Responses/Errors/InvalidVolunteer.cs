
namespace SrProj.API.Responses.Errors
{
    public class InvalidVolunteer : JsonError
    {
        public InvalidVolunteer()
        {
            this.code = "Invalid Volunteer";
            this.detail = "There was something wrong with the provided volunteer.";
        }
    }
}