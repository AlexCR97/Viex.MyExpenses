﻿using System;
using System.Collections.Generic;
using System.Text;
using Viex.MyExpenses.Domain.Models;
using Viex.MyExpenses.Persistence.Entities;

namespace Viex.MyExpenses.Domain.Services.TransactionEntries
{
    public class WeeklyTransactions
    {
        public WeeklyTransactionsEntry Sunday { get; set; }
        public WeeklyTransactionsEntry Monday { get; set; }
        public WeeklyTransactionsEntry Tuesday { get; set; }
        public WeeklyTransactionsEntry Wednesday { get; set; }
        public WeeklyTransactionsEntry Thursday { get; set; }
        public WeeklyTransactionsEntry Friday { get; set; }
        public WeeklyTransactionsEntry Saturday { get; set; }
    }
}