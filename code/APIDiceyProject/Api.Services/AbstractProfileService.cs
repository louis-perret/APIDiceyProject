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
        public Profile? AddProfile(Profile profile)
        {
            return _profileRepository.AddProfile(profile);
        }

        public Profile? GetProfileById(Guid id)
        {
            return _profileRepository.GetProfileById(id);
        }

        public List<Profile> GetProfilesByPage(int numPage, int nbByPage)
        {
            return _profileRepository.ProfilesByPage(numPage, nbByPage);
        }

        public bool RemoveAllProfiles()
        {
            return _profileRepository.RemoveAllProfiles();
        }

        public bool RemoveProfileById(Guid id)
        {
            return _profileRepository.RemoveProfileById(id);
        }

        public bool UpdateProfile(Profile profile)
        {
            return _profileRepository.UpdateProfile(profile);
        }
        #endregion
    }
}
