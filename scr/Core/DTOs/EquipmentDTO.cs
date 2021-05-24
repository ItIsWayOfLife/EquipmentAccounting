
namespace Core.DTOs
{
    public class EquipmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string InventoryNumber { get; set; }
        public int? EmployeeId { get; set; }
        public string EmployeeFullName { get; set; }
        public string StatusEquipmentName { get; set; }
        public double Price { get; set; }
        public int Term { get; set; }
        public int ProcentYear { get; set; }

        // сумма отчисления в месяц
        public string DeductionAmountPerMonth { get; set; }
    }
}
