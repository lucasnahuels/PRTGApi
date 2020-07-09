using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Amazon.DynamoDb.Interfaces
{
    public interface IDynamoDbContext<T> : IDisposable where T : class
    {
        Task<T> GetByIdAsync(string id);
        Task SaveAsync(T item);
        Task DeleteAsync(T item);
        Task<IEnumerable<T>> Get(IEnumerable<ScanCondition> scanConditions = null);
    }
}
