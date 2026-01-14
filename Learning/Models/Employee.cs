using System.Reflection.Metadata;

namespace Learning.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public decimal Salary { get; set; }
        public int? DepartmentId { get; set; }      // when Scope == Department
        public int? UserId { get; set; }

        public ReportScope Scope { get; set; }
    }
    public enum ReportScope
    {
        Department = 1,
        User = 2,
        Period = 3
    }
}
