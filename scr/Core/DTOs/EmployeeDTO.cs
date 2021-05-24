using System;

namespace Core.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }


        public string PositionName { get; set; }
        public string DepartmentName { get; set; }
    }
}
