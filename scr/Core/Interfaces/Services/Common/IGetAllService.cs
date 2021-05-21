using System.Collections.Generic;

namespace Core.Interfaces.Services.Common
{
    /// <summary>
    /// Read all models.
    /// </summary>
    /// <typeparam name="T">Model.</typeparam>
    public interface IGetAllService<T> where T : class
    {
        /// <summary>
        /// Get all models.
        /// </summary>
        /// <returns>All Models.</returns>
        public IEnumerable<T> GetAll();
    }
}
