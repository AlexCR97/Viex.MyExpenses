﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Viex.MyExpenses.Domain.Models
{
    public class BaseModel
    {
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}