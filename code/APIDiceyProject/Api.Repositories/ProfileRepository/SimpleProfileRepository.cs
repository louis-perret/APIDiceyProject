using Api.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Repositories.ProfileRepository
{
    public class SimpleProfileRepository : AbstractProfileRepository
    {
        public SimpleProfileRepository(ApiDbContext context) : base(context)
        {

        }
    }
}
