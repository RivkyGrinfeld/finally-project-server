using Bl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Api
{
    public interface IBlTest:IBlCrud<BlTest>
    {
        public Task<BlTest> Get(int id);
    }   
}
