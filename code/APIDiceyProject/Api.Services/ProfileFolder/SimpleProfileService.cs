using Api.Repositories.ProfileRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.ProfileFolder
{
    public class SimpleProfileService : AbstractProfileService
    {
        public SimpleProfileService(ILogger<AbstractProfileService> logger, IProfileRepository diceRepository) : base(logger, diceRepository)
        {
        }
    }
}
