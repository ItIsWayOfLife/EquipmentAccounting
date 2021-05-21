using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
        public class PositionRepository : IRepository<Position>
        {
            private readonly ApplicationContext _applicationContext;

            public PositionRepository(ApplicationContext applicationContext)
            {
                _applicationContext = applicationContext;
            }

            public void Create(Position item)
            {
                _applicationContext.Positions.Add(item);
            }

            public void Delete(int id)
            {
            Position position = _applicationContext.Positions.Find(id);

                if (position != null)
                {
                    _applicationContext.Positions.Remove(position);
                }
            }

            public IEnumerable<Position> Find(Func<Position, bool> predicate)
            {
                return _applicationContext.Positions.Include(p => p.Employees).Where(predicate).ToList();
            }

            public Position Get(int id)
            {
                return _applicationContext.Positions.Find(id);
            }

            public IEnumerable<Position> GetAll()
            {
                return _applicationContext.Positions;
            }

            public void Update(Position item)
            {
                _applicationContext.Entry(item).State = EntityState.Modified;
            }
        }
    }
