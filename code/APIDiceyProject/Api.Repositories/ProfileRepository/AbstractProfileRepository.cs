using Api.Model;
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
        public AbstractProfileRepository() : base()
        {

        }
        #endregion

        public Profile? AddProfile(Profile profileAdd)
        {
            try
            {
                if (_context.profiles.Where(profile=> profile.Id == profileAdd.Id).FirstOrDefault() == null)
                {
                    var prof = _context.profiles.Add(profileAdd.ToEntity());
                    _context.SaveChanges();
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

        public Profile? GetProfileById(Guid id)
        {
            return _context.profiles.Where(profile => profile.Id == id).FirstOrDefault()?.ToModel();
        }

        public List<Profile> ProfilesByPage(int numPage, int nbByPage)
        {
            return _context.profiles.Skip(nbByPage * (numPage-1))
                    .Take(nbByPage)
                    .Select(profile => profile.ToModel())
                    .ToList();
        }

        public bool RemoveAllProfiles()
        {
            try
            {
                _context.profiles.ExecuteDelete();
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

        public bool RemoveProfileById(Guid id)
        {
            try
            {
                var profile = _context.profiles.Where(profile => profile.Id== id).FirstOrDefault();
                if (profile == null)
                    return false;
                _context.profiles.Remove(profile);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }

        public bool UpdateProfile(Profile profileAdd)
        {
            try
            {
                var profile = _context.profiles.Where(profile => profile.Id == profileAdd.Id).FirstOrDefault();
                if (profile == null)
                    return false;
                profile.Name = profileAdd.Name;
                profile.Surname = profileAdd.Surname;
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }
    }
}
