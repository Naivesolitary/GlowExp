using System.Collections.Generic;
using System.Linq;
/* issues related  while json deserialization to be implemented and solve later
 */
namespace GlowExp.Components.Data
{
    public class TransactionService
    {
        private readonly List<TransactionBase> _transactions = new();

        public decimal TotalIncomes { get; private set; } = 0;  // Default total income
        public decimal TotalExpenses { get; private set; } = 0;  // Default total expenses
        public decimal TotalDebts { get; private set; } = 0;  // Default total expenses

        public int TotalTransactions => _transactions.Count; // Total number of transactions
        public decimal TotalTransactionsValue => TotalIncomes + TotalDebts - TotalExpenses;

        public decimal HighestInflow => _transactions.OfType<Income>().Max(i => i.Amount); // Max income
        public decimal HighestOutflow => _transactions.OfType<Expense>().Max(e => e.Amount); // Max expense
        public decimal HighestDebt => _transactions.OfType<Debt>().Max(d => d.Amount); // Max debt


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
            // Update total income or expense based on the transaction type
            if (transaction is Income income)
            {
                TotalIncomes += income.Amount;
            }
            else if (transaction is Expense expense)
            {
                TotalExpenses += expense.Amount;
            }
            else if (transaction is Debt debt)
            {
                TotalDebts += debt.Amount;
                    }

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
