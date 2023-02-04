using Api.EF;
using Api.Model.Throw;
using Api.Repositories.DiceRepository;
using Api.Repositories.ProfileRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Repositories.ThrowRepository
{
    /// <summary>
    /// Définit la logique du contrat de IThrowRepository.
    /// </summary>
    public abstract class AbstractThrowRepository : BaseRepository, IThrowRepository
    {
        #region attributs
        /// <summary>
        /// Repository pour pouvoir récupérer des dés pour chaque lancer.
        /// </summary>
        private IDiceRepository _diceRepository;

        private IProfileRepository _profileRepository;
        #endregion

        #region constructeur
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="context">DbContext pour accéder à la base de données.</param>
        /// <param name="diceRepository">DiceRepository pour récupérer des dés.</param>
        public AbstractThrowRepository(ApiDbContext context, IDiceRepository diceRepository, IProfileRepository profileRepository) : base(context)
        {
            this._diceRepository = diceRepository;
            this._profileRepository = profileRepository;
        }
        #endregion

        #region méthodes redéfinies

        /// <inheritdoc/>
        public async Task<Throw?> GetThrowById(Guid id)
        {
            var throwEntity = await _context.throws.Where(t => t.Id == id).FirstOrDefaultAsync();
            if (throwEntity == null) return null;
            return new Throw(throwEntity.Result, await _diceRepository.GetDiceById(throwEntity.DiceId), throwEntity.Id);
        }

        public Task<List<Throw>>? GetThrowByProfileId(Guid idProfile)
        {
            var profile = _profileRepository.GetProfileById(idProfile);
            if(profile == null)
            {
                return null;
            }

            return null;
        }

        #endregion
    }
}
