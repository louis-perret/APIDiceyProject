using Api.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Model;

namespace Api.Repositories.DiceRepository
{
    /// <summary>
    /// Définit la logique du contrat de IDiceRepository.
    /// </summary>
    public class AbstractDiceRepository : BaseRepository, IDiceRepository
    {
        #region constructeur
        public AbstractDiceRepository() : base()
        {
        }
        #endregion

        #region méthodes redéfinies

        /// <inheritdoc/>
        public List<Dice> GetDices()
        {
            var dices = _context.dices.ToList();

            var modelDices = new List<Api.Model.Dice>();

            foreach(Api.Entities.Dice diceEntity in dices)
            {
                modelDices.Add(new Api.Model.SimpleDice(diceEntity.NbFaces));
            }

            return modelDices;
        }
        #endregion
    }
}
