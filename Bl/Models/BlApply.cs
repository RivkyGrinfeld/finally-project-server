using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Models
{
    public class BlApply
    {
        public int Id { get; set; }

        public string CustId { get; set; } = null!;

        public int PostId { get; set; }

        public bool Confirmed { get; set; }
        public DateTime Date { get; set; }

    }
}
