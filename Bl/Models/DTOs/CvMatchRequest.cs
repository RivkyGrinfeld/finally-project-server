using System;
using System.Collections.Generic;

namespace Bl.Models.DTOs
{
    public class CvMatchRequest
    {
        public string FileName { get; set; } = string.Empty;
        public string? CandidateInfo { get; set; }
        public List<string>? Requirements { get; set; }
    }
}
