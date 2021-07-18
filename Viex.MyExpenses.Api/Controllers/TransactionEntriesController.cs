using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Viex.MyExpenses.Domain.Models;
using Viex.MyExpenses.Domain.Services.TransactionEntries;

namespace Viex.MyExpenses.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionEntriesController : ControllerBase
    {
        private readonly ITransactionEntryService _service;

        public TransactionEntriesController(ITransactionEntryService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<TransactionEntryModel> Create([FromBody] TransactionEntryModel transaction)
        {
            return await _service.Create(transaction);
        }

        [HttpDelete("all")]
        public async Task DeleteAll()
        {
            await _service.DeleteAll();
        }

        [HttpGet]
        public async Task<IEnumerable<TransactionEntryModel>> GetAll()
        {
            return await _service.GetAll();
        }

        [HttpPost("weekly")]
        public async Task<WeeklyTransactions> GetWeeklyTransactions([FromBody] WeeklyTransactionsSearchParams searchParams)
        {
            return await _service.GetWeeklyTransactions(searchParams);
        }

        [HttpPost("import")]
        public async Task ImportFromCsv([FromBody] ImportTransactionsFromCsvModel model)
        {
            await _service.ImportFromCsv(model.CsvFileBase64);
        }
    }
}
