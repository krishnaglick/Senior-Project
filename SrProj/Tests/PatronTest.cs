using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SrProj.API;
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

            var patronController = new PatronController {Request = new HttpRequestMessage()};
            patronController.Request.SetConfiguration(new HttpConfiguration());

            var response = patronController.Create(newPatron);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created, "Patron not created.");
        }
    }
}