using System.Collections.Generic;

namespace Core.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int IdPosition { get; set; }
        public int IdDepartment { get; set; }

        public Position Position { get; set; }
        public Department Department { get; set; }

        public ICollection<Equipment> Equipments { get; set; }
    }
}
