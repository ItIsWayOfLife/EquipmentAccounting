
namespace Core.Entities
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string InventoryNumber { get; set; }
        public int? EmployeeId { get; set; }
        public int StatusEquipmentId { get; set; }
        public int EquipmentTypeId { get; set; }
        public Employee Employee { get; set; }
        public StatusEquipment StatusEquipment { get; set; }
        public EquipmentType EquipmentType { get; set; }
        public double Price { get; set; }
        public int Term { get; set; }
        public int ProcentYear { get; set; }
    }
}
