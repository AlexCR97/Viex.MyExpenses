using System;
using System.Collections.Generic;
using System.Text;
using Viex.MyExpenses.Persistence.Entities;

namespace Viex.MyExpenses.Domain.Models
{
    public class TransactionEntryModel : BaseModel
    {
        public long TransactionEntryId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

        public long? CategoryId { get; set; }
        public CategoryDescriptorModel Category { get; set; }

        public long TypeId { get; set; }
        public TransactionTypeDescriptorModel Type { get; set; }

        public long UserId { get; set; }
        public UserModel User { get; set; }
    }

    public static class TransactionEntryModelMapper
    {
        public static TransactionEntry AsEntity(this TransactionEntryModel model)
        {
            return new TransactionEntry
            {
                Amount = model.Amount,
                Category = model.Category?.AsEntity(),
                CategoryId = model.CategoryId,
                DateCreated = model.DateCreated,
                DateUpdated = model.DateUpdated,
                Description = model.Description,
                TransactionEntryId = model.TransactionEntryId,
                Type = model.Type?.AsEntity(),
                TypeId = model.TypeId,
                User = model.User?.AsEntity(),
                UserId = model.UserId,
            };
        }

        public static TransactionEntryModel AsModel(this TransactionEntry entity)
        {
            return new TransactionEntryModel
            {
                Amount = entity.Amount,
                Category = entity.Category?.AsModel(),
                CategoryId = entity.CategoryId,
                DateCreated = entity.DateCreated,
                DateUpdated = entity.DateUpdated,
                Description = entity.Description,
                TransactionEntryId = entity.TransactionEntryId,
                Type = entity.Type?.AsModel(),
                TypeId = entity.TypeId,
                User = entity.User?.AsModel(),
                UserId = entity.UserId,
            };
        }
    }
}
