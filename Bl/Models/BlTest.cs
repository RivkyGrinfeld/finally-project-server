using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Models
{
    public class BlTest
    {
        public int TestId { get; set; }

        public string CustId { get; set; } = null!;

        public string Grade { get; set; } = null!;

        public BlCustomer Cust { get; set; } = null!;

        public List<BlPointsTest> PointsTest { get; set; } = new List<BlPointsTest>();
    }
}
