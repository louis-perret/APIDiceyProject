using Api.Model;
using Api.Repositories.DiceRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services
{
    /// <summary>
    /// Service abstrait pour les Dés.
    /// </summary>
    public abstract class AbstractDiceService : IDiceService
    {
        #region attributs
        /// <summary>
        /// Repository gérant la logique des dés en base.
        /// </summary>
        private IDiceRepository _diceRepository;


        /// <summary>
        /// Logger de la classe.
        /// </summary>
        private ILogger<AbstractDiceService>? _logger;
        #endregion

        #region constructeurs

        /// <summary>
        /// Constructeur à un argument.
        /// </summary>
        /// <param name="diceRepository"> Repository gérant la logique des dés en base. </param>
        protected AbstractDiceService(IDiceRepository diceRepository)
        {
            _diceRepository = diceRepository;
        }

        /// <summary>
        /// Constructeur complet.
        /// </summary>
        /// <param name="logger"> Logger de la classe. </param>
        /// <param name="diceRepository"> Repository gérant la logique des dés en base. </param>
        protected AbstractDiceService(ILogger<AbstractDiceService> logger, IDiceRepository diceRepository) : this(diceRepository)
        {
            _logger = logger;
        }

        #endregion

        #region méthodes redéfinies

        /// <summary>
        /// Récupère la liste complète des dés en base.
        /// </summary>
        /// <returns> La liste complète des dés en base. </returns>
        public List<Dice> GetDices()
        {
            return _diceRepository.GetDices();
        }

        /// <summary>
        /// Récupère un dé avec son nombre de faces.
        /// </summary>
        /// <param name="id">Nombre de faces</param>
        /// <returns>Un dé potentiellement NULL</returns>
        public Dice? GetDiceById(int id)
        {
            return _diceRepository.GetDiceById(id);
        }

        /// <summary>
        /// Supprime tous les dés.
        /// </summary>
        /// <returns>True si correctement supprimés.</returns>
        bool RemoveAllDices()
        {
            _diceRepository.RemoveAllDices();
            return true;
        }

        bool IDiceService.RemoveAllDices()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
