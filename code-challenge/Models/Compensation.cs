using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Models
{
    /// <summary>
    /// Compensation.cs
    /// Represents the Compensation type.
    /// </summary>
    public class Compensation
    {
        /// <summary>
        /// Primary key for compensation is the EmployeeId.
        /// used DataAnnotation JsonIgnore to suppress the
        /// Id because it would be redundant info since Employee
        /// is displayed on successful response
        /// </summary>
        [JsonIgnore]
        public String Id { get; set; }

        //The Employee this compensation record is connected to
        public Employee Employee { get; set;}

        //The salary assigned to this given employee
        public int Salary { get; set; }

        //The date the Employee should begin receiving the assigned Salary
        public DateTime EffectiveDate { get; set; }
    }
}
