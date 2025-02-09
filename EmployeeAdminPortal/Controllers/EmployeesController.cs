using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models.Dtos;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    // route attribute is used to define the route for the controller
    [Route("api/[controller]")]
    // ApiController attribute is used to define the controller as an API controller
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        // connect database using db context (constructor injection)
        public EmployeesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var allEmployees = dbContext.Employees.ToList();
            // return ok status code with all employees
            return Ok(allEmployees);
        }

        [HttpGet]
        // Route parameter to accept identifier need match the parameter for the method
        [Route("{id:guid}")]
        public IActionResult GetEmployeeById(
            Guid id
            )
        {
            var employee = dbContext.Employees.Find(id);
            if (employee is null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult AddEmployee(
            AddEmployeeDto addEmployeeDto
            )
        {
            var employeeEntity = new Employee() {
                Email = addEmployeeDto.Email,
                Name = addEmployeeDto.Name,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary
            };
            dbContext.Employees.Add(employeeEntity);
            // save the changes to the database
            dbContext.SaveChanges();
            return Created();
        }

        [HttpPut]
        [Route("{id: guid}")]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = dbContext.Employees.Find(id);

            if (employee is null)
            {
                return NotFound();
            }

            employee.Name = updateEmployeeDto.Name;
            employee.Email = updateEmployeeDto.Email;
            employee.Phone = updateEmployeeDto.Phone;
            employee.Salary = updateEmployeeDto.Salary;

            dbContext.SaveChanges();
            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id: guid}")]
        public IActionResult DeleteEmployeeById(
            Guid id)
        {
            var employee = dbContext.Employees.Find(id);
            if (employee is null)
            {
                return NotFound();
            }

            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}
