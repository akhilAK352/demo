using Learning.Models;
using Microsoft.EntityFrameworkCore;

namespace Learning.DbContextClass
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
