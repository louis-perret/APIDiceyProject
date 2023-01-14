using Api.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Repositories.DiceRepository
{
    /// <summary>
    /// Implémentation simple de AbstractDiceRepository.
    /// </summary>
    public class SimpleDiceRepository : AbstractDiceRepository
    {
        public SimpleDiceRepository(ApiDbContext context) : base(context)
        {
        }
    }
}
