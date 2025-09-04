using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;
using WebAPIDotNetCore.Entities;
using WebAPIDotNetCore.Repository;

namespace WebAPIDotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _department;
        public DepartmentController(IDepartmentRepository department)
        {
            _department = department ??
                throw new ArgumentNullException(nameof(department));
        }
        [HttpGet]
        [Route("GetDepartment")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _department.GetDepartment());
        }
        [HttpGet]
        [Route("GetDepartmentByID/{Id}")]
        public async Task<IActionResult> GetDeptById(int Id)
        {
            return Ok(await _department.GetDepartmentByID(Id));
        }
        [HttpPost]
        [Route("AddDepartment")]
        public async Task<IActionResult> Post(DepartmentEntity dep)
        {
            if (ModelState.IsValid)
            {
                bool result = await _department.InsertDepartment(dep);
                if (!result)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
                }
                return Ok(dep.DepartmentName + " " + "Added Successfully");
            }
            return BadRequest();
        }
        [HttpPut]
        [Route("UpdateDepartment")]
        public async Task<IActionResult> Put(DepartmentEntity dep)
        {
            await _department.UpdateDepartment(dep);
            return Ok("Updated Successfully");
        }
        [HttpDelete]
        //[HttpDelete("{id}")]
        [Route("DeleteDepartment")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _department.DeleteDepartment(id);
            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok("Deleted Successfully");
        }
    }
}
