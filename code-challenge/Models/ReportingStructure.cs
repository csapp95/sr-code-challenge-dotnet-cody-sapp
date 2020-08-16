using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Models
{
    /// <summary>
    /// ReportingStructure.cs includes ReportStructure
    /// class that contains the Employee of focus
    /// and the number of employees whom report to
    /// the Employee of focus.
    /// </summary>
    public class ReportingStructure
    {
        /// <summary>
        /// This attribute contains the Employee object of focus.
        /// </summary>
        /// <see cref="Models.Employee"/>
        public Employee Employee { get; set; }

        /// <summary>
        /// NumberOfReports attribute contains the number of employees
        /// whom report to the Employee of focus, including those whom
        /// report to the reportees and so forth.
        /// </summary>
        public int NumberOfReports { get; set; }

        public ReportingStructure(Employee employee)
        {
            this.Employee = employee;
            this.NumberOfReports = CalculateNumberOfReports(employee);
        }

        /// <summary>
        /// Traverses recurrsively through DirectReports of the given employee
        /// and returns the count of DirectReports for the given employee which
        /// includes any DirectReports of reporting employees.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        private int CalculateNumberOfReports(Employee employee)
        {   

            int totalReports = 0;

            if(employee.DirectReports.Count > 0)
            {
                totalReports += employee.DirectReports.Count;
                foreach (Employee e in employee.DirectReports)
                {
                    totalReports += CalculateNumberOfReports(e);
                }
            }
            
            return totalReports; 
        }
    }
}
