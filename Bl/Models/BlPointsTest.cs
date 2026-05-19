using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Models
{
    public class BlPointsTest
    {
        public int Id { get; set; }

        public int TestId { get; set; }

        public int PropertyId { get; set; }

        public int GradeProperty { get; set; }

        public BlProperties Property { get; set; } = null!;

        public BlTest Test { get; set; } = null!;
    }
}
