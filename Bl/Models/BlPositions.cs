using Dal.Models;

namespace Bl.Models
{
    public class BlPositions
    {

        public int Id { get; set; }

        public int BranchId { get; set; }

        public string Description { get; set; } = null!;


        //public List<PostsTbl> PostsTbls { get; set; } = new List<PostsTbl>();


    }
}