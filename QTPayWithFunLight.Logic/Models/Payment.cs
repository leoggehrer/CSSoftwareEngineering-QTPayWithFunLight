namespace QTPayWithFunLight.Logic.Models
{
    public class Payment : IdentityModel
    {
        internal Entities.Payment Source { get; set; } = new();
        public override int Id 
        {
            get => Source.Id; 
            set => Source.Id = value; 
        }
        public DateTime Date 
        {
            get => Source.Date; 
            set => Source.Date = value; 
        }
        public string CreditCardNumber 
        {
            get => Source.CreditCardNumber; 
            set => Source.CreditCardNumber = value; 
        }
        public decimal Amount 
        {
            get => Source.Amount;
            set => Source.Amount = value;
        }
        public string? Note 
        {
            get => Source.Note;
            set => Source.Note = value;
        }
    }
}
