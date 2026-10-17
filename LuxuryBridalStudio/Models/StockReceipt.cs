using System;
using System.Collections.Generic;

namespace LuxuryBridalStudio
{
    public sealed class StockReceipt
    {
        public string Code { get; set; }
        public string SupplierCode { get; set; }
        public string Employee { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public List<RentalLine> Lines { get; set; }
        public decimal Total { get; set; }
    }
}
