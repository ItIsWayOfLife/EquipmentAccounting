using Core.DTOs;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.Interfaces.Services;
using Core.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services
{
    public class EquipmentService : Service, IEquipmentService
    {
        private readonly IConverter<Equipment, EquipmentDTO> _equipmentConverter;

        public EquipmentService(IUnitOfWork uow, 
            IConverter<Equipment, EquipmentDTO> equipmentConverter) : base(uow)
        {
            _equipmentConverter = equipmentConverter;
        }

        public void Add(EquipmentDTO model)
        {
            int employeeId = GetEmployeeIdByEmployeeIdName(model.EmployeeFullName);
            int statusEquipmentId = GetStatusEquipmentIdByStatusEquipmentName(model.StatusEquipmentName);

            var equipment = _equipmentConverter.ConvertDTOToModel(model);

            equipment.EmployeeId = employeeId;
            equipment.StatusEquipmentId = statusEquipmentId;

            Database.Equipment.Create(equipment);
            Database.Save();
        }

        public void Delete(int id)
        {
            Database.Equipment.Delete(id);
            Database.Save();
        }

        public void Edit(EquipmentDTO model)
        {
            var equipment = Database.Equipment.Get(model.Id);

            if (equipment == null)
            {
                throw new ValidationException("Оборудование не найден", string.Empty);
            }

            int employeeId = GetEmployeeIdByEmployeeIdName(model.EmployeeFullName);
            int statusEquipmentId = GetStatusEquipmentIdByStatusEquipmentName(model.StatusEquipmentName);

            equipment.InventoryNumber = model.InventoryNumber;
            equipment.Name = model.Name;
            equipment.Price = model.Price;
            equipment.ProcentYear = model.ProcentYear;
            equipment.Term = model.Term;

            equipment.EmployeeId = employeeId;
            equipment.StatusEquipmentId = statusEquipmentId;

            Database.Equipment.Update(equipment);
            Database.Save();
        }

        public EquipmentDTO Get(int id)
        {
            var equipment = Database.Equipment.Get(id);

            if (equipment == null)
            {
                throw new ValidationException("Оборудование не найден", string.Empty);
            }

            var equipmentDTO = _equipmentConverter.ConvertModelToDTO(equipment);

            return equipmentDTO;
        }

        public IEnumerable<EquipmentDTO> GetAll()
        {
            var equipments = Database.Equipment.GetAll();

            var equipmentDTOs = new List<EquipmentDTO>();

            foreach (var equipment in equipments)
            {
                equipmentDTOs.Add(_equipmentConverter.ConvertModelToDTO(equipment));
            }

            return equipmentDTOs;
        }

        private int GetStatusEquipmentIdByStatusEquipmentName(string statusEquipmentName)
        {
            int? statusEquipmentId = Database.StatusEquipment.Find(p => p.Name == statusEquipmentName).FirstOrDefault().Id;

            if (statusEquipmentId == null)
            {
                throw new ValidationException($"Не установлен статус", string.Empty);
            }

            return statusEquipmentId.Value;
        }

        private int GetEmployeeIdByEmployeeIdName(string employeeIdName)
        {
            string[] arrayIdName = employeeIdName.Split('|');

            int? employeeId = Convert.ToInt32(arrayIdName[0]);

            if (employeeId == null)
            {
                throw new ValidationException($"Не установлен работник", string.Empty);
            }

            return employeeId.Value;
        }

    }
}
