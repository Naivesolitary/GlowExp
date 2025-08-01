using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
/* issues related  while json deserialization to be implemented and solve later
 */
namespace GlowExp.Components.Data
{
    public class TransactionService
    {
        private readonly List<TransactionBase> _transactions = new();
        private readonly string _appDataDirectory;
        private readonly string _transactionsFilePath;

        public TransactionService()
        {
            _appDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"GlowExp");
            _transactionsFilePath = Path.Combine(_appDataDirectory,"transaction.json");

            if (!Directory.Exists(_appDataDirectory)) { Directory.CreateDirectory(_appDataDirectory); }

            LoadTransactions();

        }


        public decimal TotalIncomes { get; private set; } = 0;  // Default total income
        public decimal TotalExpenses { get; private set; } = 0;  // Default total expenses
        public decimal TotalDebts { get; private set; } = 0;  // Default total expenses
        public decimal ClearedDebts { get; private set; } = 0;

        

        public int TotalTransactions => _transactions.Count; // Total number of transactions
        public decimal TotalTransactionsValue => TotalIncomes + TotalDebts - TotalExpenses;

        

        public decimal HighestInflow => _transactions.OfType<Income>().Select(i => i.Amount).DefaultIfEmpty(0).Max(); // Max income
        public decimal HighestOutflow => _transactions.OfType<Expense>().Select(e => e.Amount).DefaultIfEmpty(0).Max(); // Max expense
        public decimal HighestDebt => _transactions.OfType<Debt>().Select(d => d.Amount).DefaultIfEmpty(0).Max(); // Max debt



        public List<TransactionBase> Top5HighestTransactions =>
            _transactions.OrderByDescending(t => t.Amount).Take(5).ToList();


        public List<TransactionBase> Top5LowestTransactions =>
            _transactions.OrderBy(t => t.Amount).Take(5).ToList();
        public List<Debt> GetPendingDebts() => _transactions.OfType<Debt>().Where(d => !d.IsCleared).ToList();

        public void AddTransaction(TransactionBase transaction)
        {
            _transactions.Add(transaction);
            UpdateTotals(transaction);
            SaveTransactions();

            //SaveTransactions();
            // Update total income or expense based on the transaction type




        }


        public decimal getCurrentBalance()  /// GET THE CURRRENT BALANCE
        {
            var totalInflow = _transactions.OfType<Income>().Sum(i => i.Amount);
            var totalExpense = _transactions.OfType<Expense>().Sum(e => e.Amount);
            // Get already cleared debts
            var totalClearedDebts = _transactions.OfType<Debt>().Where(d => d.IsCleared).Sum(d => d.Amount);

            // Calculate available balance
            var currentBalance = totalInflow - totalExpense - totalClearedDebts;
            return currentBalance > 0 ? currentBalance : 0;

        }

        public bool ClearDebtAndSave(Debt debt)
        {
           

            // Calculate available balance
            var currentBalance = getCurrentBalance();


            // Checking if current debt can be cleared
            if (currentBalance >= debt.Amount) { 
                debt.IsCleared = true;
                SaveTransactions();
                return true;
            }

            return false;
            
        }



      

        public decimal getTotalClearedDebts()
        {

            return _transactions.OfType<Debt>().Where(d => d.IsCleared).Sum(d => d.Amount);

        }

        private void UpdateTotals(TransactionBase transaction)
        {

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


        /* Serializing and saving objects into Json*/

        private void SaveTransactions()
        {

            try
            {
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    Formatting = Formatting.Indented,
                };

                string json = JsonConvert.SerializeObject(_transactions, settings);
                File.WriteAllText(_transactionsFilePath, json);
            }
            catch (Exception ex) { Console.WriteLine($"Error saving transactions: {ex.Message}"); }

        }



        /* Loading the Transaction Json*/

        private void LoadTransactions()
        {
            if (File.Exists(_transactionsFilePath))
            {
                try { 
                string json = File.ReadAllText(_transactionsFilePath);
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                };

                var transactions = JsonConvert.DeserializeObject<List<TransactionBase>>(json,settings) ?? new List<TransactionBase>();
                _transactions.Clear();
                _transactions.AddRange(transactions);

                RecalculateTotals();
                }catch (Exception ex){
                    Console.WriteLine($"Error loading transactions: {ex.Message}");
                }




            }
            
            
            
            }


        private void RecalculateTotals()
        {
            TotalIncomes = _transactions.OfType<Income>().Sum(i => i.Amount);
            TotalExpenses = _transactions.OfType<Expense>().Sum(e => e.Amount);
            TotalDebts = _transactions.OfType<Debt>().Sum(d => d.Amount);
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
            SaveTransactions();

        }


        public void DeleteAllTransactions()
        {
            _transactions.Clear();
            RecalculateTotals();
            SaveTransactions();
        }


    }

}
