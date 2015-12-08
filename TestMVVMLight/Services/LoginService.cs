

using System;
using System.Linq;
using DataAccess.Contexts;
using Microsoft.AspNet.Identity;
using TestMVVMLight.Model;
using ILogin = TestMVVMLight.ViewModels.ILogin;

namespace TestMVVMLight.Services
{
    public interface ILoginService : IDataService
    {
        PasswordVerificationResult? Login(ILogin volunteer);
    }

    public class LoginService : ILoginService
    {
        public PasswordVerificationResult? Login(ILogin volunteer)
        {
            using (var context = new Database())
            {
                var loginUser = context.Volunteers.FirstOrDefault(v => v.Username == volunteer.Username);
                if (loginUser != null)
                {
                    return loginUser.VerifyPassword(volunteer.Password);
                }

                return null;
            }
        }

        public void GetData(Action<DataItem, Exception> callback)
        {
            throw new NotImplementedException();
        }
    }
}
