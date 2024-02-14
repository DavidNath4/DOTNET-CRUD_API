namespace ExerciseAPI.ViewModels
{
    public class EmployeeUpdateVM
    {
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool isActive {  get; set; }

        public string DepartmentId { get; set; }
    }
}
