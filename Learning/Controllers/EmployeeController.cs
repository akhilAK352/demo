using Learning.DbContextClass;
using Learning.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Learning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbContext dbContext;
        public EmployeeController(EmployeeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var allEmployee = await dbContext.Employees.ToListAsync();
            return Ok(allEmployee);
        }

        [HttpGet]
        [Route("byID")] // this gives the route name after the table name
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await dbContext.Employees.FindAsync(id);
            if (employee == null) {
                return NotFound("There is no data present in the given id");
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployees(AddEmployee addEmployee)
        {
            if (addEmployee.Scope == ReportScope.Department && !addEmployee.DepartmentId.HasValue) {
                return BadRequest("DepartmentId is required when Scope = Department");
            }
            if (addEmployee.Scope == ReportScope.User && !addEmployee.UserId.HasValue) {
                return BadRequest("UserId is required when Scope = User");
            }

            var employeeList = new Employee() // it is used to assign the value to the table variables
            {
                Name = addEmployee.Name,
                Email = addEmployee.Email,
                Phone = addEmployee.Phone,
                Salary = addEmployee.Salary,
                Scope= addEmployee.Scope,
                DepartmentId =addEmployee.Scope == ReportScope.Department ? addEmployee.DepartmentId : null,
                UserId=addEmployee.Scope == ReportScope.User ? addEmployee.UserId : null,
            };

           await dbContext.Employees.AddAsync(employeeList);
            await dbContext.SaveChangesAsync();
            //return Ok(employeeList);
            return CreatedAtAction(nameof(GetEmployeeById), // tells ASP.NET Core to use the GetEmployeeById action
                        new { id = employeeList.Id }, // passes the new employee's ID into the route
                       employeeList); // sends the created employee object in the response body );
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateEmployee(int id, UpdateEmployee updateEmployee) { 
            var employee= await dbContext.Employees.FindAsync(id);
            if (employee == null) {
                return NotFound();
            }
            if (updateEmployee.Scope == ReportScope.Department && !updateEmployee.DepartmentId.HasValue) { 
                return BadRequest("DepartmentId is required when Scope = Department"); 
            }
            if (updateEmployee.Scope == ReportScope.User && !updateEmployee.UserId.HasValue) { 
                return BadRequest("UserId is required when Scope = User"); 
            }

            employee.Name= updateEmployee.Name;
            employee.Email= updateEmployee.Email;
            employee.Phone= updateEmployee.Phone;
            employee.Salary= updateEmployee.Salary;
            employee.Scope= updateEmployee.Scope;
            employee.DepartmentId = updateEmployee.Scope == ReportScope.Department ? updateEmployee.DepartmentId : null; 
            employee.UserId = updateEmployee.Scope == ReportScope.User ? updateEmployee.UserId : null;

            await dbContext.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteEmployees(int id) { 
            var employee= await dbContext.Employees.FindAsync(id);
            if (employee == null) { 
                return NotFound(); 
            }
            dbContext.Employees.Remove(employee);
            await dbContext.SaveChangesAsync();
            return Ok(employee);
        }
    }

}
