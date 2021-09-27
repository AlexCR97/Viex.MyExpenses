using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Viex.MyExpenses.Domain.Services;
using Viex.MyExpenses.Domain.Services.Descriptors;
using Viex.MyExpenses.Domain.Services.TransactionCategoryDescriptors;

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

        [HttpGet("types")]
        public async Task<IList<string>> GetDescriptorTypes() =>
            await _descriptorService.GetDescriptorTypes();

        [HttpPost(DescriptorTypes.TransactionCategories)]
        public async Task CreateCategories([FromBody] IEnumerable<TransactionCategoryDescriptorModel> descriptors) =>
            await _descriptorService.CreateTransactionCategories(descriptors);

        [HttpPost(DescriptorTypes.TransactionTypes)]
        public async Task CreateTransactionTypes([FromBody] IEnumerable<string> descriptors) =>
            await _descriptorService.CreateTransactionTypes(descriptors);

        [HttpPost(DescriptorTypes.Roles)]
        public async Task CreateRoles([FromBody] IEnumerable<string> descriptors) =>
            await _descriptorService.CreateRoles(descriptors);

        [HttpGet(DescriptorTypes.Roles)]
        public async Task<IList<string>> GetRoles() =>
            await _descriptorService.GetRoles();

        [HttpGet(DescriptorTypes.TransactionCategories)]
        public async Task<IList<string>> GetTransactionCategories() =>
            await _descriptorService.GetTransactionCategories();

        [HttpGet(DescriptorTypes.TransactionTypes)]
        public async Task<IList<string>> GetTransactionTypes() =>
            await _descriptorService.GetTransactionTypes();
    }
}
