using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithSwagger.Data.Models
{
    public class Deposit
    {
        public int Id { get; set; }
        public short ItemType { get; set; }
        public short DepositType { get; set; }
        public decimal DailyDepositPrice { get; set; }
    }
}
