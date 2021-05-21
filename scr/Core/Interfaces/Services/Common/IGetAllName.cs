using System.Collections.Generic;

namespace Core.Interfaces.Services.Common
{
    /// <summary>
    /// Read all names.
    /// </summary>
    public interface IGetAllName
    {
        /// <summary>
        /// Get all names.
        /// </summary>
        /// <returns>All names.</returns>
        public IEnumerable<string> GetAllName();
    }
}
