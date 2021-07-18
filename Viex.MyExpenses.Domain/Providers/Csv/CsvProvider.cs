using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Viex.MyExpenses.Domain.Providers.Csv
{
    public interface ICsvProvider
    {
        Task<IEnumerable<T>> Parse<T>(string csvFileBase64);
    }

    public class CsvProvider : ICsvProvider
    {
        public async Task<IEnumerable<T>> Parse<T>(string csvFileBase64)
        {
            var fileBase64 = Base64WithoutHeader(csvFileBase64);
            var fileBytes = Convert.FromBase64String(fileBase64);
            var fileStream = new StreamContent(new MemoryStream(fileBytes));
            var fileContent = await fileStream.ReadAsStreamAsync();

            using var fileReader = new StreamReader(fileContent);
            using var csvReader = new CsvReader(fileReader, CultureInfo.InvariantCulture);

            var records = csvReader.GetRecords<T>();

            return records.ToList();
        }

        private string Base64WithoutHeader(string base64)
        {
            try
            {
                var tokens = base64.Split(',');
                return tokens[1];
            }
            catch
            {
                return base64;
            }
        }
    }
}
