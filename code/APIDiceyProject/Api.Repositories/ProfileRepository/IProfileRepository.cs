using Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Repositories.ProfileRepository
{
    public interface IProfileRepository
    {
        /// <summary>
        /// Retourne un certain nombre de profils stockés en base de manière paginée
        /// </summary>
        /// <param name="numPage">Page de laquelle on veut les profils</param>
        /// <param name="nbByPage">Nombre de profils par page</param>
        /// <param name="subString">la substring à trouver dans la chaîne nom+prénom</param>
        /// <returns>la liste des profils correspondant aux critères</returns>
        Task<List<Profile>> ProfilesByPage(int numPage, int nbByPage,string subString);

        /// <summary>
        /// Méthode qui retourne un profil via son ID
        /// </summary>
        /// <param name="id">ID du profil à retourner</param>
        /// <returns>Le profil qui a l'id correspondant</returns>
        Task<Profile?> GetProfileById(Guid id);
        /// <summary>
        /// Méthode qui supprime tous les profils en base
        /// </summary>
        /// <returns>true si tout s'est bien passé, false sinon</returns>
        Task<bool> RemoveAllProfiles();

        /// <summary>
        /// Méthode qui supprime un profil via son Id
        /// </summary>
        /// <param name="id">l'id du profil à supprimer</param>
        /// <returns>true si tout s'est bien passé, false sinon</returns>
        Task<bool> RemoveProfileById(Guid id);

        /// <summary>
        /// Méthode qui retourne le nombre de profils dans la base
        /// </summary>
        /// <returns>le nombre de profils dans la base</returns>
        Task<int> getNbProfiles();

        /// <summary>
        /// Méthode qui ajoute un profil en base
        /// </summary>
        /// <param name="profile">Le profil à ajouter</param>
        /// <returns>true si le profil a pu être ajouté, false sinon</returns>
        Task<Profile?> AddProfile(Profile profile);

        /// <summary>
        /// Méthode qui change un profil en base
        /// </summary>
        /// <param name="profile">Le profil à modifier</param>
        /// <returns>booleen à true si le profil a pu être modifié, faux sinon</returns>
        Task<bool> UpdateProfile(Profile profile);
    }
}       
