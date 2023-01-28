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
        /// Récupère nbByPage Profile de la page numPage
        /// </summary>
        /// <param name="numPage">Numéro de la page à récupérer</param>
        /// <param name="nbByPage">Nombre de profils de la page à récupérer</param>
        /// <returns>Liste des Profile récupérés</returns>
        List<Profile> GetProfilesByPage(int numPage, int nbByPage);

        /// <summary>
        /// Récupère un Profile avec son Id.
        /// </summary>
        /// <param name="id">Id du Profile</param>
        /// <returns>Un Profile potentiellement NULL</returns>
        Profile? GetProfileById(Guid id);

        /// <summary>
        /// Supprime tous les Profile.
        /// </summary>
        /// <returns>True si correctement supprimés.</returns>
        bool RemoveAllProfiles();

        /// <summary>
        /// Ajoute un Profile en base.
        /// </summary>
        /// <param name="profile"> Profile à enregistrer. </param>
        /// <returns> Le Profile créé, potentiellement null </returns>
        Profile? AddProfile(Profile profile);
        
        /// <summary>
        /// Supprime un Profile via son Id
        /// </summary>
        /// <param name="id">l'Id du Profile à supprimer</param>
        /// <returns>true si le Profile a pu être supprimé, false sinon</returns>
        bool RemoveProfileById(Guid id);

        /// <summary>
        /// Met à jour un Profile
        /// </summary>
        /// <param name="profile">le Profile avec les informations mises à jour</param>
        /// <returns>true si le Profile a pu être updaté, false sinon</returns>
        bool UpdateProfile(Profile profile);

    }
}
