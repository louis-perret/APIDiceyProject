﻿using Api.Model.Throw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.ThrowService
{
    /// <summary>
    /// Contrat définissant les méthodes que doit implémenter un ThrowService.
    /// </summary>
    public interface IThrowService
    {
        /// <summary>
        /// Récupère un lancer par son id.
        /// </summary>
        /// <param name="id">Id du lancer à récupérer.</param>
        /// <returns>Le lancer voulu</returns>
        public Task<Throw?> GetThrowById(Guid id);

        /// <summary>
        /// Récupère les lancers d'un joueur avec un système de pagination.
        /// </summary>
        /// <param name="idProfile">Id du profil ayant créé le lancer.</param>
        /// <param name="numPage">Numéro de page voulu.</param>
        /// <param name="nbByPage">Nombre d'éléments à retourner.</param>
        /// <returns>Une liste de lancers.</returns>
        public Task<List<Throw>> GetThrowByProfileId(Guid idProfile, int numPage, int nbByPage);

        /// <summary>
        /// Ajoute un lancer.
        /// </summary>
        /// <param name="result">Résultat du lancer.</param>
        /// <param name="nbFacesDe">Dé lancé.</param>
        /// <param name="profileId">Joueur ayant lancé le dé.</param>
        /// <returns>L'id du lancer ajouté.</returns>
        public Task<Guid> AddThrow(int result, int nbFacesDe, Guid profileId);
    }
}
