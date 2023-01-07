using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model
{
    /// <summary>
    /// Implémentation simple de Dice.
    /// </summary>
    public class SimpleDice : Dice
    {
        public SimpleDice(int nbFaces) : base(nbFaces) { }
    }
}
