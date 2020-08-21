using challenge.Models;
using challenge.Services;
using code_challenge.Tests.Integration.Extensions;
using code_challenge.Tests.Integration.Helpers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace code_challenge.Tests.Integration
{
    [TestClass]
    public class ReportingStructureControllerTests
    {

        private static HttpClient _httpClient;
        private static TestServer _testServer;

        private static IEmployeeService _employeeService;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<TestServerStartup>()
                .UseEnvironment("Development"));

            _httpClient = _testServer.CreateClient();

            _employeeService = Substitute.For<IEmployeeService>();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }


        [TestMethod]
        public void ReportingStructure_Get_Success()
        {
            //Arrange
            var employee = new Employee()
            {
                FirstName = "Eddie",
                LastName = "Van Halen",
                Department = "Rock Music",
                Position = "Lead Guitar",
                DirectReports = new List<Employee>()
                {
                    new Employee()
                    {
                        EmployeeId = "Reportee-Eddie-VanHalen",
                        FirstName = "Sammy",
                        LastName = "Hagar",
                        Department = "Rock Music",
                        Position = "Replacement Lead Singer"
                    }                
                }
            };

            var numberOfReports = 1;

            var requestContent = new JsonSerialization().ToJson(employee);

            //add employee
            var postRequestTask = _httpClient.PostAsync("api/employee",
                new StringContent(requestContent, Encoding.UTF8, "application/json"));

            var lookUpId = postRequestTask.Result.DeserializeContent<Employee>().EmployeeId;
            employee.EmployeeId = lookUpId;

            //Act
            var getRequest = _httpClient.GetAsync($"api/reportingStructure/{lookUpId}");

            var response = getRequest.Result;
            //Assert

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var report = response.DeserializeContent<ReportingStructure>();

            Assert.AreEqual(report.Employee.FirstName, employee.FirstName);
            Assert.AreEqual(report.Employee.LastName, employee.LastName);
            Assert.AreEqual(report.Employee.Department, employee.Department);
            Assert.AreEqual(report.Employee.Position, employee.Position);

            Assert.AreEqual(report.NumberOfReports, numberOfReports);
        }
    }
}
