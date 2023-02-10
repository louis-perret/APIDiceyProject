using System;
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
        /// Identifiant du dé lancer.
        /// </summary>
        [ForeignKey("Dice")]
        public int DiceId { get; set; }

        /// <summary>
        /// Identifiant du profil ayant créé le lancer.
        /// </summary>
        [Required]
        public Guid ProfileId { get; set; }

        /// <summary>
        /// Profile ayant créé le lancer.
        /// </summary>
        [ForeignKey("ProfileId")]
        public Profile Profile { get; set; }
        #endregion

        #region constructeurs

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="result">Résultat obtenu.</param>
        /// <param name="diceId">Dé avec lequel le résultat a été obtenu.</param>
        /// <param name="profileId">Identifiant du profil ayant créé le lancer.</param>
        public Throw(Guid id, int result, int diceId, Guid profileId)
        {
            Id = id;
            Result = result;
            DiceId = diceId;
            ProfileId = profileId;
        }
        #endregion
    }
}
