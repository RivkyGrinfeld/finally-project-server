using Bl.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Services
{
    public class CvValidator
    {
        public static string? GetNextQuestion(CandidateCvDto cv)
        {
            if (string.IsNullOrWhiteSpace(cv.FullName))
                return "מה השם המלא שלך?";

            if (string.IsNullOrWhiteSpace(cv.Email))
                return "מה כתובת המייל שלך?";

            if (string.IsNullOrWhiteSpace(cv.Phone))
                return "מה מספר הטלפון שלך?";

            if (!cv.Skills.Any())
                return "אילו כישורים או טכנולוגיות יש לך?";

            if (!cv.Experience.Any())
                return "מה הניסיון התעסוקתי שלך? ציין תפקיד וחברה.";

            if (!cv.Education.Any())
                return "מה ההשכלה שלך? ציין מוסד ותואר.";

            return null; // כל השדות מלאים
        }
    }
}
