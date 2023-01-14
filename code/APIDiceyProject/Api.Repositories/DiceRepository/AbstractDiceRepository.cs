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
        public AbstractDiceRepository(ApiDbContext context) : base(context)
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
        public Dice? GetDiceById(int id)
        {
            var diceEntity = _context.dices.Where(dice => dice.NbFaces == id).FirstOrDefault();

            if (diceEntity == null) return null;

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
                throw;
            }

            return true;
        }

        public bool AddDice(Dice diceAdd)
        {
            try
            {
                if (_context.dices.Where(dice => dice.NbFaces == diceAdd.NbFaces).FirstOrDefault()==null && diceAdd.NbFaces >0)
                {
                    _context.dices.Add(diceAdd.ToEntity());
                    _context.SaveChanges();
                    return true;
                }
                else 
                    return false;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public bool RemoveDiceById(int id)
        {
            try
            {
                var dice = _context.dices.Where(dice => dice.NbFaces == id).FirstOrDefault();
                if (dice == null)
                    return false;
                _context.dices.Remove(dice);
                _context.SaveChanges();
            }
            catch(Exception)
            {
                throw;
            }
            return true;
        }

        #endregion
    }
}
