using Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Repositories.DiceRepository
{
    /// <summary>
    /// Contrat pour les DiceRepository.
    /// </summary>
    public interface IDiceRepository
    {
        /// <summary>
        /// Retourne l'intégralité des dés stockés en base.
        /// </summary>
        /// <returns> Les dés stockés en base. </returns>
        List<Dice> GetDices();

        /// <summary>
        /// Retourne le dé identifié par le nombre de faces en paramètre.
        /// </summary>
        /// <param name="id"> Nombre de faces du dé voulu. </param>
        /// <returns></returns>
        Dice? GetDiceById(int id);

        /// <summary>
        /// Supprime tous les dés.
        /// </summary>
        /// <returns>True si correctement supprimés.</returns>
        bool RemoveAllDices();

        /// <summary>
        /// Ajoute un dé en base.
        /// </summary>
        /// <param name="dice"> Le dé à ajouter. </param>
        /// <returns> Vrai si le dé a pu être ajouté, faux autrement. </returns>
        bool AddDice(Dice dice);
        bool RemoveDiceById(int id);
    }
}
