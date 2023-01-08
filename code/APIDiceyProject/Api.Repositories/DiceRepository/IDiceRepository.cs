using Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Repositories.DiceRepository
{
    /// <summary>
    /// Contrat pour les DiceRepository.
    /// </summary>
    public interface IDiceRepository
    {
        /// <summary>
        /// Retourne l'intégralité des dés stockés en base.
        /// </summary>
        /// <returns> Les dés stockés en base. </returns>
        List<Dice> GetDices();
    }
}
