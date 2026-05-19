using Bl.Services;
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Models
{
    public class BlUserVertificationToken
    {
        public int Id { get; set; }

        public string UserId { get; set; } = null!;

        public string Token { get; set; } = null!;

        public DateTime CreationTime { get; set; }

        public bool IsVerified { get; set; }

        public DateTime ExpirationTime { get; set; }

        public  BlCustomer User { get; set; } = null!;
    }
}
