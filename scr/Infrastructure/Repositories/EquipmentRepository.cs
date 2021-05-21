using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class EquipmentRepository : IRepository<Equipment>
    {
        private readonly ApplicationContext _applicationContext;

        public EquipmentRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public void Create(Equipment item)
        {
            _applicationContext.Equipments.Add(item);
        }

        public void Delete(int id)
        {
            Equipment equipment = _applicationContext.Equipments.Find(id);

            if (equipment != null)
            {
                _applicationContext.Equipments.Remove(equipment);
            }
        }

        public IEnumerable<Equipment> Find(Func<Equipment, bool> predicate)
        {
            return _applicationContext.Equipments.Include(p => p.Employee).Include(p => p.StatusEquipment).Where(predicate).ToList();
        }

        public Equipment Get(int id)
        {
            return _applicationContext.Equipments.Include(p => p.Employee).Include(p => p.StatusEquipment).FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Equipment> GetAll()
        {
            return _applicationContext.Equipments.Include(p => p.Employee).Include(p => p.StatusEquipment);
        }

        public void Update(Equipment item)
        {
            _applicationContext.Entry(item).State = EntityState.Modified;
        }
    }
}
