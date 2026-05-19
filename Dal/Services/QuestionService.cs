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
        public class QuestionsService:IQuestions
        {
            private readonly DbManager _dbManager;

            public QuestionsService(DbManager dbManager)
            {
                _dbManager = dbManager;
            }

            // שליפת שאלה לפי ID
            public async Task<QuestionsTbl> Get(int id)
            {
                return await _dbManager.QuestionsTbls
                    .FirstOrDefaultAsync(q => q.Id == id);
            }

            // שליפת כל השאלות
            public async Task<List<QuestionsTbl>> GetAll()
            {
                return await _dbManager.QuestionsTbls.ToListAsync();
            }

            // יצירת שאלה
            public async Task<bool> Create(QuestionsTbl question)
            {
                if (question == null)
                    throw new ArgumentNullException(nameof(question));

                _dbManager.QuestionsTbls.Add(question);
                await _dbManager.SaveChangesAsync();
                return true;
            }

            // עדכון שאלה
            public async Task<bool> Update(QuestionsTbl question)
            {
                var existingQuestion = await _dbManager.QuestionsTbls
                    .FirstOrDefaultAsync(q => q.Id == question.Id);

                if (existingQuestion == null)
                    return false;

                existingQuestion.Text = question.Text;
                existingQuestion.PropertyId = question.PropertyId;
                _dbManager.QuestionsTbls.Update(existingQuestion);
                await _dbManager.SaveChangesAsync();
                return true;
            }

            // מחיקת שאלה
            public async Task<bool> Delete(QuestionsTbl question)
            {
                if (question == null)
                    throw new ArgumentNullException(nameof(question));

                _dbManager.QuestionsTbls.Remove(question);
                await _dbManager.SaveChangesAsync();
                return true;
            }

        public Task<QuestionsTbl> GetById(int t)
        {
            throw new NotImplementedException();
        }
    }
    }

