using Api.Model.Throw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Repositories.ThrowRepository
{
    /// <summary>
    /// Contrat pour les ThrowRepository.
    /// </summary>
    public interface IThrowRepository
    {
        /// <summary>
        /// Récupère un lancer par son id.
        /// </summary>
        /// <param name="id">Id du lancer à récupérer.</param>
        /// <returns>Le lancer voulu</returns>
        public Task<Throw?> GetThrowById(Guid id);
    }
}
