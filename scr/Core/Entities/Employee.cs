using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public string Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }


        public int IdPosition { get; set; }
        public int IdDepartment { get; set; }

        public Position Position { get; set; }
        public Department Department { get; set; }

        public ICollection<Equipment> Equipments { get; set; }
    }
}
