namespace QTPayWithFunLight.AspMvc.Models
{
    public class Payment : IdentityModel
    {
        public DateTime Date { get; set; }
        [Required]
        [MaxLength(16)]
        public string CreditCardNumber { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        [MaxLength(1024)]
        public string? Note { get; set; }
    }
}
