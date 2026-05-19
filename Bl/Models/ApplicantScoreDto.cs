using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Models
{
    public class ApplicantScoreDto
    {
        public string CustomerId { get; set; } = null!;
        public int PostId { get; set; }
        public double ScoreTest { get; set; } // אחוז התאמה מהמבחן
        public double ScoreCV { get; set; }   // אופציונלי, אם נרצה לכלול פרמטרים נוספים
        public double FinalScore { get; set; } // אחוז התאמה סופי
    }
}
