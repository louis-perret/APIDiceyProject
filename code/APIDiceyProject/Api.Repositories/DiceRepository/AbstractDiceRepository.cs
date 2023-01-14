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
        public async Task<List<Dice>> GetDices()
        {
            return (await _context.dices.ToListAsync()).ToModel();
        }

        /// <inheritdoc/>
        public async Task<Dice?> GetDiceById(int id)
        {
            var diceEntity = await _context.dices.Where(dice => dice.NbFaces == id).FirstOrDefaultAsync();

            if (diceEntity == null) return null;

            return diceEntity.ToModel();
        }

        /// <inheritdoc/>
        public async Task<bool> RemoveAllDices()
        {
            try
            {
                await _context.dices.ExecuteDeleteAsync();
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> AddDice(Dice diceAdd)
        {
            try
            {
                if (await _context.dices.Where(dice => dice.NbFaces == diceAdd.NbFaces).FirstOrDefaultAsync() == null && diceAdd.NbFaces >0)
                {
                    await _context.dices.AddAsync(diceAdd.ToEntity());
                    await _context.SaveChangesAsync();
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

        /// <inheritdoc/>
        public async Task<bool> RemoveDiceById(int id)
        {
            try
            {
                var dice = await _context.dices.Where(dice => dice.NbFaces == id).FirstOrDefaultAsync();
                if (dice == null)
                    return false;
                await Task.FromResult(_context.dices.Remove(dice));
                await _context.SaveChangesAsync();
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
