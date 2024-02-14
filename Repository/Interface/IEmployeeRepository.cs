using ExerciseAPI.Models;
using ExerciseAPI.ViewModels;

namespace ExerciseAPI.Repository.Interface
{
    public interface IEmployeeRepository
    {
        public IEnumerable<Employee> Get();
        public Employee Get(string Id);
        public int Insert(EmployeeInsertVM employee);
        public int Update(string NIK, EmployeeUpdateVM employee);
        public int Delete(string Id);
    }
}
