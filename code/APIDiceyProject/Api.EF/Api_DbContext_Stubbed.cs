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
    public class Api_DbContext_Stubbed : Api_DbContext
    {
        #region constructeurs
        public Api_DbContext_Stubbed()
        {
        }

        public Api_DbContext_Stubbed(DbContextOptions<Api_DbContext> options) : base(options)
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
        }
        #endregion
    }
}
