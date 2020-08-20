using challenge.Models;
using challenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Controllers
{ 
    [Route("api/compensation")]
    public class CompensationController:Controller
    {
        private readonly ICompensationService _compensationService;
        private readonly ILogger<CompensationController> _logger;
        private readonly IEmployeeService _employeeService;
        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService)
        {
            _compensationService = compensationService;
            _logger = logger;
            
        }

        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Received compensation create request for employee {compensation.Employee.FirstName} {compensation.Employee.LastName}");

            _compensationService.Create(compensation);

            return CreatedAtRoute("getCompensationByEmployeeId", new { id = compensation.Employee.EmployeeId }, compensation);

        }

        [HttpGet("{id}", Name ="getCompensationByEmployeeId")]
        public IActionResult GetCompensationByEmployeeId(String id)
        {
            _logger.LogDebug($"Received compensation get request for '{id}'");

            var compensation = _compensationService.GetById(id);

            if(compensation == null)
            {
                return NotFound();
            }

            return Ok(compensation);

        }
    }
}
