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
    [Route("api/reportStructure")]
    public class ReportStructureController : Controller
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;

        public ReportStructureController(ILogger<ReportStructureController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

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
