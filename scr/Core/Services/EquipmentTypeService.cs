using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.Interfaces.Services;
using Core.Services.Common;
using System.Collections.Generic;

namespace Core.Services
{
    public class EquipmentTypeService : Service, IEquipmentTypeService
    {
        public EquipmentTypeService(IUnitOfWork uow) : base(uow)
        {

        }

        public IEnumerable<string> GetAllName()
        {
            var equipmentTypes = Database.EquipmentType.GetAll();
            var equipmentTypesNames = new List<string>();

            foreach (var equipmentType in equipmentTypes)
            {
                equipmentTypesNames.Add(equipmentType.Name);
            }

            return equipmentTypesNames;
        }

        public void Add(EquipmentType model)
        {
            Database.EquipmentType.Create(model);
            Database.Save();
        }

        public void Delete(int id)
        {
            Database.EquipmentType.Delete(id);
            Database.Save();
        }

        public void Edit(EquipmentType model)
        {
            var equipmentType = Database.EquipmentType.Get(model.Id);

            if (equipmentType == null)
            {
                throw new ValidationException("Тип не найдена", string.Empty);
            }

            equipmentType.Name = model.Name;

            Database.EquipmentType.Update(equipmentType);
            Database.Save();
        }

        public EquipmentType Get(int id)
        {
            var equipmentType = Database.EquipmentType.Get(id);

            if (equipmentType == null)
            {
                throw new ValidationException("Тип не найдена", string.Empty);
            }

            return equipmentType;
        }

        public IEnumerable<EquipmentType> GetAll()
        {
            return Database.EquipmentType.GetAll();
        }
    }
}
