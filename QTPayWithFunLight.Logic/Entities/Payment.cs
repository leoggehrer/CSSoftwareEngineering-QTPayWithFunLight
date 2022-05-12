namespace QTPayWithFunLight.Logic.Entities
{
    [Table("Payments", Schema = "App")]
    public class Payment : VersionEntity
    {
        public DateTime Date { get; set; }
        [Required]
        [MaxLength(16)]
        public string CreditCardNumber { get; set; } = string.Empty;
        [Precision(18,2)]
        public decimal Amount { get; set; }
        [MaxLength(1024)]
        public string? Note { get; set; }
    }
}
