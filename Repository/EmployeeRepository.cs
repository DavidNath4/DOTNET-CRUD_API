using ExerciseAPI.Context;
using ExerciseAPI.Models;
using ExerciseAPI.Repository.Interface;
using ExerciseAPI.ViewModels;
using System.Diagnostics.Metrics;

namespace ExerciseAPI.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MyContext context;
        

        public EmployeeRepository(MyContext context)
        {
            this.context = context;
        }   

        public int Delete(string Id)
        {
            var data = context.Employees.Find(Id);
            context.Employees.Remove(data);
            return context.SaveChanges();
        }

        public int DeleteActive(string Id)
        {
            var data = context.Employees.Find(Id);
            if (data != null)
            {
                data.IsActive = false;
            }
            return context.SaveChanges();
        }


        public IEnumerable<Employee> Get()
        {
            return context.Employees.ToList();            
        }

        public Employee Get(string Id)
        {
            var findEmployee = context.Employees.Find(Id);
            return findEmployee;
        }

        public int Insert(EmployeeInsertVM employee)
        {

            var addEmployee = new Employee
            {
                NIK = GenerateUniqueNIK(),
                FirstName = employee.FirstName,
                LastName = employee.LastName,                
                Email = GenerateEmail(employee.FirstName, employee.LastName),
                Phone = employee.Phone,
                Address = employee.Address,
                IsActive = true,

                DepartmentId = employee.DepartmentId
            };
            context.Employees.Add(addEmployee);
            var result = context.SaveChanges();
            return result;           
        }

        public int Update(string NIK, EmployeeUpdateVM employee)
        {
            var updateEmployee = context.Employees.Find(NIK);
            if(updateEmployee != null)
            {
                updateEmployee.FirstName = employee.FirstName;
                updateEmployee.LastName = employee.LastName;
                updateEmployee.Phone = employee.Phone;
                updateEmployee.Address = employee.Address;
                updateEmployee.IsActive = employee.isActive;
                updateEmployee.DepartmentId = employee.DepartmentId;
                return context.SaveChanges();
            }
            return 0;
        }







        // HELPER METHOD


        private string GenerateUniqueNIK()
        {
            // Get the current date in the "ddmmyy" format
            string currentDate = DateTime.Now.ToString("ddMMyy");

            // Find the highest NIK value that starts with the current date
            var latestNIK = context.Employees
                .Where(e => e.NIK.StartsWith(currentDate))
                .OrderByDescending(e => e.NIK)
                .FirstOrDefault();

            // If there are no existing NIKs for the current date, start with "001"
            if (latestNIK == null)
            {
                return currentDate + "001";
            }

            // Extract the numeric part of the latest NIK and increment it by 1
            if (int.TryParse(latestNIK.NIK.Substring(6), out int lastNumber))
            {
                int nextNumber = lastNumber + 1;

                // Ensure the numeric part is always three digits long (e.g., "001", "012", "123")
                string nextNumberString = nextNumber.ToString().PadLeft(3, '0');

                return currentDate + nextNumberString;
            }

            // Default to a safe value if parsing fails
            return currentDate + "001";
        }

        public string GenerateEmail(string firstName, string lastName)
        {
            string baseEmail = $"{firstName.ToLower()}.{lastName.ToLower()}@berca.co.id";
            string generatedEmail = baseEmail;

            int emailCounter = 0;
            while (EmailExists(generatedEmail))
            {
                emailCounter++;
                generatedEmail = $"{firstName.ToLower()}.{lastName.ToLower()}{emailCounter}@berca.co.id";                
            }

            return generatedEmail;
        }

        private bool EmailExists(string email)
        {
            return context.Employees.Any(e => e.Email == email);
        }


        public bool IsEmployeeExsist(string NIK)
        {
            var result = context.Employees.FirstOrDefault(e => e.NIK == NIK);
            return result != null;
        }

        public bool IsPhoneDuplicate(string phone , string nik)
        {
            var isDuplicate = context.Employees.Any(e => e.Phone == phone && e.NIK != nik);
            return isDuplicate;
        }

    }
}
