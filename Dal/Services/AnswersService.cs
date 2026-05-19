using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Dal.Api;

namespace Dal.Services
    {
        public class AnswersService:IAnswars
        {
            private readonly DbManager _dbManager;

            public AnswersService(DbManager dbManager)
            {
                _dbManager = dbManager;
            }

            // שליפת תשובה לפי ID
            public async Task<AnswersTbl> Get(int id)
            {
                return await _dbManager.AnswersTbls
                    .FirstOrDefaultAsync(a => a.Id == id);
            }

            // שליפת כל התשובות
            public async Task<List<AnswersTbl>> GetAll()
            {
                return  _dbManager.AnswersTbls.ToList();
            }

            // יצירת תשובה
            public async Task<bool> Create(AnswersTbl answer)
            {
                if (answer == null)
                    throw new ArgumentNullException(nameof(answer));

                _dbManager.AnswersTbls.Add(answer);
                await _dbManager.SaveChangesAsync();
                return true;
            }

            // עדכון תשובה
            public async Task<bool> Update(AnswersTbl answer)
            {
                var existingAnswer = await _dbManager.AnswersTbls
                    .FirstOrDefaultAsync(a => a.Id == answer.Id);

                if (existingAnswer == null)
                    return false;

                existingAnswer.Text = answer.Text;
                existingAnswer.IsCorrect = answer.IsCorrect;
                _dbManager.AnswersTbls.Update(existingAnswer);
                await _dbManager.SaveChangesAsync();
                return true;
            }

            // מחיקת תשובה
            public async Task<bool> Delete(AnswersTbl answer)
            {
                if (answer == null)
                    throw new ArgumentNullException(nameof(answer));

                _dbManager.AnswersTbls.Remove(answer);
                await _dbManager.SaveChangesAsync();
                return true;
            }

        public Task<AnswersTbl> GetById(int t)
        {
            throw new NotImplementedException();
        }
    }
    }

