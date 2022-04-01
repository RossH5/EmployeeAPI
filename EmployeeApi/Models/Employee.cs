namespace EmployeeApi.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string HireDate { get; set; }
        public int ProjectId { get; set; }
        public int DepartmentId { get; set; }
        public string Salary { get; set; }
    }
}
