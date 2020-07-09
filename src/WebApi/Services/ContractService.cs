using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using WebApi.Models;
using WebApi.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Amazon.DynamoDb.Interfaces;

namespace WebApi.Services
{
    public class ContractService : IContractService
    {
        private readonly IDynamoDbContext<Contract> _context;

        public ContractService(IDynamoDbContext<Contract> context)
        {
            _context = context;
        }

        public async Task<Contract> CreateAsync(Contract contract)
        {
            await _context.SaveAsync(contract);

            return contract;
        }

        public async Task<Contract> DeleteAsync(Contract contract)
        {
            await _context.DeleteAsync(contract);

            return contract;
        }

        public async Task<bool> Exists(int id)
        {
            var contracts = await _context.Get(new List<ScanCondition>() {
                new ScanCondition("Id", ScanOperator.Equal, id)
            });

            return contracts.Any();
        }

        public async Task<IEnumerable<Contract>> GetAsync()
        {
            return await _context.Get();
        }

        public async Task<Contract> GetAsync(int id)
        {
            var contracts = await _context.Get(new List<ScanCondition>() {
                new ScanCondition("Id", ScanOperator.Equal, id)
            });

            return contracts.FirstOrDefault(contract => contract.Id == id);
        }

        public async Task<Contract> UpdateAsync(Contract contract)
        {
            return await UpdateAsync(contract);
        }
    }
}
