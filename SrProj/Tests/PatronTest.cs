using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SrProj.API;
using SrProj.Controllers;
using SrProj.Models;

namespace SrProj.Tests
{
    [TestClass]
    public class PatronTest
    {
        [TestMethod]
        public void CreatePatron()
        {

            var newPatron = new Patron
            {
                FirstName = "TestUser",
                LastName = "TestUser",
                DateOfBirth = DateTime.Now
            };

            var req = new HttpRequestMessage();
            //req.SetOwinContext(new OwinContext());
            var patronController = new PatronController {Request = req};
            //var accountController = new AccountController() { Request = new HttpRequestMessage(), UserManager = req.GetOwinContext().GetUserManager<ApplicationUserManager>() };
            patronController.Request.SetConfiguration(new HttpConfiguration());
            /*accountController.Request.SetConfiguration(new HttpConfiguration());
            accountController.Create(new RegisterBindingModel
            {
                Email = "potato",
                Password = "swordfish",
                ConfirmPassword = "swordfish"
            });*/

            var response = patronController.Create(newPatron);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created, "Patron not created.");
        }
    }
}