using challenge.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace code_challenge.Tests.Integration.ModelTests
{
    [TestClass]
    public class CompensationTests
    {

        [TestMethod]
        public void Compensation_Id_Get_Set()
        {
            //Arrange
            var compIdValue = "ThisIsAStringId";
            var compensation = new Compensation()
            {
                Id = compIdValue
            };


            //Act

            var getCompId = compensation.Id;

            //Assert

            Assert.AreEqual(compIdValue, getCompId);
        }

        [TestMethod]
        public void Compensation_Salary_Get_Set()
        {
            //Arrange
            var targetValue = 500000;
            var compensation = new Compensation()
            {
                Salary = targetValue
            };


            //Act

            var returnValue = compensation.Salary;

            //Assert

            Assert.AreEqual(targetValue, returnValue);
        }

        [TestMethod]
        public void Compensation_EffectiveDate_Get_Set()
        {
            //Arrange
            var targetValue = DateTime.Today;
            var compensation = new Compensation()
            {
                EffectiveDate = targetValue
            };


            //Act

            var returnValue = compensation.EffectiveDate;

            //Assert

            Assert.AreEqual(targetValue, returnValue);
        }

        [TestMethod]
        public void Compensation_Employee_Get_Set()
        {
            //Arrange
            var targetValue = new Employee()
            {
                EmployeeId = "EmployeeTestId",
                FirstName = "John",
                LastName = "Doe",
                Position = "Uknown",
                Department = "Also Unknown"
            }; ;
            var compensation = new Compensation()
            {
                Employee = targetValue
            };


            //Act

            var returnValue = compensation.Employee;

            //Assert

            Assert.AreEqual(targetValue, returnValue);
        }
    }
}
