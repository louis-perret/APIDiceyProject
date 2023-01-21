using Api.Model;
using ModelEntityExtensions;
using Microsoft.EntityFrameworkCore;
using Api.EF;
using Exceptions;

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
            //On tente de supprimer tout les dés de la base.
            try
            {
                await _context.dices.ExecuteDeleteAsync();
                await _context.SaveChangesAsync();
            }
            //Si l'action n'a pas pu être entreprise, on retourne faux
            catch (Exception)
            {
                return false;
            }

            //L'action s'est bien passé, on retourne vrai
            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> AddDice(Dice diceAdd)
        {
            //Si le dé à ajouter n'existe pas déjà
            if (await _context.dices.Where(dice => dice.NbFaces == diceAdd.NbFaces).FirstOrDefaultAsync() == null && diceAdd.NbFaces >0)
            {
                //On tente de l'enregistrer en base
                try
                {
                    await _context.dices.AddAsync(diceAdd.ToEntity());
                    await _context.SaveChangesAsync();
                }
                //Si l'enregistrement n'a pas réussi, on lance une exception
                catch (Exception e)
                {
                    throw new EntityFrameworkException(e.Message);
                }
                return true;
            }
            //Si le dé voulu existe déjà, on retourne faux
            else return false;
        }

        /// <inheritdoc/>
        public async Task<bool> RemoveDiceById(int id)
        {
            //On tente de supprimer le dé avec l'ID donné
            try
            {
                var dice = await _context.dices.Where(dice => dice.NbFaces == id).FirstOrDefaultAsync();
                
                //Si la base ne contient de dé avec l'identifiant donné, on retourne faux
                if (dice == null) return false;

                //On supprime et on sauvegarde la base
                await Task.FromResult(_context.dices.Remove(dice));
                await _context.SaveChangesAsync();
            }
            //Si il y a une erreur durant l'opération, on lance une EntityFrameworkException
            catch(Exception e)
            {
                throw new EntityFrameworkException(e.Message);
            }

            //Tout s'est bien bassé, on retourne vrai
            return true;
        }

        #endregion
    }
}
