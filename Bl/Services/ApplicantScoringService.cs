using Bl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Services
{
    public static class ApplicantScoringService
    {
        public static ApplicantScoreDto CalculateScore(BlCustomer customer, BlPosts post)
        {
            double totalScore = 0;
            int countProperties = 0;

            // עבור כל דרישה במשרה
            foreach (var request in post.Requests)
            {
                // חפש את הניקוד של המועמד עבור המאפיין הזה
                var point = customer.Tests
                            .SelectMany(t => t.PointsTest)
                            .FirstOrDefault(p => p.PropertyId == request.PropertyId);

                if (point != null)
                {
                    // חישוב אחוז התאמה פר Property
                    double propertyScore = Math.Min((double)point.GradeProperty / request.MinGradeProperty, 1.0) * 100;
                    totalScore += propertyScore;
                    countProperties++;
                }
            }

            double scoreTest = countProperties > 0 ? totalScore / countProperties : 0;

            return new ApplicantScoreDto
            {
                CustomerId = customer.Id,
                PostId = post.Id,
                ScoreTest = scoreTest,
                ScoreCV = 0,  // נוכל להרחיב בעתיד
                FinalScore = scoreTest // כרגע רק מבחן
            };
        }
    }
}
