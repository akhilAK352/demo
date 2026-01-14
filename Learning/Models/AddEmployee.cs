namespace Learning.Models
{
    public class AddEmployee
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public decimal Salary { get; set; }
        public int? DepartmentId { get; set; }      // when Scope == Department
        public int? UserId { get; set; }
        public ReportScope Scope { get; set; }
    }
}
