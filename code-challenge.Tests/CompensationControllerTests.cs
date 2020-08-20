using challenge.Models;
using code_challenge.Tests.Integration.Extensions;
using code_challenge.Tests.Integration.Helpers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace code_challenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
    {

        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<TestServerStartup>()
                .UseEnvironment("Development"));

            _httpClient = _testServer.CreateClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void CreateCompensation_Returns_Created()
        {
            //Arrange
            var employee = new Employee()
            {
                EmployeeId = "Super-Unique-Id",
                FirstName = "Eddie",
                LastName = "Van Halen",
                Department = "Rock Music",
                Position = "Lead Guitar"
            };

            var compensation = new Compensation()
            {
                Id = employee.EmployeeId,
                Employee = employee,
                Salary = 100000,
                EffectiveDate = DateTime.Today
            };

            var requestBody = new JsonSerialization().ToJson(compensation);


            //Act

            var postRequest = _httpClient.PostAsync("api/compensation",
                new StringContent(requestBody, Encoding.UTF8, "application/json"));

            var response = postRequest.Result;

            //Assert

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newCompensation = response.DeserializeContent<Compensation>();

            Assert.AreEqual(compensation.Salary, newCompensation.Salary);
            Assert.AreEqual(compensation.EffectiveDate, newCompensation.EffectiveDate);

            Assert.AreEqual(compensation.Employee.EmployeeId, newCompensation.Employee.EmployeeId);
            Assert.AreEqual(compensation.Employee.FirstName, newCompensation.Employee.FirstName);
            Assert.AreEqual(compensation.Employee.LastName, newCompensation.Employee.LastName);
            Assert.AreEqual(compensation.Employee.Department, newCompensation.Employee.Department);
            Assert.AreEqual(compensation.Employee.Position, newCompensation.Employee.Position);

        }

        [TestMethod]
        public void GetCompensationByEmplyeeId_Returns_Ok()
        {
            var employee = new Employee()
            {
                EmployeeId = "AnotherSuperUniqueId",
                FirstName = "David",
                LastName = "Bowie",
                Department = "Pop Music",
                Position = "Singer"
            };

            var compensation = new Compensation()
            {
                Id = employee.EmployeeId,
                Employee = employee,
                Salary = 123456,
                EffectiveDate = DateTime.Today
            };

            var content = new JsonSerialization().ToJson(compensation);
            var postRequest = _httpClient.PostAsync("api/compensation", new StringContent(content, Encoding.UTF8, "application/json"));


        }

        [TestMethod]
        public void GetCompensationByEmplyeeId_Returns_NotFound()
        {

        }

    }
}
