using Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services
{
    public interface IProfileService
    {
        /// <summary>
        /// Récupère la liste complète des dés en base.
        /// </summary>
        /// <returns> La liste complète des dés en base. </returns>
        List<Profile> GetProfilesByPage(int numPage, int nbByPage);

        /// <summary>
        /// Récupère un dé avec son nombre de faces.
        /// </summary>
        /// <param name="id">Nombre de faces</param>
        /// <returns>Un dé potentiellement NULL</returns>
        Profile? GetProfileById(Guid id);

        /// <summary>
        /// Supprime tous les dés.
        /// </summary>
        /// <returns>True si correctement supprimés.</returns>
        bool RemoveAllProfiles();

        /// <summary>
        /// Ajoute un dé en base.
        /// </summary>
        /// <param name="dice"> Dé à enregistrer. </param>
        /// <returns> Vrai si l'instertion a pu s'effectuer, faux autrement. </returns>
        bool AddProfile(Profile profile);
        
        bool RemoveProfileById(Guid id);

        bool UpdateProfile(Profile profile);

    }
}
