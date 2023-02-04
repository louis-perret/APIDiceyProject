﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Entities
{
    /// <summary>
    /// Représentation d'un lancer de dé en base.
    /// </summary>
    public class Throw
    {
        #region attribut
        /// <summary>
        /// Id du lancer.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Résultat eu.
        /// </summary>
        [Required]
        public int Result { get; set; }

        /// <summary>
        /// Identifiant du dé lancé.
        /// </summary>
        [ForeignKey("Dice")]
        public int DiceId { get; set; }
        #endregion

        #region constructeurs

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="result">Résultat obtenu.</param>
        /// <param name="diceId">Dé avec lequel le résultat a été obtenu.</param>
        public Throw(Guid id, int result, int diceId)
        {
            Id = id;
            Result = result;
            DiceId = diceId;
        }
        #endregion
    }
}