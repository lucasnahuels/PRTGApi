using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using System;
using System.Threading.Tasks;
using WebApi.Amazon.DynamoDb.Interfaces;
using System.Collections.Generic;

namespace WebApi.Amazon.DynamoDb.Context
{
    public class DynamoDbContext<T> : DynamoDBContext, IDynamoDbContext<T> where T : class
    {
        private readonly DynamoDBOperationConfig _config;

        public DynamoDbContext(IAmazonDynamoDB client, DynamoDbOptions dynamoDbOptions) : base(client)
        {
            _config = new DynamoDBOperationConfig()
            {
                TableNamePrefix = dynamoDbOptions.EnvPrefix
            };
        }

        public async Task<IEnumerable<T>> Get(IEnumerable<ScanCondition> scanConditions = null)
        {
            if(scanConditions == null)
            {
                scanConditions = new List<ScanCondition>();
            }
            return await ScanAsync<T>(scanConditions, _config).GetRemainingAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await LoadAsync<T>(id, _config);
        }

        public async Task<T> UpdateAsync(string id, T entity)
        {
            var loadEntity = await LoadAsync<T>(id, _config);
            if(loadEntity != null)
            {
                await SaveAsync(entity, _config);
                return await LoadAsync<T>(id, _config);
            }

            throw new Exception("No existe la entidad a modificar");
        }

        public async Task SaveAsync(T item)
        {
            await SaveAsync(item, _config);
        }

        public async Task DeleteAsync(T item)
        {
            await DeleteAsync(item, _config);
        }
    }
}
