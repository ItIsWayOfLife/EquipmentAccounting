using System.Collections.Generic;

namespace Core.Entities
{
    public class StatusEquipment
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Equipment> Equipments { get; set; }
    }
}
