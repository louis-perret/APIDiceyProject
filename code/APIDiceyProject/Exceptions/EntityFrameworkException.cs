﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    public class EntityFrameworkException : Exception
    {
        public EntityFrameworkException(string? message) : base(message)
        {
        }
    }
}
