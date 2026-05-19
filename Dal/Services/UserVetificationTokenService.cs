using Dal.Api;
using Dal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Services
{
    public class UserVetificationTokenService : IUserVertificationToken
    {
        DbManager dbm;
        public UserVetificationTokenService(DbManager dbm)
        {
            this.dbm = dbm;
        }
        public async Task<bool> Create(UserVerificationToken t)
        {
            if(t == null)
                throw new ArgumentNullException(nameof(t));
            dbm.UserVerificationTokens.Add(t);
            dbm.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(UserVerificationToken t)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserVerificationToken>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<UserVerificationToken> GetById(int t)
        {
            throw new NotImplementedException();
        }

        public async Task<UserVerificationToken> GetByToken(string token)
        {
            return await dbm.UserVerificationTokens.FirstOrDefaultAsync(t => t.Token == token);
        }

        public async Task<bool> Update(UserVerificationToken t)
        {
            UserVerificationToken c = dbm.UserVerificationTokens.ToList().Where(c => c.Id.Equals(t.Id)).FirstOrDefault();
            if (c == null)
                return false;
            c.UserId = t.UserId;
            c.Token = t.Token;
            c.CreationTime = t.CreationTime;
            c.ExpirationTime = t.ExpirationTime;
            c.IsVerified = t.IsVerified;
            dbm.UserVerificationTokens.Update(c);
            dbm.SaveChangesAsync();

            return true;
        }
    }
}
