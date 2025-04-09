﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public abstract override string ToString();
    }
}
