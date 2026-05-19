using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Models
{
    public class BlRequest
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public int PropertyId { get; set; }

        public int MinGradeProperty { get; set; }

    }
}

