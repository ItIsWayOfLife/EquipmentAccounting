using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.Interfaces.Services;
using Core.Services.Common;
using System.Collections.Generic;

namespace Core.Services
{
    public class PositionService : Service, IPositionService
    {
        public PositionService(IUnitOfWork uow) : base(uow)
        {

        }

        public IEnumerable<string> GetAllName()
        {
            var positions = Database.Position.GetAll();
            var positionsNames = new List<string>();

            foreach (var position in positions)
            {
                positionsNames.Add(position.Name);
            }

            return positionsNames;
        }

        public void Add(Position model)
        {
            Database.Position.Create(model);
            Database.Save();
        }

        public void Delete(int id)
        {
            Database.Position.Delete(id);
            Database.Save();
        }

        public void Edit(Position model)
        {
            var position = Database.Position.Get(model.Id);

            if (position == null)
            {
                throw new ValidationException("Должность не найдена", string.Empty);
            }

            position.Name = model.Name;

            Database.Position.Update(position);
            Database.Save();
        }

        public Position Get(int id)
        {
            var position = Database.Position.Get(id);

            if (position == null)
            {
                throw new ValidationException("Должность не найдена", string.Empty);
            }

            return position;
        }

        public IEnumerable<Position> GetAll()
        {
            return Database.Position.GetAll();
        }
    }
}

