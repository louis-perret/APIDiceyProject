using Api.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.EF
{
    /// <summary>
    /// Implémentation de Api_DbContext, ajoutant des données à sa création.
    /// </summary>
    public class ApiDbContextStubbed : ApiDbContext
    {
        #region constructeurs
        public ApiDbContextStubbed()
        {
        }

        public ApiDbContextStubbed(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }
        #endregion

        #region Méthodes redéfinies
        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Dice>().HasData(
                new Dice(2),
                new Dice(3),
                new Dice(4),
                new Dice(5),
                new Dice(6)
            );

            builder.Entity<Throw>().HasData(
                new Throw(1, 2),
                new Throw(2, 2),
                new Throw(4, 4),
                new Throw(3, 4),
                new Throw(3, 3),
                new Throw(5, 6)
            );
        }
        #endregion
    }
}
