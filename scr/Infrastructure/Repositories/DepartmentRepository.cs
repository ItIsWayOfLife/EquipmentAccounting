using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class DepartmentRepository : IRepository<Department>
    {
        private readonly ApplicationContext _applicationContext;

        public DepartmentRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public void Create(Department item)
        {
            _applicationContext.Departments.Add(item);
        }

        public void Delete(int id)
        {
            Department department = _applicationContext.Departments.Find(id);

            if (department != null)
            {
                _applicationContext.Departments.Remove(department);
            }
        }

        public IEnumerable<Department> Find(Func<Department, bool> predicate)
        {
            return _applicationContext.Departments.Include(p => p.Employees).Where(predicate).ToList();
        }

        public Department Get(int id)
        {
            return _applicationContext.Departments.Find(id);
        }

        public IEnumerable<Department> GetAll()
        {
            return _applicationContext.Departments;
        }

        public void Update(Department item)
        {
            _applicationContext.Entry(item).State = EntityState.Modified;
        }
    }
}
