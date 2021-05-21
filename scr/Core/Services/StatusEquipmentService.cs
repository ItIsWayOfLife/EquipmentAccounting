using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.Interfaces.Services;
using Core.Services.Common;
using System.Collections.Generic;

namespace Core.Services
{
   public class StatusEquipmentService : Service, IStatusEquipmentService
    {
        public StatusEquipmentService(IUnitOfWork uow) : base(uow)
        {

        }

        public IEnumerable<string> GetAllName()
        {
            var statusEquipments = Database.StatusEquipment.GetAll();
            var statusEquipmentsNames = new List<string>();

            foreach (var statusEquipment in statusEquipments)
            {
                statusEquipmentsNames.Add(statusEquipment.Name);
            }

            return statusEquipmentsNames;
        }
        public void Add(StatusEquipment model)
        {
            Database.StatusEquipment.Create(model);
            Database.Save();
        }

        public void Delete(int id)
        {
            Database.StatusEquipment.Delete(id);
            Database.Save();
        }

        public void Edit(StatusEquipment model)
        {
            var statusEquipment = Database.StatusEquipment.Get(model.Id);

            if (statusEquipment == null)
            {
                throw new ValidationException("Состояние оборудования не найдено", string.Empty);
            }

            statusEquipment.Name = model.Name;

            Database.StatusEquipment.Update(statusEquipment);
            Database.Save();
        }

        public StatusEquipment Get(int id)
        {
            var statusEquipment = Database.StatusEquipment.Get(id);

            if (statusEquipment == null)
            {
                throw new ValidationException("Состояние оборудования не найдено", string.Empty);
            }

            return statusEquipment;
        }

        public IEnumerable<StatusEquipment> GetAll()
        {
            return Database.StatusEquipment.GetAll();
        }
    }
}
