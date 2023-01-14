using Api.Model;
using Api.Repositories.DiceRepository;
using Microsoft.Extensions.Logging;

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
        /// <inheritdoc/>
        public async Task<List<Dice>> GetDices()
        {
            return await _diceRepository.GetDices();
        }

        /// <inheritdoc/>
        public async Task<Dice?> GetDiceById(int id)
        {
            return await _diceRepository.GetDiceById(id);
        }

        /// <inheritdoc/>
        public async Task<bool> RemoveAllDices()
        {
            try
            {
               await _diceRepository.RemoveAllDices();
            }
            catch (Exception e)
            {
                throw;
            }
                
            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> AddDice(Dice dice)
        {
            try
            {
                return await _diceRepository.AddDice(dice);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> RemoveDiceById(int id)
        {
            try
            {
                return await _diceRepository.RemoveDiceById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
