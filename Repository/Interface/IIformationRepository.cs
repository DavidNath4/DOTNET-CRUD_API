using ExerciseAPI.ViewModels;

namespace ExerciseAPI.Repository.Interface
{
    public interface IIformationRepository
    {
        // Active + Dept Name (get all active employee + dept name)
        public IEnumerable<AllEmployeeVM> GetAllActiveEmployee();

        // Resign + Dept Name (get all active employee + dept name)
        public IEnumerable<AllEmployeeVM> GetAllResignEmployee();

        // Active in Dept (search in dept active employee)
        public IEnumerable<AllEmployeeVM> GetAllActiveEmployeeInDept(string deptID);

        // Resign in Dept (search in dept resign employee)
        public IEnumerable<AllEmployeeVM> GetAllResignEmployeeInDept(string deptID);

        // Active Employee Count / Dept
        public IEnumerable<DepartmentEmployeeCountVM> GetCountOfActiveEmployeesByDepartment();

        // Resign Employee Count / Dept
        public IEnumerable<DepartmentEmployeeCountVM> GetCountOfResignedEmployeesByDepartment();
    }
}
