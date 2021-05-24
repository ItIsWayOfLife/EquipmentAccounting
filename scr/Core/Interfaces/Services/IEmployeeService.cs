using Core.DTOs;
using Core.Interfaces.Services.Common;
using System.Collections.Generic;

namespace Core.Interfaces.Services
{
    public interface IEmployeeService : ICrudableService<EmployeeDTO>
    {
        public IEnumerable<string> GetAllIdName();
        public EmployeeDTO GetIdName(int id);
    }
}
