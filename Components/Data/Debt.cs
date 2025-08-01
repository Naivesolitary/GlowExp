
namespace GlowExp.Components.Data
{
    public class Debt:TransactionBase
    {
       
        public string? Category { get; set;}
        public string? Source { get; set; }
        public DateOnly DueDate{ get; set; }

        public bool IsCleared { get; set; } = false;
        

    }
}
