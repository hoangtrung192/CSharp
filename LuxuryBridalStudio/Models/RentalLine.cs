namespace LuxuryBridalStudio
{
    public sealed class RentalLine
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get { return Quantity * UnitPrice; } }

        public RentalLine Clone() { return (RentalLine)MemberwiseClone(); }
    }
}
