using System;
using System.Linq;
using System.Threading.Tasks;
using capgemini_api.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace capgemini_api.Infra.Services
{
    public class BdTransactionService
    {
        private int TransactionsRunning = 0;

        private IDbContextTransaction Transaction;

        private readonly ApiContext _context;

        public BdTransactionService(ApiContext context)
        {
            _context = context;
        }

        public IDbContextTransaction GetDbContextTransaction()
        {
            return Transaction;
        }

        public async Task BeginTransactionAsync()
        {
            if (TransactionsRunning > 0)
            {
                TransactionsRunning++;
                return;
            }

            TransactionsRunning++;
            Transaction = await _context.Database.BeginTransactionAsync();
        }

        public void CommitTransaction()
        {
            TransactionsRunning--;
            if (TransactionsRunning == 0)
            {
                _context.Database.CommitTransaction();
                Transaction = null;
            }
        }

        public void RollbackTransaction()
        {
            TransactionsRunning--;
            if (TransactionsRunning == 0)
            {
                _context.Database.RollbackTransaction();
                Transaction = null;
                try
                {
                    var entries = _context.ChangeTracker.Entries().ToList();
                    foreach (var entry in entries)
                    {
                        if (entry.Entity != null)
                        {
                            entry.State = EntityState.Detached;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw ex;
                }
            }
        }
    }
}
