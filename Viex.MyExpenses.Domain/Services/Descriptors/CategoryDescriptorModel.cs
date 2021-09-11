using System;
using System.Collections.Generic;
using System.Text;
using Viex.MyExpenses.Persistence.Entities;

namespace Viex.MyExpenses.Domain.Models
{
    public class CategoryDescriptorModel : DescriptorModel
    {
        public long CategoryDescriptorId { get; set; }
    }

    public static class CategoryDescriptorMapper
    {
        public static CategoryDescriptor AsEntity(this CategoryDescriptorModel model)
        {
            return new CategoryDescriptor
            {
                CategoryDescriptorId = model.CategoryDescriptorId,
                DateCreated = model.DateCreated,
                DateUpdated = model.DateUpdated,
                Description = model.Description,
            };
        }

        public static CategoryDescriptorModel AsModel(this CategoryDescriptor model)
        {
            return new CategoryDescriptorModel
            {
                CategoryDescriptorId = model.CategoryDescriptorId,
                DateCreated = model.DateCreated,
                DateUpdated = model.DateUpdated,
                Description = model.Description,
            };
        }
    }
}
