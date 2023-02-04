using Api.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Repositories
{
    /// <summary>
    /// Repository de base. Contient une référence vers le DbContext de l'API.
    /// </summary>
    public abstract class BaseRepository
    {

        /// <summary>
        /// DbContext de l'API.
        /// </summary>
        protected ApiDbContext _context;

        /// <summary>
        /// Constructeur instanciant l'Api_DbContext voulu.
        /// </summary>
        protected BaseRepository(ApiDbContext context)
        {
            _context = context;
        }
    }
}
