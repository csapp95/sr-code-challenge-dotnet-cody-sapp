﻿using challenge.Data;
using challenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Repositories
{
    public class CompensationRepository :ICompensationRepository
    {

        private readonly CompensationContext _compensationContext;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRepository(ILogger<ICompensationRepository> logger, CompensationContext compensationContext)
        {
            this._compensationContext = compensationContext;
            this._logger = logger;
        }

        public Compensation GetById(String id)
        {
            return _compensationContext.CompensationRecords.SingleOrDefault(c => c.Employee.EmployeeId == id);
        }

        public Compensation Add(Compensation compensation)
        {
            return null;
        }

        public Task SaveAsync()
        {
            return _compensationContext.SaveChangesAsync();
        }

        public Compensation Remove(Compensation compensation)
        {
            return _compensationContext.CompensationRecords.Remove(compensation).Entity;
        }
    }
}
