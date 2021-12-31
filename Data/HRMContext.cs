#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HRM.Models;

namespace HRM.Data
{
    public class HRMContext : DbContext
    {
        public HRMContext (DbContextOptions<HRMContext> options)
            : base(options)
        {
        }

        public DbSet<HRM.Models.Employee> Employee { get; set; }
    }
}
