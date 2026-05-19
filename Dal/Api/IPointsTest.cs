using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
    public interface IPointsTest : ICrud<PointsTestTbl>
    {
        public Task<PointsTestTbl> Get(int id);
    }
}
