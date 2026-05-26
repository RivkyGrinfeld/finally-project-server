using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bl.Models
{
    public class BlPosts
    {

        public int Id { get; set; }

        public int CompanyId { get; set; }

        public int PositionId { get; set; }

        public bool IsAvailble { get; set; }

        public string City { get; set; } = null!;

        public long? Salary { get; set; }

        public DateTime Date { get; set; }
        public int MaxCadidated { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public string JobDescription { get; set; } = string.Empty;

        public bool IsConfirmed { get; set; } = false;

        //public CompaniesTbl Company { get; set; } = null!;

        //public virtual PositionsTbl Position { get; set; } = null!;
        //[JsonIgnore]
        public List<BlRequest> Requests { get; set; } = new List<BlRequest>();
    }

}