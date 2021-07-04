using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viex.MyExpenses.Domain.Models;
using Viex.MyExpenses.Domain.Services;

namespace Viex.MyExpenses.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DescriptorsController : ControllerBase
    {
        private readonly IDescriptorService _service;

        public DescriptorsController(IDescriptorService service)
        {
            _service = service;
        }

        [HttpPost("categories")]
        public async Task CreateCategories([FromBody] IEnumerable<string> categories)
        {
            await _service.CreateCategories(categories);
        }

        [HttpPost("transactionTypes")]
        public async Task CreateTransactionTypes([FromBody] IEnumerable<string> types)
        {
            await _service.CreateTransactionTypes(types);
        }

        [HttpGet("categories")]
        public async Task<IEnumerable<CategoryDescriptorModel>> GetCategories()
        {
            return await _service.GetCategories();
        }

        [HttpGet("transactionTypes")]
        public async Task<IEnumerable<TransactionTypeDescriptorModel>> GetTransactionTypes()
        {
            return await _service.GetTransactionTypes();
        }
    }
}
