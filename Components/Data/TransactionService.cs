using System.Collections.Generic;
using System.Linq;
/* issues related  while json deserialization to be implemented and solve later
 */
namespace GlowExp.Components.Data
{
    public class TransactionService
    {
        private readonly List<TransactionBase> _transactions = new();
        //private readonly TransactionStorage _storage;

       

/*
        public TransactionService()
        {
            _storage = new TransactionStorage();
            _transactions = _storage.LoadTransactions();

        }*/

        public void AddTransaction(TransactionBase transaction)
        {
            _transactions.Add(transaction);
            //SaveTransactions();
            
        }



        /*private void SaveTransactions()
        {
            _storage.SaveTransactions(_transactions);
        }*/

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
