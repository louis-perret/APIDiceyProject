﻿using Api.Model.Throw;
using Api.Repositories.ThrowRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.ThrowService
{
    /// <summary>
    /// Service abstrait pour les lancers.
    /// </summary>
    public abstract class AbstractThrowService : IThrowService
    {
        #region attributs
        /// <summary>
        /// Repository gérant la logique des lancers en base.
        /// </summary>
        private IThrowRepository _throwRepository;
        /// <summary>
        /// Logger de la classe.
        /// </summary>
        private ILogger<AbstractThrowService>? _logger;
        #endregion

        #region constructeurs

        /// <summary>
        /// Constructeur à un argument.
        /// </summary>
        /// <param name="throwRepository"> Repository gérant la logique des lancers en base. </param>
        protected AbstractThrowService(IThrowRepository throwRepository)
        {
            _throwRepository = throwRepository;
        }

        /// <summary>
        /// Constructeur complet.
        /// </summary>
        /// <param name="logger"> Logger de la classe. </param>
        /// <param name="diceRepository"> Repository gérant la logique des lancers en base. </param>
        protected AbstractThrowService(ILogger<AbstractThrowService> logger, IThrowRepository throwRepository) : this(throwRepository)
        {
            _logger = logger;
        }

        #endregion

        #region méthodes redéfinies

        /// <inheritdoc/>
        public async Task<Throw?> GetThrowById(Guid id)
        {
            return await _throwRepository.GetThrowById(id);       
        }

        /// <inheritdoc/>
        public async Task<List<Throw>> GetThrowByProfileId(Guid idProfile, int numPage, int nbByPage)
        {
            return await _throwRepository.GetThrowByProfileId(idProfile, numPage, nbByPage); 
        }

        /// <inheritdoc/>
        public async Task<Guid> AddThrow(int result, int nbFacesDe, Guid profileId)
        {
            return await _throwRepository.AddThrow(result, nbFacesDe, profileId);
        }

        /// <inheritdoc/>
        public async Task<bool> RemoveThrow(Guid id)
        {
            return await _throwRepository.RemoveThrow(id);
        }
        #endregion

    }
}
