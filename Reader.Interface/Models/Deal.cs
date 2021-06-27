using System;
using System.Collections.Generic;
namespace Reader.Interface.Models
{
    public class Deal
    {
        public int DealNumber  { get; set; }

        public string  CustomerName  { get; set; }

        public string DealershipName  { get; set; }

        public string Vehicle { get; set; }

        public Decimal? Price { get; set; }

        public DateTime? Date { get; set; }

    }

    
}
