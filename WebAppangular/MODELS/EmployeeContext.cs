using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace WebAppangular.MODELS
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {

        }
        public DbSet<TblEmployees> TblEmployees { get; set; }
        public DbSet<TblDesignation> TblDesignation { get; set; }
    }
}
