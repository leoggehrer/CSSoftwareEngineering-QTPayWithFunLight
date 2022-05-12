namespace QTPayWithFunLight.AspMvc.Models
{
    public class FilterModel
    {
        public bool HasValue => Year.HasValue || Month.HasValue || Day.HasValue || string.IsNullOrEmpty(CardNumber) == false;

        public int? Day { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public string? CardNumber { get; set; }
        public decimal Volume { get; set; }
        public override string ToString()
        {
            return $"Day: {(Day.HasValue ? Day : "---")} Month: {(Month.HasValue ? Month : "---")} Year: {(Year.HasValue ? Year : "---")} Card-Number: {(string.IsNullOrEmpty(CardNumber) == false ? CardNumber : "---")} Volumne: {Volume} EUR";
        }
    }
}
