using challenge.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Data
{
    /// <summary>
    /// CompensationContext keeps record of Employee compensation
    /// </summary>
    public class CompensationContext:DbContext
    {
        public CompensationContext(DbContextOptions<CompensationContext> options) : base(options)
        {

        }

        /// <summary>
        /// CompensationRecords is the DbSet that contains the Compensation's
        /// </summary>
        public DbSet<Compensation> CompensationRecords { get; set; }
    }
}
