using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Models
{
    public class BlCompanies
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;
        public int UserId { get; set; }
        public string Password { get; set; } = null!;
        public string UserName { get; set; } = null!;

        //public  List<PostsTbl> PostsTbls { get; set; } = new List<PostsTbl>();
    }
}
