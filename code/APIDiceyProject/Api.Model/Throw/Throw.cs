using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.Throw
{
    public class Throw
    {
        #region attribut
        /// <summary>
        /// Id du lancer.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Résultat eu.
        /// </summary>
        public int Result { get; private set; }

        /// <summary>
        /// Dé lancé.
        /// </summary>
        public Dice Dice { get; private set; }
        #endregion

        #region constructeurs

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="result">Résultat obtenu.</param>
        /// <param name="dice">Dé avec lequel le résultat a été obtenu.</param>
        public Throw(int result, Dice dice, Guid id)
        {
            Id = id;
            Result = result;
            Dice = dice;
        }
        #endregion
    }
}
