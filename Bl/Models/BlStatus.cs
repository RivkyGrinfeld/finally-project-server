using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Models
{
    public class BlStatus
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public List<BlCustomer> Customers { get; set; } = new List<BlCustomer>();
    }
}
