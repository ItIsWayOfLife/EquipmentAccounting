using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using System.Linq;

namespace Core.Converters
{
    public class EquipmentConverter : IConverter<Equipment, EquipmentDTO>
    {
        public Equipment ConvertDTOToModel(EquipmentDTO modelDTO)
        {
            return new Equipment()
            {
                Id = modelDTO.Id,
                InventoryNumber = modelDTO.InventoryNumber,
                Name = modelDTO.Name,
                Price = modelDTO.Price,
                ProcentYear = modelDTO.ProcentYear,
                Term = modelDTO.Term
            };
        }

        public EquipmentDTO ConvertModelToDTO(Equipment model)
        {
            return new EquipmentDTO()
            {
                Id = model.Id,
                EmployeeFullName = model.Employee.FullName,
                EmployeeId = model.EmployeeId,
                InventoryNumber = model.InventoryNumber,
                Name = model.Name,
                Price = model.Price,
                ProcentYear = model.ProcentYear,
                StatusEquipmentName = model.StatusEquipment.Name,
                Term = model.Term,
                DeductionAmountPerMonth = new string(Calculate_Month_Amort(model.Price, model.ProcentYear, model.Term).Take(5).ToArray())
            };
        }

        private static string Calculate_Month_Amort(double Price, int year, int term)
        {
            double yearNorm = 1.0 / term;
            double everyYear = Price * yearNorm;
            return (everyYear / 12).ToString();
        }
    }
}
