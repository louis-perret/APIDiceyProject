﻿using Api.Model.Throw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.ThrowService
{
    public interface IThrowService
    {
        public Task<Throw?> GetThrowById(Guid id);
    }
}
