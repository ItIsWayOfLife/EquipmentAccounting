using Core.Entities;
using System;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Department> Department { get; }
        IRepository<Employee> Employee { get; }
        IRepository<Equipment> Equipment { get; }
        IRepository<Position> Position { get; }
        IRepository<StatusEquipment> StatusEquipment { get; }
        IRepository<EquipmentType> EquipmentType { get; }

        void Save();
    }
}
