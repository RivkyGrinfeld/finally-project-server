using Bl.Api;
using Bl.Models;
using Dal.Api;
using Dal.Models;
using Dal;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;

namespace Bl.Services
{
    public class BlAnswersService : IBlAnswers
    {
        private readonly IDal _dal;

        public BlAnswersService(IDal dal)
        {
            _dal = dal;
        }

        // יצירת תשובה ב-BL
        public async Task<bool> Create(BlAnswers blAnswer)
        {
            AnswersTbl answer = Converts.ConvertFromBlAnswerToAnswer(blAnswer);
            return await _dal.Answers.Create(answer);
        }

        // עדכון תשובה ב-BL
        public async Task<bool> Update(BlAnswers blAnswer)
        {
            AnswersTbl answer = Converts.ConvertFromBlAnswerToAnswer(blAnswer);
            return await _dal.Answers.Update(answer);
        }

        // מחיקת תשובה ב-BL
        public async Task<bool> Delete(BlAnswers blAnswer)
        {
            AnswersTbl answer = Converts.ConvertFromBlAnswerToAnswer(blAnswer);
            return await _dal.Answers.Delete(answer);
        }

        // שליפת תשובה לפי ID ב-BL
        public async Task<BlAnswers> Get(int id)
        {
            var answer = await _dal.Answers.GetById(id);
            return Converts.ConvertFromAnswerToBlAnswer(answer);
        }

        // שליפת כל התשובות ב-BL
        public async Task<List<BlAnswers>> GetAll()
        {
            List<BlAnswers> blA = new List<BlAnswers>();
            _dal.Answers.GetAll().Result.ForEach(x => blA.Add(Converts.ConvertFromAnswerToBlAnswer(x)));
            return blA;          
        }

        public Task<BlAnswers> GetById(int t)
        {
            throw new NotImplementedException();
        }
    }
}