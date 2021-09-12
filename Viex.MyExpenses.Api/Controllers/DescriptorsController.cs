using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Viex.MyExpenses.Domain.Services;
using Viex.MyExpenses.Domain.Services.Descriptors;

namespace Viex.MyExpenses.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DescriptorsController : ControllerBase
    {
        private readonly IDescriptorService _descriptorService;

        public DescriptorsController(IDescriptorService descriptorService)
        {
            _descriptorService = descriptorService;
        }

        [HttpPost(DescriptorTypes.Categories)]
        public async Task CreateCategories([FromBody] IEnumerable<string> categories) =>
            await _descriptorService.CreateCategories(categories);

        [HttpPost(DescriptorTypes.TransactionTypes)]
        public async Task CreateTransactionTypes([FromBody] IEnumerable<string> types) =>
            await _descriptorService.CreateTransactionTypes(types);

        [HttpGet(DescriptorTypes.Categories)]
        public async Task<IList<string>> GetCategories() =>
            await _descriptorService.GetCategories();

        [HttpGet(DescriptorTypes.TransactionTypes)]
        public async Task<IList<string>> GetTransactionTypes() =>
            await _descriptorService.GetTransactionTypes();
    }
}
