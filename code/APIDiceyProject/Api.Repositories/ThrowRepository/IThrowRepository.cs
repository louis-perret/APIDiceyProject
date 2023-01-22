using Api.Model.Throw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Repositories.ThrowRepository
{
    public interface IThrowRepository
    {
        public Task<Throw?> GetThrowByIdAsync(Guid id);
    }
}
