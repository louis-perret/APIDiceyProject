using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Entities
{
    /// <summary>
    /// Représentation d'un dé en base.
    /// </summary>
    public class Dice
    {
        #region attribut
        /// <summary>
        /// Le nombre de faces est la clé de l'entité.
        /// Deux dé en base ne peuvent donc pas avoir le même nombre de faces.
        /// </summary>
        [Key]
        public int NbFaces { get; set; }
        #endregion

        #region constructeurs
        /// <summary>
        /// Constructeur vide pour EntityFramework Core.
        /// </summary>
        public Dice() { }

        /// <summary>
        /// Constructeur complet.
        /// </summary>
        /// <param name="nbFaces"> Nombre de faces d'un dé et clé primaire. </param>
        public Dice(int nbFaces)
        {
            NbFaces = nbFaces;
        }
        #endregion
    }
}
