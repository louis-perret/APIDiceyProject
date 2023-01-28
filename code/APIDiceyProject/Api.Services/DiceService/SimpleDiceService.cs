using Api.Repositories.DiceRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.DiceService
{
    /// <summary>
    /// Implémentation simple de AbstractDiceService.
    /// </summary>
    public class SimpleDiceService : AbstractDiceService
    {
        #region constructeurs
        public SimpleDiceService(IDiceRepository diceRepository) : base(diceRepository)
        {
        }

        public SimpleDiceService(ILogger<AbstractDiceService> logger, IDiceRepository diceRepository) : base(logger, diceRepository)
        {
        }
        #endregion
    }
}
