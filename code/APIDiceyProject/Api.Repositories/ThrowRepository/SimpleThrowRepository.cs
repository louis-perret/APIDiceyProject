using Api.EF;
using Api.Repositories.DiceRepository;
using Api.Repositories.ProfileRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Repositories.ThrowRepository
{
    /// <summary>
    /// Implémentation simple de AbstractThrowRepository.
    /// </summary>
    public class SimpleThrowRepository : AbstractThrowRepository
    {
        /// <inheritdoc/>
        public SimpleThrowRepository(ApiDbContext context, IDiceRepository diceRepository, IProfileRepository profileRepository) : base(context,  diceRepository, profileRepository)
        {
        }
    }
}
