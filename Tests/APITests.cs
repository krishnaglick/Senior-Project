
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using SrProj.API;

namespace Tests
{
    [TestClass]
    public class APITests
    {
        [TestInitialize]
        public void BeforeEach()
        {
            DataAccess.Contexts.ConnectionString.ChosenConnection = "homeDesktop";
        }

        [TestMethod]
        public void LoginTest()
        {
            //return;
            //For some reason the DB context gets all wacky, so this isn't doing anything!
            var testVolunteer = new Login
            {
                Username = "user",
                Password = "swordfish"
            };

            var loginController = new LoginController();

            var testLogin = loginController.Login(testVolunteer);
        }
    }
}
