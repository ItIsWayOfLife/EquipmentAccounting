using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.Interfaces.Services;
using Core.Services.Common;
using System.Collections.Generic;

namespace Core.Services
{
    public class DepartmentService : Service, IDepartmentService
    {
        public DepartmentService(IUnitOfWork uow) : base(uow)
        {

        }

        public IEnumerable<string> GetAllName()
        {
            var departments = Database.Department.GetAll();
            var departmentsNames = new List<string>();

            foreach (var department in departments)
            {
                departmentsNames.Add(department.Name);
            }

            return departmentsNames;
        }

        public void Add(Department model)
        {
            Database.Department.Create(model);
            Database.Save();
        }

        public void Delete(int id)
        {
            Database.Department.Delete(id);
            Database.Save();
        }

        public void Edit(Department model)
        {
            var department = Database.Department.Get(model.Id);

            if (department == null)
            {
                throw new ValidationException("Отдел не найден", string.Empty);
            }

            department.Name = model.Name;

            Database.Department.Update(department);
            Database.Save();
        }

        public Department Get(int id)
        {
            var department = Database.Department.Get(id);

            if (department == null)
            {
                throw new ValidationException("Отдел не найден", string.Empty);
            }

            return department;
        }

        public IEnumerable<Department> GetAll()
        {
            return Database.Department.GetAll();
        }
    }
}
