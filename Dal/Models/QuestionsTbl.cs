using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class QuestionsTbl
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;

    public int PropertyId { get; set; }

    public int Score { get; set; }

    public bool IsAmerican { get; set; }

    public virtual ICollection<AnswersTbl> AnswersTbls { get; set; } = new List<AnswersTbl>();

    public virtual PropertiesTbl Property { get; set; } = null!;
}
