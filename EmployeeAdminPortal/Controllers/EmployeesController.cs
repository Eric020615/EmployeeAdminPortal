using EmployeeAdminPortal.Data;
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
    }
}
