using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Services;
using challenge.Models;
using challenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace challenge.Controllers
{
    /// <summary>
    /// ReportingStructureController
    /// Contains endpoints related to ReportStructure
    /// </summary>
    [Route("api/reportingStructure")]
    public class ReportingStructureController : Controller
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;

        /// <summary>
        /// ReportingStructureController constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="employeeService"></param>
        public ReportingStructureController(ILogger<ReportingStructureController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        /// <summary>
        /// ReportingStructure Get endpoint:
        /// Creates a new ReportingStructure object on demand
        /// which contains the employee of focus and calculates the number of DirectReport
        /// </summary>
        /// <param name="id">EmployeeId of focus</param>
        /// <returns>
        /// HttpStatusCode.Ok and reportingstructure on success
        /// HttpStatusCode.NotFound if there is not employee with the given id
        /// </returns>
        [HttpGet("{id}")]
        public IActionResult ReportingStructure(String id)
        {
            _logger.LogDebug($"Received employee report structure request for '{id}'.");

            var employee = _employeeService.GetById(id);

            if (employee == null)
                return NotFound();

            ReportingStructure reportingStructure = new ReportingStructure(employee);

            return Ok(reportingStructure);
        }
    }
}
