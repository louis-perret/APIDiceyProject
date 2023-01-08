using Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services
{
    /// <summary>
    /// Contrat définissant les méthodes que doit implémenter un DiceService.
    /// </summary>
    public interface IDiceService
    {

        /// <summary>
        /// Récupère la liste complète des dés en base.
        /// </summary>
        /// <returns> La liste complète des dés en base. </returns>
        List<Dice> GetDices();
    }
}
