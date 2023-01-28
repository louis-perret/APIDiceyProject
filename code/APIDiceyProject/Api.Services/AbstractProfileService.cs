using Api.Model;
using Api.Repositories.ProfileRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services
{
    public abstract class AbstractProfileService : IProfileService
    {
        #region attributs
        /// <summary>
        /// Repository gérant la logique des dés en base.
        /// </summary>
        private IProfileRepository _profileRepository;


        /// <summary>
        /// Logger de la classe.
        /// </summary>
        private ILogger<AbstractProfileService>? _logger;
        #endregion

        #region constructeurs
        protected AbstractProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        /// <summary>
        /// Constructeur complet.
        /// </summary>
        /// <param name="logger"> Logger de la classe. </param>
        /// <param name="diceRepository"> Repository gérant la logique des dés en base. </param>
        protected AbstractProfileService(ILogger<AbstractProfileService> logger, IProfileRepository diceRepository) : this(diceRepository)
        {
            _logger = logger;
        }
        #endregion

        #region méthodes
        async public Task<Profile?> AddProfile(Profile profile)
        {
            return await _profileRepository.AddProfile(profile);
        }

        async public Task<Profile?> GetProfileById(Guid id)
        {
            return await _profileRepository.GetProfileById(id);
        }

        async public Task<List<Profile>> GetProfilesByPage(int numPage, int nbByPage)
        {
            return await _profileRepository.ProfilesByPage(numPage, nbByPage);
        }

        async public Task<bool> RemoveAllProfiles()
        {
            return await _profileRepository.RemoveAllProfiles();
        }

        async public Task<bool> RemoveProfileById(Guid id)
        {
            return await _profileRepository.RemoveProfileById(id);
        }

        async public Task<bool> UpdateProfile(Profile profile)
        {
            return await _profileRepository.UpdateProfile(profile);
        }
        #endregion
    }
}
