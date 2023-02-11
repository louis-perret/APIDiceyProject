using Api.EF;
using Api.Model;
using Api.Model.Throw;
using Microsoft.EntityFrameworkCore;
using ModelEntityExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Repositories.ProfileRepository
{
    public abstract class AbstractProfileRepository : BaseRepository, IProfileRepository
    {
        #region constructeurs
        protected AbstractProfileRepository(ApiDbContext context) : base(context)
        {
        }
        #endregion

        #region méthodes redéfinies
        async public Task<Profile?> AddProfile(Profile profileAdd)
        {
            try
            {
                if (await _context.profiles.Where(profile=> profile.Id == profileAdd.Id).FirstOrDefaultAsync() == null)
                {
                    var prof = _context.profiles.Add(profileAdd.ToEntity());
                    await _context.SaveChangesAsync();
                    return prof.Entity.ToModel();
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<int> getNbProfiles()
        {
            return await _context.profiles.CountAsync();
        }


        /// <inheritdoc/>
        public async Task<bool> AddThrow(int result, int nbFacesDe, Guid profileId)
        {
            var t = new Entities.Throw(result, nbFacesDe, profileId);
            var profile = await _context.profiles.FindAsync(t.ProfileId);
            if (profile == null) return false;
            profile.Throws.Add(t);
            return true;
        }

        async public Task<Profile?> GetProfileById(Guid id)
        {
            return  (await _context.profiles.Where(profile => profile.Id == id).FirstOrDefaultAsync())?.ToModel();
        }

        async public Task<List<Profile>> ProfilesByPage(int numPage, int nbByPage, string subString)
        {
            return await _context.profiles.Where(profile => (profile.Name + profile.Surname).Contains(subString) || (profile.Surname + profile.Name).Contains(subString))
                    .Skip(nbByPage * (numPage-1))
                    .Take(nbByPage)
                    .Select(profile => profile.ToModel())
                    .ToListAsync();
        }

        async public Task<bool> RemoveAllProfiles()
        {
            try
            {
                await _context.profiles.ExecuteDeleteAsync();
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

        async public Task<bool> RemoveProfileById(Guid id)
        {
            try
            {
                var profile = await _context.profiles.Where(profile => profile.Id== id).FirstOrDefaultAsync();
                if (profile == null)
                    return false;
                _context.profiles.Remove(profile);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }

        async public Task<bool> UpdateProfile(Profile profileAdd)
        {
            try
            {
                var profile = await _context.profiles.Where(profile => profile.Id == profileAdd.Id).FirstOrDefaultAsync();
                if (profile == null)
                    return false;
                profile.Name = profileAdd.Name;
                profile.Surname = profileAdd.Surname;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }
    }
    #endregion
}
