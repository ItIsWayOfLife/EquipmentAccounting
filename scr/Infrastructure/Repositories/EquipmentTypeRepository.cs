using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class EquipmentTypeRepository : IRepository<EquipmentType>
    {
        private readonly ApplicationContext _applicationContext;

        public EquipmentTypeRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public void Create(EquipmentType item)
        {
            _applicationContext.EquipmentTypes.Add(item);
        }

        public void Delete(int id)
        {
            EquipmentType equipmentType = _applicationContext.EquipmentTypes.Find(id);

            if (equipmentType != null)
            {
                _applicationContext.EquipmentTypes.Remove(equipmentType);
            }
        }

        public IEnumerable<EquipmentType> Find(Func<EquipmentType, bool> predicate)
        {
            return _applicationContext.EquipmentTypes.Where(predicate).ToList();
        }

        public EquipmentType Get(int id)
        {
            return _applicationContext.EquipmentTypes.Find(id);
        }

        public IEnumerable<EquipmentType> GetAll()
        {
            return _applicationContext.EquipmentTypes;
        }

        public void Update(EquipmentType item)
        {
            _applicationContext.Entry(item).State = EntityState.Modified;
        }
    }
}
