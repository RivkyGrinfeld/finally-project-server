using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Models.DTOs
{
    public class ConversationMessage
    {
        public string Role { get; set; } // "user" או "assistant"
        public string Content { get; set; }
    }
}
