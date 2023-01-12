using Api.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Model;
using ModelEntityExtensions;
using Microsoft.EntityFrameworkCore;

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
            return _context.dices.ToList().ToModel();
        }

        /// <inheritdoc/>
        public Dice GetDiceById(int id)
        {
            var diceEntity = _context.dices.Where(dice => dice.NbFaces == id).FirstOrDefault();

            if (diceEntity == null) throw new Exception("No dice with this ID");

            return diceEntity.ToModel();
        }

        /// <inheritdoc/>
        public bool RemoveAllDices()
        {
            try
            {
                _context.dices.ExecuteDelete();
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool AddDice(Dice dice)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
