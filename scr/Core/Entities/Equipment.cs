﻿
namespace Core.Entities
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string InventoryNumber { get; set; }
        public int? IdEmployee { get; set; }
        public int IdStatus { get; set; }
        public Employee Employee { get; set; }
        public StatusEquipment StatusEquipment { get; set; }
        public double Price { get; set; }
        public int Term { get; set; }
        public int ProcentYear { get; set; }
    }
}