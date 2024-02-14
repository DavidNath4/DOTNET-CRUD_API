using ExerciseAPI.Models;
using ExerciseAPI.Repository;
using ExerciseAPI.Repository.Interface;
using ExerciseAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Linq.Dynamic.Core;


namespace ExerciseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentRepository repository;

        public DepartmentController(DepartmentRepository repository)
        {
            this.repository = repository;
        }



        // CRUD
        // Create
        [Route("InsertDepartment")]
        [HttpPost]
        public ActionResult InsertDepartment(DepartmentInsertVM dept)
        {
            try
            {

                if (string.IsNullOrEmpty(dept.Name))
                {
                    return CreateResponse(HttpStatusCode.BadRequest, "Department Name is required.");
                }

                if (repository.IsDepartmentNameDuplicate(dept.Name))
                {
                    return CreateResponse(HttpStatusCode.BadRequest, "Department Name is duplicate.");
                }

                var insertData = repository.Insert(dept);
                if (insertData == 1)
                {
                    return CreateResponse(HttpStatusCode.OK, "Sucess Insert Employee", dept);
                }
                return CreateResponse(HttpStatusCode.BadRequest, "Failled Insert Employee");
            }
            catch (Exception ex)
            {
                return CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // Read
        [Route("GetDepartments")]
        [HttpGet]
        public ActionResult GetDepartments()
        {
            try
            {
                var data = repository.Get();
                return CreateResponse(HttpStatusCode.OK, "Success Retrieved Data.", data);
            }
            catch (Exception ex)
            {
                return CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [Route("GetPaging")]
        [HttpPost]
        public ActionResult GetPaging()
        {
            try
            {
                int totalRecord = 0;
                int filterRecord = 0;
                var draw = Request.Form["draw"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0");
                int skip = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");
                var data = repository.Get().AsQueryable();
                //get total count of data in table
                totalRecord = data.Count();
                // search data when search value found
                if (!string.IsNullOrEmpty(searchValue))
                {
                    data = data.Where(x => 
                    x.Name.ToLower().Contains(searchValue.ToLower())
                    || x.Id.ToLower().Contains(searchValue.ToLower()                    
                    ));
                }
                // get total count of records after search
                filterRecord = data.Count();
                //sort data                 
                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection)) data = data.OrderBy(sortColumn + " " + sortColumnDirection);

                //pagination
                var deptList = data.Skip(skip).Take(pageSize).ToList();
                var returnObj = new
                {
                    draw = draw,
                    recordsTotal = totalRecord,
                    recordsFiltered = filterRecord,
                    data = deptList
                };
                return Ok(returnObj);
            }
            catch (Exception ex)
            {
                return CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }




        [Route("GetDepartment/{ID}")]
        [HttpGet]
        public ActionResult GetDepartment(string ID)
        {
            try
            {
                var data = repository.Get(ID);
                return CreateResponse(HttpStatusCode.OK, "Success Retrieved Data.", data);
            }
            catch (Exception ex)
            {
                return CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // Update
        [Route("UpdateDepartment/{ID}")]
        [HttpPut]
        public ActionResult UpdateDepartment(string ID, DepartmentUpdateVM updateDepartment)
        {
            try
            {
                if (string.IsNullOrEmpty(updateDepartment.NewName))
                {
                    return CreateResponse(HttpStatusCode.BadRequest, "Department Name is required.");
                }

                // check if duplicate
                if (repository.IsDepartmentNameDuplicate(updateDepartment.NewName, ID))
                {
                    return CreateResponse(HttpStatusCode.BadRequest, "Department Name is duplicate.");
                }

                var data = repository.Update(ID, updateDepartment);

                if (data == 1)
                {
                    return CreateResponse(HttpStatusCode.OK, "Sucess Update Department", updateDepartment);
                }
                else if (data == 0)
                {
                    return CreateResponse(HttpStatusCode.OK, "Sucess Update Department (there is no change)", updateDepartment);
                }
                return CreateResponse(HttpStatusCode.BadRequest, "Failled Update Department");
            }
            catch (Exception ex)
            {
                return CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // Delete
        [Route("DeleteDepartment/{ID}")]
        [HttpDelete]
        public ActionResult DeleteDepartment(string ID)
        {
            try
            {
                var find = repository.IsDepartmentExsist(ID);
                if (!find)
                {
                    return CreateResponse(HttpStatusCode.NotFound, "Data not found.");
                }

                if (!repository.IsEmployeesDeptNull(ID))
                {
                    return CreateResponse(HttpStatusCode.BadRequest, "Can't Remove Department Because There are Employees.");
                }

                var result = repository.Delete(ID);
                if (result == 1)
                {
                    return CreateResponse(HttpStatusCode.OK, "Sucess Remove Department");
                }
                return CreateResponse(HttpStatusCode.BadRequest, "Failled Remove Department");
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
