﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Viex.MyExpenses.Persistence.Repositores
{
    public class BaseEntity
    {
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}