using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExerciseAPI.Models
{
    [Table("Employee")]
    public class Employee
    {        
        [Key]
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }

        // Foreign key
        public string DepartmentId {  get; set; }

        // Foreign navigation
        public Department Department { get; set; }

    }
}
