using System.Collections.Generic;
using System.Linq;
namespace GlowExp.Components.Data
{
    public class TransactionService
    {
        private readonly List<TransactionBase> _transactions = new();

        public void AddTransaction(TransactionBase transaction)
        {
            _transactions.Add(transaction);
        }



        public List<TransactionBase> GetAllTransactions()
        {
            return _transactions;
        }

        public List<T> GetTransactionByType<T>() where T : TransactionBase
        {
            return _transactions.OfType<T>().ToList();
        }


        public void ClearTransactions<T>() where T : TransactionBase
        {
            _transactions.RemoveAll(t => t is T);
        }


    }

}
