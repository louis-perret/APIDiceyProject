using Api.EF;
using Api.Model.Throw;
using Api.Repositories.DiceRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Repositories.ThrowRepository
{
    public class AbstractThrowRepository : BaseRepository, IThrowRepository
    {
        #region attributs
        private IDiceRepository _diceRepository;
        #endregion

        #region constructeur
        public AbstractThrowRepository(ApiDbContext context, IDiceRepository diceRepository) : base(context)
        {
            this._diceRepository = diceRepository;
        }
        #endregion

        #region méthodes redéfinies
        public async Task<Throw?> GetThrowByIdAsync(Guid id)
        {
            var throwEntity = await _context.throws.Where(t => t.Id == id).FirstOrDefaultAsync();
            if (throwEntity == null) return null;
            return new Throw(throwEntity.Result, await _diceRepository.GetDiceById(throwEntity.DiceId), throwEntity.Id);
        }

        #endregion
    }
}
