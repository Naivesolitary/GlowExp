

namespace GlowExp.Components.Data
{
    public class Expense
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public string? Category { get; set; }
        public string? Account { get; set; }
        public string? Note { get; set; }

    
}
}
