using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.DTOs
{
    /// <summary>
    /// DTO d'un dé.
    /// </summary>
    public class Dice : IEquatable<Dice?>
    {
        /// <summary>
        /// Nombre de faces du dé.
        /// </summary>
        [Required]
        public int NbFaces { get; set; }

        /// <summary>
        /// Constructeur complet.
        /// </summary>
        /// <param name="nbFaces"> Nombre de faces du dé. </param>
        public Dice(int nbFaces)
        {
            NbFaces = nbFaces;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Dice);
        }

        public bool Equals(Dice? other)
        {
            return other is not null &&
                   NbFaces == other.NbFaces;
        }
    }
}
