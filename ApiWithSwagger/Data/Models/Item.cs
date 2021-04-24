using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithSwagger.Data.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public short? ItemType { get; set; }
        public DateTime Received { get; set; }
        public DateTime? Take { get; set; }
        public decimal? DailyPrice { get; set; }
    }
}
