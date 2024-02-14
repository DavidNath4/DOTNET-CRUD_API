using ExerciseAPI.Models;
using ExerciseAPI.ViewModels;

namespace ExerciseAPI.Repository.Interface
{
    public interface IDepartmentRepository
    {
        public IEnumerable<Department> Get();
        public Department Get(string id);
        public int Insert (DepartmentInsertVM department);
        public int Update(string ID, DepartmentUpdateVM department);
        public int Delete(string Id);
    }
}
