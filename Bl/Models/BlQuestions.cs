using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Models
{
    public class BlQuestions
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int PropertyId { get; set; }
        public int Score { get; set; }
        public bool IsAmerican { get; set; }

        //public virtual ICollection<AnswersTbl> AnswersTbls { get; set; } = new List<AnswersTbl>();

        //public virtual PropertiesTbl? Property { get; set; }
    }
}
