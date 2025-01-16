
namespace GlowExp.Components.Data
{
    public class Debt:TransactionBase
    {
       
        public string? Tag {get; set;}
        public string? Source { get; set; }
        public DateTime DueDate{ get; set; }
        

    }
}
