
namespace GlowExp.Components.Data
{
    public class Debt
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public string? Tag {get; set;}
        public string? Source { get; set; }
        public DateTime DueDate{ get; set; }
        public string? Note { get; set; }

    }
}
