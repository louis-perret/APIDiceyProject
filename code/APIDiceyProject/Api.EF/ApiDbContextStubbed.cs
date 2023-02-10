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

            var profile = new Profile(Guid.Parse("cc6f9111-b174-4064-814b-ce7eb4169e80"), "Perret", "Louis");
            var profile2 = new Profile(Guid.NewGuid(), "Grienenberger", "Côme");
            var profile3 = new Profile(Guid.NewGuid(), "Malvezin", "Neitah");

            builder.Entity<Profile>().HasData(
              profile,
              profile2,
              profile3
            );

            builder.Entity<Throw>().HasData(
                new Throw(Guid.Parse("aa6f9111-b174-4064-814b-ce7eb4169e80"),1, 2, profile.Id),
                new Throw(Guid.NewGuid(), 2, 2, profile.Id),
                new Throw(Guid.NewGuid(), 4, 4, profile2.Id),
                new Throw(Guid.NewGuid(), 3, 4, profile2.Id),
                new Throw(Guid.NewGuid(), 3, 3, profile3.Id),
                new Throw(Guid.NewGuid(), 5, 6, profile3.Id)
            );
        }
        #endregion
    }
}
