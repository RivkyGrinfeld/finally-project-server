using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class AnswersTbl
{
    public int Id { get; set; }

    public int QuestionId { get; set; }

    public string Text { get; set; } = null!;

    public bool IsCorrect { get; set; }

    public virtual QuestionsTbl Question { get; set; } = null!;
}
