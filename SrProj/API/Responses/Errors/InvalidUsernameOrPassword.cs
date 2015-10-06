
namespace SrProj.API.Responses.Errors
{
    public class InvalidUsernameOrPassword : JsonError
    {
        public InvalidUsernameOrPassword()
        {
            this.code = "Invalid Username or Password";
            this.detail = "The provided username or password was incorrect.";
            this.id = 2;
        }
    }
}