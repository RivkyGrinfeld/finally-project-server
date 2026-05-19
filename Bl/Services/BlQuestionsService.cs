using Bl.Api;
using Bl.Models;
using Dal.Api;
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bl.Services
{
    public class BlQuestionsService : IBlQuestions
    {
        private readonly IDal _dal;

        public BlQuestionsService(IDal dal)
        {
            _dal = dal;
        }

        // יצירת שאלה ב-BL
        public async Task<bool> Create(BlQuestions blQuestion)
        {
            QuestionsTbl question = Converts.ConvertFromBlQuestionToQuestion(blQuestion);
            return await _dal.Questions.Create(question);
        }

        // עדכון שאלה ב-BL
        public async Task<bool> Update(BlQuestions blQuestion)
        {
            QuestionsTbl question = Converts.ConvertFromBlQuestionToQuestion(blQuestion);
            return await _dal.Questions.Update(question);
        }

        // מחיקת שאלה ב-BL
        public async Task<bool> Delete(BlQuestions blQuestion)
        {
            QuestionsTbl question = Converts.ConvertFromBlQuestionToQuestion(blQuestion);
            return await _dal.Questions.Delete(question);
        }

        // שליפת שאלה לפי ID ב-BL
        public async Task<BlQuestions> Get(int id)
        {
            var question = await _dal.Questions.GetById(id);
            return Converts.ConvertFromQuestionToBlQuestion(question);
        }

        // שליפת כל השאלות ב-BL
        public async Task<List<BlQuestions>> GetAll()
        {
           
            List<BlQuestions> blList = new List<BlQuestions>();
            _dal.Questions.GetAll().Result.ForEach(c => blList.Add(Converts.ConvertFromQuestionToBlQuestion(c)));
            return blList;
        }

        public Task<BlQuestions> GetById(int t)
        {
            throw new NotImplementedException();
        }
    }
}