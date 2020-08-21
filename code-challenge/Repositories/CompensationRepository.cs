using challenge.Data;
using challenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Repositories
{
    /// <summary>
    /// CompensationRepository implements ICompensationRepository
    /// </summary>
    public class CompensationRepository :ICompensationRepository
    {

        private readonly CompensationContext _compensationContext;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRepository(ILogger<ICompensationRepository> logger, CompensationContext compensationContext)
        {
            this._compensationContext = compensationContext;
            this._logger = logger;
        }

        /// <summary>
        /// Gets compensation object associated with the given id if one exists.
        /// </summary>
        /// <param name="id">EmployeeId string</param>
        /// <returns></returns>
        public Compensation GetById(String id)
        {
            return _compensationContext.CompensationRecords.SingleOrDefault(c => c.Employee.EmployeeId == id);
        }

        /// <summary>
        /// Adds compensation object to the context.
        /// </summary>
        /// <param name="compensation"></param>
        /// <returns></returns>
        public Compensation Add(Compensation compensation)
        {
            _compensationContext.CompensationRecords.Add(compensation);
            return compensation;

        }

        public Task SaveAsync()
        {
            return _compensationContext.SaveChangesAsync();
        }

        /// <summary>
        /// Removes the given compensation object.
        /// </summary>
        /// <param name="compensation"></param>
        /// <returns></returns>
        public Compensation Remove(Compensation compensation)
        {
            return _compensationContext.CompensationRecords.Remove(compensation).Entity;
        }
    }
}
