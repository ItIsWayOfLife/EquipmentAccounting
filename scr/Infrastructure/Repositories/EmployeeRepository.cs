using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
   public  class EmployeeRepository : IRepository<Employee>
    {
        private readonly ApplicationContext _applicationContext;

        public EmployeeRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public void Create(Employee item)
        {
            _applicationContext.Employees.Add(item);
        }

        public void Delete(int id)
        {
            Employee employee = _applicationContext.Employees.Find(id);

            if (employee != null)
            {
                _applicationContext.Employees.Remove(employee);
            }
        }

        public IEnumerable<Employee> Find(Func<Employee, bool> predicate)
        {
            return _applicationContext.Employees.Include(p => p.Position).Include(p => p.Department).Where(predicate).ToList();
        }

        public Employee Get(int id)
        {
            return _applicationContext.Employees.Include(p => p.Position).Include(p => p.Department).FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Employee> GetAll()
        {
            return _applicationContext.Employees.Include(p => p.Position).Include(p => p.Department);
        }

        public void Update(Employee item)
        {
            _applicationContext.Entry(item).State = EntityState.Modified;
        }
    }
}
