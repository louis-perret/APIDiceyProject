using Api.Repositories.ThrowRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.ThrowService
{
    /// <summary>
    /// Implémentation simple de AbstractThrowService.
    /// </summary>
    public class SimpleThrowService : AbstractThrowService
    {
        #region constructeurs
        public SimpleThrowService(IThrowRepository throwRepository) : base(throwRepository)
        {
        }

        public SimpleThrowService(ILogger<AbstractThrowService> logger, IThrowRepository throwRepository) : base(logger, throwRepository)
        {
        }
        #endregion
    }
}
