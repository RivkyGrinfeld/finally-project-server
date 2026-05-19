using Bl.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bl.Models.DTOs
{
    public class CandidateCvDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        // שינוי מ-string ל-List<string>
        public List<string> Skills { get; set; } = new List<string>();

        // Experience ו-Education כבר מערכים (או יש לשנות במידת הצורך)
        public List<ExperienceDto> Experience { get; set; } = new List<ExperienceDto>();
        public List<EducationDto> Education { get; set; } = new List<EducationDto>();

        public List<ConversationMessage> Conversation { get; set; } = new List<ConversationMessage>();
    }
}

