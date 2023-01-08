using Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.EF
{
    /// <summary>
    /// DbContext de l'API.
    /// </summary>
    public class Api_DbContext : DbContext
    {
        #region attributs
        /// <summary>
        /// Dés stockés en base.
        /// </summary>
        public DbSet<Dice> dices { get; set; }
        #endregion

        #region constructeurs

        /// <summary>
        /// Constructeur vide.
        /// </summary>
        public Api_DbContext() { }

        /// <summary>
        /// Constructeur avec options.
        /// </summary>
        /// <param name="options"> Options du DbContext. </param>
        public Api_DbContext(DbContextOptions<Api_DbContext> options) : base(options) { }
        #endregion

        #region méthodes redéfinies
        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlite($"Data Source=Database_Test.db");
            }
        }
        #endregion

    }
}
