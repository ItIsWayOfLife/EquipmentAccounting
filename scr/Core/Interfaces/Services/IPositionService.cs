using Core.Entities;
using Core.Interfaces.Services.Common;

namespace Core.Interfaces.Services
{
    public interface IPositionService : ICrudableService<Position>, IGetAllName
    {
    }
}
