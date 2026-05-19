using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Models
{
    public class BlBranches
    {
        public int Id { get; set; }

        public string Description { get; set; } = null!;

        //public List<CustomersTbl> CustomersTbls { get; set; } = new List<CustomersTbl>();

        public List<BlPositions> PositionsTbls { get; set; } = new List<BlPositions>();
    }
}

