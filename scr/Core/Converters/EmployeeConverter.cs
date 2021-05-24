using Core.DTOs;
using Core.Entities;
using Core.Interfaces;

namespace Core.Converters
{
    public class EmployeeConverter : IConverter<Employee, EmployeeDTO>
    {
        public Employee ConvertDTOToModel(EmployeeDTO modelDTO)
        {
            return new Employee()
            {
                Id = modelDTO.Id,
                DateOfBirth = modelDTO.DateOfBirth,
                FullName = modelDTO.FullName,
                Phone = modelDTO.Phone,
                Sex = modelDTO.Sex
            };
        }

        public EmployeeDTO ConvertModelToDTO(Employee model)
        {
            return new EmployeeDTO()
            {
                Id = model.Id,
                DateOfBirth = model.DateOfBirth,
                FullName = model.FullName,
                Phone = model.Phone,
                Sex = model.Sex,
                DepartmentName = model?.Department?.Name,
                PositionName = model?.Position?.Name
            };
        }
    }
}
