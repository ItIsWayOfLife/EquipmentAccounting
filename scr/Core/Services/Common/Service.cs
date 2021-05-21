using Core.Interfaces;
using System;

namespace Core.Services.Common
{
    /// <summary>
    /// Abstract repository for use work with UnitOfWork.
    /// </summary>
    public abstract class Service : IDisposable
    {
        /// <summary>
        /// Work with UnitOfWork.
        /// </summary>
        protected IUnitOfWork Database { get; set; }

        public Service(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
