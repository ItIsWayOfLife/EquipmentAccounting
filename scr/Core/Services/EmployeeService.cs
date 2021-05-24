using Core.DTOs;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.Interfaces.Services;
using Core.Services.Common;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services
{
    public class EmployeeService : Service, IEmployeeService
    {
        private readonly IConverter<Employee, EmployeeDTO> _employeeConverter;

        public EmployeeService(IUnitOfWork uow, IConverter<Employee, EmployeeDTO> employeeConverter) : base(uow)
        {
            _employeeConverter = employeeConverter;
        }

        public void Add(EmployeeDTO model)
        {
            int positionId = GetPositionIdByPositionName(model.PositionName);
            int departmentId = GetDepartmentIdByDepartmentName(model.DepartmentName);
            
            var employee = _employeeConverter.ConvertDTOToModel(model);

            employee.PositionId = positionId;
            employee.DepartmentId = departmentId;

            Database.Employee.Create(employee);
            Database.Save();
        }

        public void Delete(int id)
        {
            Database.Employee.Delete(id);
            Database.Save();
        }

        public void Edit(EmployeeDTO model)
        {
            var employee = Database.Employee.Get(model.Id);

            if (employee == null)
            {
                throw new ValidationException("Работник не найден", string.Empty);
            }

            int positionId = GetPositionIdByPositionName(model.PositionName);
            int departmentId = GetDepartmentIdByDepartmentName(model.DepartmentName);

            employee.DateOfBirth = model.DateOfBirth;
            employee.FullName = model.FullName;
            employee.Phone = model.Phone;
            employee.Sex = model.Sex;

            employee.PositionId = positionId;
            employee.DepartmentId = departmentId;
          
            Database.Employee.Update(employee);
            Database.Save();
        }

        public EmployeeDTO Get(int id)
        {
            var employee = Database.Employee.Get(id);

            if (employee == null)
            {
                throw new ValidationException("Работник не найден", string.Empty);
            }

            var employeeDTO = _employeeConverter.ConvertModelToDTO(employee);
 
            return employeeDTO;
        }

        public IEnumerable<EmployeeDTO> GetAll()
        {
            var employees = Database.Employee.GetAll();

            var employeeDTOs = new List<EmployeeDTO>();

            foreach (var employee in employees)
            {
                var employeeDTO = _employeeConverter.ConvertModelToDTO(employee);
                employeeDTOs.Add(employeeDTO);
            }

            return employeeDTOs;
        }

        private int GetPositionIdByPositionName(string positionName)
        {
            int? positionId = Database.Position.Find(p => p.Name == positionName).FirstOrDefault().Id;

            if (positionId == null)
            {
                throw new ValidationException($"Не установлена должность", string.Empty);
            }

            return positionId.Value;
        }

        private int GetDepartmentIdByDepartmentName(string departmentName)
        {
            int? departmentId = Database.Department.Find(p => p.Name == departmentName).FirstOrDefault().Id;

            if (departmentId == null)
            {
                throw new ValidationException($"Не установлен отдел", string.Empty);
            }

            return departmentId.Value;
        }

        public EmployeeDTO GetIdName(int id)
        {
            var employee = Database.Employee.Get(id);

            if (employee == null)
            {
                throw new ValidationException("Работник не найден", string.Empty);
            }

            var employeeDTO = _employeeConverter.ConvertModelToDTO(employee);
            employeeDTO.FullName = employee.Id.ToString() + '|' + employee.FullName;

            return employeeDTO;
        }

        public IEnumerable<string> GetAllIdName()
        {
            var employees = Database.Employee.GetAll();
            var employeesNames = new List<string>();

            foreach (var employee in employees)
            {
                employeesNames.Add(employee.Id.ToString() +'|' +employee.FullName);
            }

            return employeesNames;
        }
    }
}
