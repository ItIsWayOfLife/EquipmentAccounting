using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    /// <summary>
    /// Converter entity model to DTO model.
    /// </summary>
    /// <typeparam name="TModel">Model entity.</typeparam>
    /// <typeparam name="TModelDTO">Model DTO.</typeparam>
    public interface IConverter<TModel, TModelDTO>
       where TModel : class
       where TModelDTO : class
    {
        /// <summary>
        /// Convert model DTO to model entity.
        /// </summary>
        /// <param name="modelDTO">Model DTO.</param>
        /// <returns>Model entity.</returns>
        TModel ConvertDTOToModel(TModelDTO modelDTO);

        /// <summary>
        /// Convert model entity to model DTO.
        /// </summary>
        /// <param name="model">Model entity.</param>
        /// <returns>Model DTO.</returns>
        TModelDTO ConvertModelToDTO(TModel model);
    }
}
