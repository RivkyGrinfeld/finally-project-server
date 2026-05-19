using Bl.Api;
using Bl.Models;
using Dal.Api;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Services
{
    public class BlUserVertificationTokenService : IBlUserVertificationToken
    {
        IDal dal;
        public BlUserVertificationTokenService(IDal dal)
        {
            this.dal = dal;
        }
        public Task<bool> Create(BlUserVertificationToken t)
        {         
            return dal.UserVertificationTokens.Create(Converts.ConvertFromBlUserVerificationTokenToUserVerificationToken(t));               
        }

        public Task<bool> Delete(BlUserVertificationToken t)
        {
            throw new NotImplementedException();
        }

        public Task<List<BlUserVertificationToken>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<BlUserVertificationToken> GetById(int t)
        {
            throw new NotImplementedException();
        }

        public Task<BlUserVertificationToken> GetByToken(string token)
        {
           return dal.UserVertificationTokens.GetByToken(token).ContinueWith(task =>
            {
                var dalToken = task.Result;
                if (dalToken != null)
                {
                    return Converts.ConvertFromUserVerificationTokenToBlUserVerificationToken(dalToken);
                }
                return null;
            });
        }

        public async Task<bool> Update(BlUserVertificationToken t)
        {
            return await dal.UserVertificationTokens.Update(Converts.ConvertFromBlUserVerificationTokenToUserVerificationToken(t));
        }
    }
}
