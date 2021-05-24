using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data.Entities;
using System;

namespace Infrastructure.Repositories.UnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _applicationContext;

        private DepartmentRepository _departmentRepository;
        private EmployeeRepository _employeeRepository;
        private EquipmentRepository _equipmentRepository;
        private PositionRepository _positionRepository;
        private StatusEquipmentRepository _statusEquipmentRepository;
        private EquipmentTypeRepository _equipmentTypeRepository;

        public EFUnitOfWork(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public IRepository<EquipmentType> EquipmentType
        {
            get
            {
                if (_equipmentTypeRepository == null)
                {
                    _equipmentTypeRepository = new EquipmentTypeRepository(_applicationContext);
                }
                return _equipmentTypeRepository;
            }
        }

        public IRepository<Department> Department
        {
            get
            {
                if (_departmentRepository == null)
                {
                    _departmentRepository = new DepartmentRepository(_applicationContext);
                }
                return _departmentRepository;
            }
        }

        public IRepository<Employee> Employee
        {
            get
            {
                if (_employeeRepository == null)
                {
                    _employeeRepository = new EmployeeRepository(_applicationContext);
                }
                return _employeeRepository;
            }
        }

        public IRepository<Equipment> Equipment
        {
            get
            {
                if (_equipmentRepository == null)
                {
                    _equipmentRepository = new EquipmentRepository(_applicationContext);
                }
                return _equipmentRepository;
            }
        }

        public IRepository<Position> Position
        {
            get
            {
                if (_positionRepository == null)
                {
                    _positionRepository = new PositionRepository(_applicationContext);
                }
                return _positionRepository;
            }
        }

        public IRepository<StatusEquipment> StatusEquipment
        {
            get
            {
                if (_statusEquipmentRepository == null)
                {
                    _statusEquipmentRepository = new StatusEquipmentRepository(_applicationContext);
                }
                return _statusEquipmentRepository;
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _applicationContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _applicationContext.SaveChanges();
        }
    }
}
