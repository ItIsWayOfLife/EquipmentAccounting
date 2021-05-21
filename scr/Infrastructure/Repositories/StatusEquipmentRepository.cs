using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class StatusEquipmentRepository : IRepository<StatusEquipment>
    {
        private readonly ApplicationContext _applicationContext;

        public StatusEquipmentRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public void Create(StatusEquipment item)
        {
            _applicationContext.StatusEquipments.Add(item);
        }

        public void Delete(int id)
        {
            StatusEquipment statusEquipment = _applicationContext.StatusEquipments.Find(id);

            if (statusEquipment != null)
            {
                _applicationContext.StatusEquipments.Remove(statusEquipment);
            }
        }

        public IEnumerable<StatusEquipment> Find(Func<StatusEquipment, bool> predicate)
        {
            return _applicationContext.StatusEquipments.Include(p => p.Equipments).Where(predicate).ToList();
        }

        public StatusEquipment Get(int id)
        {
            return _applicationContext.StatusEquipments.Find(id);
        }

        public IEnumerable<StatusEquipment> GetAll()
        {
            return _applicationContext.StatusEquipments;
        }

        public void Update(StatusEquipment item)
        {
            _applicationContext.Entry(item).State = EntityState.Modified;
        }
    }
}
