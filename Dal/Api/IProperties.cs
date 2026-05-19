using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
    public interface IProperties : ICrud<PropertiesTbl>
    {
        public Task<PropertiesTbl> Get(int id);
    }
}