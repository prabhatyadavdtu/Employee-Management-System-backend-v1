using Microsoft.AspNetCore.Mvc;
using WebAPIDotNetCore.Entities;
using WebAPIDotNetCore.Repository;

namespace WebAPIDotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employee;
        private readonly IDepartmentRepository _department;
        private readonly IWebHostEnvironment _env;
        public EmployeeController(IEmployeeRepository employee, IDepartmentRepository department, IWebHostEnvironment env)
        {
            _employee = employee ?? throw new ArgumentNullException(nameof(employee));
            _department = department ?? throw new ArgumentNullException(nameof(department));
            _env = env;
        }
        [HttpGet]
        [Route("GetEmployee")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _employee.GetEmployees());
        }
        [HttpGet]
        [Route("GetEmployeeByID/{Id}")]
        public async Task<IActionResult> GetEmpByID(int Id)
        {
            return Ok(await _employee.GetEmployeeByID(Id));
        }
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> Post(EmployeeEntity emp)
        {
            var result = await _employee.InsertEmployee(emp);
            if (result == null || result.EmployeeID == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok("Added Successfully");
        }
        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> Put(EmployeeEntity emp)
        {
            await _employee.UpdateEmployee(emp);
            return Ok("Updated Successfully");
        }
        [HttpDelete]
        [Route("DeleteEmployee")]
        //[HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            var result = _employee.DeleteEmployee(id);
            return new JsonResult("Deleted Successfully");
        }

        [Route("SaveFile")]
        [HttpPost]
        public async Task<JsonResult> SaveFileAsync()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = Path.Combine(_env.ContentRootPath, "Photos", filename);

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    await postedFile.CopyToAsync(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {
                return new JsonResult("anonymous.png");
            }
        }

        [HttpGet]
        [Route("GetDepartment")]
        public async Task<IActionResult> GetAllDepartmentNames()
        {
            return Ok(await _department.GetDepartment());
        }
    }
}
