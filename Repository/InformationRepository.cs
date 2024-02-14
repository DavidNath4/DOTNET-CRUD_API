using ExerciseAPI.Context;
using ExerciseAPI.Repository.Interface;
using ExerciseAPI.ViewModels;

namespace ExerciseAPI.Repository
{
    public class InformationRepository : IIformationRepository
    {
        private readonly MyContext context;

        public InformationRepository(MyContext context)
        {
            this.context = context;
        }

        public IEnumerable<AllEmployeeVM> GetAllActiveEmployee()
        {
            var query = from employee in context.Employees
                        join department in context.Departments on employee.DepartmentId equals department.Id
                        where employee.IsActive == true
                        select new AllEmployeeVM
                        {
                            NIK = employee.NIK,
                            FirstName = employee.FirstName,
                            LastName = employee.LastName,
                            Email = employee.Email,
                            Phone = employee.Phone,
                            Address = employee.Address,
                            DepartmentName = department.Name,
                            IsActive = employee.IsActive

                        };

            return query.ToList();

        }

        public IEnumerable<AllEmployeeVM> GetAllActiveEmployeeInDept(string deptID)
        {
            var query = from employee in context.Employees
                        join department in context.Departments on employee.DepartmentId equals department.Id
                        where employee.IsActive == true && department.Id == deptID
                        select new AllEmployeeVM
                        {
                            NIK = employee.NIK,
                            FirstName = employee.FirstName,
                            LastName = employee.LastName,
                            Email = employee.Email,
                            Phone = employee.Phone,
                            Address = employee.Address,
                            DepartmentName = department.Name,
                            IsActive = employee.IsActive
                        };

            return query.ToList();
        }

        public IEnumerable<AllEmployeeVM> GetAllResignEmployee()
        {
            var query = from employee in context.Employees
                        join department in context.Departments on employee.DepartmentId equals department.Id
                        where employee.IsActive == false
                        select new AllEmployeeVM
                        {
                            NIK = employee.NIK,
                            FirstName = employee.FirstName,
                            LastName = employee.LastName,
                            Email = employee.Email,
                            Phone = employee.Phone,
                            Address = employee.Address,
                            DepartmentName = department.Name,
                            IsActive = employee.IsActive

                        };

            return query.ToList();
        }

        public IEnumerable<AllEmployeeVM> GetAllResignEmployeeInDept(string deptID)
        {
            var query = from employee in context.Employees
                        join department in context.Departments on employee.DepartmentId equals department.Id
                        where employee.IsActive == false && department.Id == deptID
                        select new AllEmployeeVM
                        {
                            NIK = employee.NIK,
                            FirstName = employee.FirstName,
                            LastName = employee.LastName,
                            Email = employee.Email,
                            Phone = employee.Phone,
                            Address = employee.Address,
                            DepartmentName = department.Name,
                            IsActive = employee.IsActive

                        };

            return query.ToList();
        }
            
        public IEnumerable<DepartmentEmployeeCountVM> GetCountOfActiveEmployeesByDepartment()
        {
            var query = from employee in context.Employees
                        join department in context.Departments 
                        on employee.DepartmentId equals department.Id
                        where employee.IsActive == true
                        group department by department.Name into deptGroup
                        select new DepartmentEmployeeCountVM
                        {
                            DepartmentName = deptGroup.Key,
                            EmployeeCount = deptGroup.Count()
                        };

            return query.ToList();
        }

        public IEnumerable<DepartmentEmployeeCountVM> GetCountOfResignedEmployeesByDepartment()
        {
            var query = from employee in context.Employees
                        join department in context.Departments 
                        on employee.DepartmentId equals department.Id
                        where employee.IsActive == false
                        group department by department.Name into deptGroup
                        select new DepartmentEmployeeCountVM
                        {
                            DepartmentName = deptGroup.Key,
                            EmployeeCount = deptGroup.Count()
                        };

            return query.ToList();
        }
    }
}
