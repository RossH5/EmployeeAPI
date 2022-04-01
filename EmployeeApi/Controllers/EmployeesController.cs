using System;
using System.Threading.Tasks;
using EmployeeApi.Daos;
using EmployeeApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers
{
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeDao _employeeDao;
        public EmployeesController(EmployeeDao employeeDao)
        {
            _employeeDao = employeeDao;
        }

        [HttpGet]
        [Route("employees")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var employees = await _employeeDao.GetEmployees();
                return Ok(employees);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("employees/{id}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] int id)
        {
            try
            {
                var employee = await _employeeDao.GetEmployeeById(id);
                if (employee == null)
                {
                    return StatusCode(404);
                }

                return Ok(employee);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("employees/{id}")]
        public async Task<IActionResult> DeleteEmployeeById([FromRoute] int id)
        {
            try
            {
                var employee = await _employeeDao.GetEmployeeById(id);
                if (employee == null)
                {
                    return StatusCode(404);
                }
                
                await _employeeDao.DeleteEmployeeById(id);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        [Route("employees")]
        public async Task<IActionResult> UpdateEmployeeById([FromBody]Employee updateRequest)
        {
            try
            {
                var employee = await _employeeDao.GetEmployeeById(updateRequest.Id);
                if (employee == null)
                {
                    return StatusCode(404);
                }

                await _employeeDao.UpdateEmployeeById(updateRequest);
                return StatusCode(204);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Route("employees")]
        public async Task<IActionResult> CreateEmployee([FromBody]Employee updateRequest)
        {
            try
            {
                await _employeeDao.CreateEmployee(updateRequest);
                return StatusCode(204);

            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }
    }
}
