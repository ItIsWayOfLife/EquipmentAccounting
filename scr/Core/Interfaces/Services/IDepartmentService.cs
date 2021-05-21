using Core.Entities;
using Core.Interfaces.Services.Common;

namespace Core.Interfaces.Services
{
    public interface IDepartmentService : ICrudableService<Department>, IGetAllName
    {

    }
}
