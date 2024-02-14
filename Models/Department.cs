using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExerciseAPI.Models
{
    [Table("Department")]
    public class Department
    {
        [Key]        
        public string Id { get; set; }
        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; }   
    }
}
