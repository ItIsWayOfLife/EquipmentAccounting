using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Department> Department { get; }
        IRepository<Employee> Employee { get; }
        IRepository<Equipment> Equipment { get; }
        IRepository<Position> Position { get; }
        IRepository<StatusEquipment> StatusEquipment { get; }

        void Save();
    }
}
