using ExerciseAPI.Models;
using ExerciseAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ExerciseAPI.ViewModels;

namespace ExerciseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly EmployeeRepository employeeRepository;

        public EmployeeController(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }


        // CRUD
        // Create
        [Route("InsertEmployee")]
        [HttpPost]
        public ActionResult InsertEmployee(EmployeeInsertVM employee)
        {
            try
            {
                var insertData = employeeRepository.Insert(employee);
                if (insertData == 1)
                {
                    return CreateResponse(HttpStatusCode.OK, "Sucess Insert Employee", employee);
                }
                return CreateResponse(HttpStatusCode.BadRequest, "Failled Insert Employee");
            }
            catch (Exception ex)
            {
                return CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // Read
        [Route("GetEmployees")]
        [HttpGet]
        public ActionResult GetEmployees()
        {
            try
            {
                var getEmployees = employeeRepository.Get();
                return CreateResponse(HttpStatusCode.OK, "Sucess Retrieved All Data", getEmployees);
            }
            catch (Exception ex)
            {
                return CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("Pagging")]
        [HttpPost]
        public ActionResult Pagging([FromBody] PagingVM parameter)
        {
            try
            {

                var page = parameter;
                Console.WriteLine(page);

                return StatusCode(200, new
                {
                    draw = 1,
                    recordsTotal = 0,
                    recordsFiltered = 10,                    
                });
            }
            catch (Exception ex)
            {
                return CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("GetEmployee/{NIK}")]
        [HttpGet]
        public ActionResult GetEmployee(string NIK)
        {
            try
            {
                var getEmployee = employeeRepository.Get(NIK);
                return CreateResponse(HttpStatusCode.OK, "Sucess Retrieved All Data", getEmployee);
            }
            catch (Exception ex)
            {
                return CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // Update
        [Route("UpdateEmployee/{NIK}")]
        [HttpPut]
        public ActionResult UpdateEmployee(string NIK, EmployeeUpdateVM emp)
        {
            try
            {

                if (employeeRepository.IsPhoneDuplicate(emp.Phone, NIK))
                {
                    return CreateResponse(HttpStatusCode.BadRequest, "Phone Number Cannot Duplicate.");
                }

                var getEmployee = employeeRepository.Update(NIK, emp);
                if (getEmployee == 1)
                {
                    return CreateResponse(HttpStatusCode.OK, "Sucess Update employee.", emp);
                }
                else if (getEmployee == 0)
                {
                    return CreateResponse(HttpStatusCode.OK, "Sucess Update employee. (no changes)", emp);
                }
                else
                {
                    return CreateResponse(HttpStatusCode.InternalServerError, "Failed to update employee.");
                }
            }
            catch (Exception ex)
            {
                return CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // Delete
        [Route("DeleteEmployee/{NIK}")]
        [HttpDelete]
        public ActionResult DeleteEmployee(string NIK)
        {
            try
            {
                var find = employeeRepository.IsEmployeeExsist(NIK);
                if (!find)
                {
                    return CreateResponse(HttpStatusCode.NotFound, "Data not found.");
                }
                var getEmployee = employeeRepository.Delete(NIK);
                if (getEmployee == 1)
                {
                    return CreateResponse(HttpStatusCode.OK, "Sucess Delete Employee Permanently.");
                }
                return CreateResponse(HttpStatusCode.BadRequest, "Failled Delete Employee.");
            }
            catch (Exception ex)
            {
                return CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        // Delete Active
        [Route("DeleteActiveEmployee/{NIK}")]
        [HttpDelete]
        public ActionResult DeleteActiveEmployee(string NIK)
        {
            try
            {
                var find = employeeRepository.IsEmployeeExsist(NIK);
                if (!find)
                {
                    return CreateResponse(HttpStatusCode.NotFound, "Data not found.");
                }
                var getEmployee = employeeRepository.DeleteActive(NIK);
                if (getEmployee == 1)
                {
                    return CreateResponse(HttpStatusCode.OK, "Sucess Delete employee.");
                }
                else if (getEmployee == 0)
                {
                    return CreateResponse(HttpStatusCode.OK, "Employee already delete.");
                }
                return CreateResponse(HttpStatusCode.BadRequest, "Failled Delete Employee.");
            }
            catch (Exception ex)
            {
                return CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        // Response

        private ActionResult CreateResponse(HttpStatusCode statusCode, string message, object data = null)
        {
            if (data == null)
            {
                return StatusCode((int)statusCode, new
                {
                    status_code = (int)statusCode,
                    message,
                });

            }

            return StatusCode((int)statusCode, new
            {
                status_code = (int)statusCode,
                message,
                data
            });
        }
    }
}
