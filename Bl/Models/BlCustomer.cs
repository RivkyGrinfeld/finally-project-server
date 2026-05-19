
using Bl.Services;
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Models
{
    public class BlCustomer
    {
        public string Id { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateTime BornDate { get; set; }

        public string City { get; set; } = null!;

        public string? Address { get; set; }

        public string Email { get; set; } = null!;

        public int? NumOfChildren { get; set; }

        public int BranchId { get; set; }

        public string Phone { get; set; } = null!;

        public string? FileName { get; set; }

        public string? Url { get; set; }
        public DateTime CreatedAt { get; set; }


        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; } 

        //public virtual BranchesTbl Branch { get; set; } = null!;

        //public virtual StatusTbl Status { get; set; } = null!;
        public List<BlApply> Applies { get; set; } = new List<BlApply>();

        public List<BlTest> Tests { get; set; } = new List<BlTest>();
    }
}
