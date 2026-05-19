using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Models.DTOs
{
    public class AiCvResponse
    {
        public string Question { get; set; } = "";
        public bool IsComplete { get; set; } = false;
        public CandidateCvDto? PolishedCv { get; set; }// יכול להיות null אם עדיין לא מושלם
    }
}
