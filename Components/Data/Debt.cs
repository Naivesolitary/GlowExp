
namespace GlowExp.Components.Data
{
    public class Debt:TransactionBase
    {
       
        public string? Category { get; set;}
        public string? Source { get; set; }
        public DateTime DueDate{ get; set; }
        

    }
}
