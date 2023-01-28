using Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.DiceFolder
{
    /// <summary>
    /// Contrat définissant les méthodes que doit implémenter un DiceService.
    /// </summary>
    public interface IDiceService
    {

        /// <summary>
        /// Récupère la liste complète des dés en base.
        /// </summary>
        /// <returns> La liste complète des dés en base. </returns>
        Task<List<Dice>> GetDices();

        /// <summary>
        /// Récupère un dé avec son nombre de faces.
        /// </summary>
        /// <param name="id">Nombre de faces</param>
        /// <returns>Un dé potentiellement NULL</returns>
        Task<Dice?> GetDiceById(int id);

        /// <summary>
        /// Supprime tous les dés.
        /// </summary>
        /// <returns>True si correctement supprimés.</returns>
        Task<bool> RemoveAllDices();

        /// <summary>
        /// Ajoute un dé en base.
        /// </summary>
        /// <param name="dice"> Dé à enregistrer. </param>
        /// <returns> Vrai si l'instertion a pu s'effectuer, faux autrement. </returns>
        Task<bool> AddDice(Dice dice);

        /// <summary>
        /// Supprime un dé suivant son id.
        /// </summary>
        /// <param name="id">Id du dé à supprimer.</param>
        /// <returns>True si bien supprimé, faux autrement.</returns>
        Task<bool> RemoveDiceById(int id);
    }
}
