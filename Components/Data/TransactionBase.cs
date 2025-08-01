

namespace GlowExp.Components.Data
{
    public abstract class TransactionBase
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public string? Tag { get; set; }
        public decimal Amount { get; set; }
        public string? getTransactionType {  get; set; }
        public string? Note { get; set; }

    }
}
