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
    public class ApiDbContext : DbContext
    {
        #region attributs
        /// <summary>
        /// Dés stockés en base.
        /// </summary>
        public DbSet<Dice> dices { get; set; }

        /// <summary>
        /// Lancers de dés stockés en base.
        /// </summary>
        public DbSet<Throw> throws { get; set; }

        public DbSet<Profile> profiles { get; set; }

        #endregion

        #region constructeurs
        /// <summary>
        /// Constructeur vide.
        /// </summary>
        public ApiDbContext() { }

        /// <summary>
        /// Constructeur avec options.
        /// </summary>
        /// <param name="options"> Options du DbContext. </param>
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }
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
