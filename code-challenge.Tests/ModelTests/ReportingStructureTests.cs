using challenge.Data;
using challenge.Models;
using challenge.Repositories;
using challenge.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace code_challenge.Tests.Integration
{
    [TestClass]
    public class ReportingStructureTests
    {
        
        private List<Employee> _employees;
        

        private const string JOHN_LENNON_ID = "16a596ae-edd3-4847-99fe-c4518e82c86f";
        private const string PAUL_MCCARTNEY_ID = "b7839309-3348-463b-a7e3-5de1c168beb3";
        private const string RINGO_STARR_ID = "03aa1462-ffa9-4978-901b-7c001562cf6f";
        private const string PETE_BEST_ID = "62c1084e-6e34-4630-93fd-9153afb65309";
        private const string GEORGE_HARRISON_ID = "c0c2293d-16bd-4603-8e08-638a9d18b22c";

        [TestInitialize]
        public void Initialize()
        {
            LoadEmployees();
            _employees.ForEach(employee => employee.DirectReports = new List<Employee>());
        }

        [TestMethod]
        public void ReportingStructure_Constructor_And_CalculateNumberOfReports()
        {
            //Arrange
            int johnNumberOfReports = 4;
            int ringoNumberOfReports = 2;
            int paulNumberOfReports = 0;
            int peteNumberOfReports = 0;
            int georgeNumberofReports = 0;
            ArrangeOriginalChain();

            //Act
            var john = new ReportingStructure(_employees.First(e => e.EmployeeId.Equals(JOHN_LENNON_ID)));
            var ringo = new ReportingStructure(_employees.First(e => e.EmployeeId.Equals(RINGO_STARR_ID)));
            var paul = new ReportingStructure(_employees.First(e => e.EmployeeId.Equals(PAUL_MCCARTNEY_ID)));
            var pete = new ReportingStructure(_employees.First(e => e.EmployeeId.Equals(PETE_BEST_ID)));
            var george = new ReportingStructure(_employees.First(e => e.EmployeeId.Equals(GEORGE_HARRISON_ID)));

            //Assert
            Assert.AreEqual(john.NumberOfReports, johnNumberOfReports);
            Assert.AreEqual(john.GetType(), typeof(ReportingStructure));
            Assert.AreEqual(ringo.NumberOfReports, ringoNumberOfReports);
            Assert.AreEqual(paul.NumberOfReports, paulNumberOfReports);
            Assert.AreEqual(pete.NumberOfReports, peteNumberOfReports);
            Assert.AreEqual(george.NumberOfReports, georgeNumberofReports);
        }


        private void ArrangeOriginalChain()
        {
            Employee johnLennon = _employees.First(e => e.EmployeeId.Equals(JOHN_LENNON_ID));
            Employee paulMcCartney = _employees.First(e => e.EmployeeId.Equals(PAUL_MCCARTNEY_ID));
            Employee ringoStarr = _employees.First(e => e.EmployeeId.Equals(RINGO_STARR_ID));
            Employee peteBest = _employees.First(e => e.EmployeeId.Equals(PETE_BEST_ID));
            Employee georgeHarrison = _employees.First(e => e.EmployeeId.Equals(GEORGE_HARRISON_ID));

            //Add Paul McCartney to John Lennon
            johnLennon.DirectReports.Add(paulMcCartney);
            //Add Ringo Starr to John Lennon
            johnLennon.DirectReports.Add(ringoStarr);
            //Add Pete Best to Ringo Starr
            ringoStarr.DirectReports.Add(peteBest);
            //Add George Harrison to Ringo Starr
            ringoStarr.DirectReports.Add(georgeHarrison);

        }
        private void LoadEmployees()
        {
            using(FileStream fs = new FileStream("Resources/EmployeeSeedData.json", FileMode.Open))
            using (StreamReader sr = new StreamReader(fs))
            using (JsonReader js = new JsonTextReader(sr))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();

                _employees = jsonSerializer.Deserialize<List<Employee>>(js);
            }
        }
      
    }
}
