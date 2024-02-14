using ExerciseAPI.Context;
using ExerciseAPI.Models;
using ExerciseAPI.Repository.Interface;
using ExerciseAPI.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace ExerciseAPI.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly MyContext context;
        private static int counter = 0;

        public DepartmentRepository(MyContext context)
        {
            this.context = context;
            InitializeCounter();

        }

        public int Delete(string Id)
        {
            var dept = context.Departments.Find(Id);
            context.Departments.Remove(dept);
            return context.SaveChanges();
        }

        public IEnumerable<Department> Get()
        {
            return context.Departments.ToList();
        }

        public Department Get(string id)
        {
            var findDept = context.Departments.Find(id);
            return findDept;
        }

        public int Insert(DepartmentInsertVM department)
        {
            var addDept = new Department
            {
                Id = GenerateUniqueID(),
                Name = department.Name,
            };
            context.Departments.Add(addDept);
            var result = context.SaveChanges();
            return result;
        }

        public int Update(string ID, DepartmentUpdateVM department)
        {
            var newDept = context.Departments.Find(ID);
            if (newDept != null)
            {
                newDept.Name = department.NewName;
                return context.SaveChanges();
            }
            return 0;
        }







        // HELPER

        private string GenerateUniqueID()
        {
            counter++; // Increment the counter for each call
            string uniqueNumber = counter.ToString().PadLeft(3, '0');
            string newID = "D" + uniqueNumber;
            return newID;
        }

        private void InitializeCounter()
        {
            // Find the maximum department ID from the database and set the counter accordingly
            var maxDepartmentId = context.Departments.Max(d => d.Id);
            if (!string.IsNullOrEmpty(maxDepartmentId) && maxDepartmentId.StartsWith("D"))
            {
                if (int.TryParse(maxDepartmentId.Substring(1), out int maxCounter))
                {
                    counter = maxCounter;
                }
            }
        }


        // Validator

        public bool IsDepartmentExsist(string ID)
        {
            var result = context.Departments.FirstOrDefault(e => e.Id == ID);
            return result != null;
        }


        public bool IsEmployeesDeptNull(string ID)
        {
            var department = context.Departments.Include(d => d.Employees).FirstOrDefault(d => d.Id == ID);

            bool hasEmployees = department != null && department.Employees.Any();

            return !hasEmployees;
        }

        public bool IsDepartmentNameDuplicate(string name)
        {

            return context.Departments.Any(d => d.Name == name);
        }

        public bool IsDepartmentNameDuplicate(string name, string currentDepartmentId)
        {
            return context.Departments.Any(d => d.Name == name && d.Id != currentDepartmentId);
        }



    }
}
