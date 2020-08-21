using Castle.Core.Logging;
using challenge.Models;
using challenge.Repositories;
using challenge.Services;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;

namespace code_challenge.Tests.Integration.ServiceTests
{
    [TestClass]
    public class CompensationServiceTests
    {
        private ICompensationService _compensationService;
        private ICompensationRepository _compensationRepo;
        

        [TestInitialize]
        public void Initialize()
        {
            //Using NSubstitute for dependency injection
            _compensationRepo = Substitute.For<ICompensationRepository>();
            var logger = Substitute.For<ILogger<ICompensationService>>();
            _compensationService = new CompensationService(logger, _compensationRepo);
        }

        [TestMethod]
        public void GetById_Return_Success()
        {
            //Assert
            var employee = new Employee()
            {
                EmployeeId = "EmployeeTestId",
                FirstName = "John",
                LastName = "Doe",
                Position = "Uknown",
                Department = "Also Unknown"
            };

            var compensation = new Compensation()
            {
                Id = employee.EmployeeId,
                Employee = employee,
                Salary = 70000,
                EffectiveDate = DateTime.Today
            };

            var lookUpId = employee.EmployeeId;
            _compensationRepo.GetById(lookUpId).Returns<Compensation>(compensation);

            //Act

            var newCompensation = _compensationService.GetById(lookUpId);

            //Assert

            Assert.AreEqual(compensation.Salary, newCompensation.Salary);
            Assert.AreEqual(compensation.EffectiveDate, newCompensation.EffectiveDate);

            Assert.AreEqual(compensation.Employee.EmployeeId, newCompensation.Employee.EmployeeId);
            Assert.AreEqual(compensation.Employee.FirstName, newCompensation.Employee.FirstName);
            Assert.AreEqual(compensation.Employee.LastName, newCompensation.Employee.LastName);
            Assert.AreEqual(compensation.Employee.Department, newCompensation.Employee.Department);
            Assert.AreEqual(compensation.Employee.Position, newCompensation.Employee.Position);
            
        }

        [TestMethod]
        public void GetById_Return_Null()
        {
            //Arrange
            var emptyId = String.Empty;

            //Act
            var returnCompensation = _compensationService.GetById(emptyId);

            //Assert

            Assert.IsNull(returnCompensation);
        }

        [TestMethod]
        public void Replace_Success()
        {
            //Arrange
            var employee = new Employee()
            {
                EmployeeId = "EmployeeTestId",
                FirstName = "John",
                LastName = "Doe",
                Position = "Uknown",
                Department = "Also Unknown"
            };

            var compensation = new Compensation()
            {
                Id = employee.EmployeeId,
                Employee = employee,
                Salary = 70000,
                EffectiveDate = DateTime.Today
            };

            var newCompensation = new Compensation()
            {
                Id = compensation.Id,
                Employee = employee,
                Salary = 100000,
                EffectiveDate = DateTime.Today.AddDays(5)
            };

            //Act

            var replacementComp = _compensationService.Replace(compensation, newCompensation);

            //Assert

            Assert.AreEqual(newCompensation.Id, replacementComp.Id);
            Assert.AreEqual(newCompensation.Salary, replacementComp.Salary);
            Assert.AreEqual(newCompensation.EffectiveDate, replacementComp.EffectiveDate);
            
        }

        [TestMethod]
        public void Create_Success()
        {
            //Arrange
            var employee = new Employee()
            {
                EmployeeId = "EmployeeTestId",
                FirstName = "John",
                LastName = "Doe",
                Position = "Uknown",
                Department = "Also Unknown"
            };

            var compensation = new Compensation()
            {
                Id = employee.EmployeeId,
                Employee = employee,
                Salary = 70000,
                EffectiveDate = DateTime.Today
            };

            //Act

            var createdCompensation = _compensationService.Create(compensation);

            //Assert

            Assert.AreEqual(compensation.Salary, createdCompensation.Salary);
            Assert.AreEqual(compensation.EffectiveDate, createdCompensation.EffectiveDate);

            Assert.AreEqual(compensation.Employee.EmployeeId, createdCompensation.Employee.EmployeeId);
            Assert.AreEqual(compensation.Employee.FirstName, createdCompensation.Employee.FirstName);
            Assert.AreEqual(compensation.Employee.LastName, createdCompensation.Employee.LastName);
            Assert.AreEqual(compensation.Employee.Department, createdCompensation.Employee.Department);
            Assert.AreEqual(compensation.Employee.Position, createdCompensation.Employee.Position);

        }

        [TestMethod]
        public void Create_Null_Param()
        {
            //Arrange
            Compensation newCompensation = null;

            //Act

            var createdCompensation = _compensationService.Create(newCompensation);

            //Assert

            Assert.AreEqual(null, createdCompensation);
            

        }
    }
}
