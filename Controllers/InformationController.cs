using ExerciseAPI.Repository;
using ExerciseAPI.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ExerciseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformationController : ControllerBase
    {

        private readonly InformationRepository repository;

        public InformationController(InformationRepository repository)
        {
            this.repository = repository;
        }


        // Active + Dept Name (get all active employee + dept name)
        [Route("GetALlActiveEmployee")]
        [HttpGet]
        public ActionResult GetALlActiveEmployee()
        {
            try
            {
                var data = repository.GetAllActiveEmployee();

                if (data == null || !data.Any())
                {
                    return CreateResponse(HttpStatusCode.OK, "No Data Found.", new object[]{});
                }

                return CreateResponse(HttpStatusCode.OK, "Sucess Retrieved All Data", data);
            }
            catch (Exception ex)
            {
                return CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        // Resign + Dept Name (get all active employee + dept name)
        [Route("GetALlResignEmployee")]
        [HttpGet]
        public ActionResult GetALlResignEmployee()
        {
            try
            {
                var data = repository.GetAllResignEmployee();
                if (data == null || !data.Any())
                {
                    return CreateResponse(HttpStatusCode.OK, "No Data Found.", new object[] { });
                }

                return CreateResponse(HttpStatusCode.OK, "Sucess Retrieved All Data", data);
            }
            catch (Exception ex)
            {
                return CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }



        [Route("GetActiveEmployeeInDept/{deptID}")]
        [HttpGet]
        public ActionResult GetActiveEmployeeInDept(string deptID)
        {
            try
            {
                var data = repository.GetAllActiveEmployeeInDept(deptID);

                if (data == null || !data.Any())
                {
                    return CreateResponse(HttpStatusCode.NotFound, "No Data Found.");
                }
                return CreateResponse(HttpStatusCode.OK, "Sucess Retrieved All Data", data);
            }
            catch (Exception ex)
            {
                return CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }



        [Route("GetResignEmployeeInDept/{deptID}")]
        [HttpGet]
        public ActionResult GetResignEmployeeInDept(string deptID)
        {
            try
            {
                var data = repository.GetAllResignEmployeeInDept(deptID);
                if (data == null || !data.Any())
                {
                    return CreateResponse(HttpStatusCode.NotFound, "No Data Found.");
                }
                return CreateResponse(HttpStatusCode.OK, "Sucess Retrieved All Data", data);
            }
            catch (Exception ex)
            {
                return CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }



        [Route("CountActiveEmployeesByDepartment")]
        [HttpGet]
        public ActionResult CountActiveEmployeesByDepartment()
        {
            try
            {
                var data = repository.GetCountOfActiveEmployeesByDepartment();
                if (data == null || !data.Any())
                {
                    return CreateResponse(HttpStatusCode.NotFound, "No Data Found.");
                }
                return CreateResponse(HttpStatusCode.OK, "Sucess Retrieved All Data", data);
            }
            catch (Exception ex)
            {
                return CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }



        [Route("CountResignEmployeesByDepartment")]
        [HttpGet]
        public ActionResult CountResignEmployeesByDepartment()
        {
            try
            {
                var data = repository.GetCountOfResignedEmployeesByDepartment();
                if (data == null || !data.Any())
                {
                    return CreateResponse(HttpStatusCode.NotFound, "No Data Found.");
                }
                return CreateResponse(HttpStatusCode.OK, "Sucess Retrieved All Data", data);
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
