using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model
{
    /// <summary>
    /// Classe définissant ce qu'un dé doit contenir à minima.
    /// </summary>
    public abstract class Dice
    {
        #region attributs
        /// <summary>
        /// Attributs privé non modifiable contenant le nombre de faces du dé.
        /// </summary>
        private readonly int _nbFaces;

        /// <summary>
        /// Accesseur de l'attribut _nbFaces.
        /// </summary>
        public int NbFaces { get => _nbFaces;}
        #endregion

        #region constructeur

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="nbFaces"> Nombre de faces du dé. </param>
        protected Dice(int nbFaces)
        {
            _nbFaces = nbFaces;
        }
        #endregion
    }
}
