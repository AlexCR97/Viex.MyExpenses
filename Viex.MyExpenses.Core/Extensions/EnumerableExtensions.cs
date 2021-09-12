using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viex.MyExpenses.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static async Task<IEnumerable<TResult>> SelectAsync<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, Task<TResult>> task)
        {
            var mapped = new List<TResult>();

            foreach (var item in source)
            {
                var taskResult = await task(item);
                mapped.Add(taskResult);
            }

            return mapped;
        }

        public static IList<TSource> AwaitList<TSource>(this Task<IEnumerable<TSource>> source)
        {
            var result = source.Result;
            return result.ToList();
        }
    }
}
