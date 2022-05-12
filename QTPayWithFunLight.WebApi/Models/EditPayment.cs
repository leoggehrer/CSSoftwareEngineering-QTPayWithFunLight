namespace QTPayWithFunLight.WebApi.Models
{
    public class EditPayment
    {
        public DateTime Date { get; set; }
        public string CreditCardNumber { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string? Note { get; set; }
    }
}
