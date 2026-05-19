using Bl.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Models
{
    public class BlManagers
    {
        public string Id { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public int UserId { get; set; }

        public string? Address { get; set; }

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;
    }
}
