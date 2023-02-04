using Api.Model.Throw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.ThrowService
{
    /// <summary>
    /// Contrat définissant les méthodes que doit implémenter un ThrowService.
    /// </summary>
    public interface IThrowService
    {
        /// <summary>
        /// Récupère un lancer par son id.
        /// </summary>
        /// <param name="id">Id du lancer à récupérer.</param>
        /// <returns>Le lancer voulu</returns>
        public Task<Throw?> GetThrowById(Guid id);

        public Task<List<Throw>> GetThrowByProfileId(Guid idProfile);
    }
}
