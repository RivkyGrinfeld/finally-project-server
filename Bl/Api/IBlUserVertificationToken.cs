using Bl.Models;
using Dal.Api;
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Api
{
    public interface IBlUserVertificationToken:ICrud<BlUserVertificationToken>
    {
       public Task<BlUserVertificationToken> GetByToken(string token);
    }
}
