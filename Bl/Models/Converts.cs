using Bl.Api;
using Dal.Api;
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Models
{
    public static class Converts
    {
        public static BlApply ConvertFromApplyToBlApply(ApplyTbl branch)
        {
            BlApply c = new();
            c.Id = branch.Id;
            c.Confirmed = branch.Confirmed;
            c.Date = branch.Date;
            c.CustId = branch.CustId;
            c.PostId = branch.PostId;
            return c;
        }
        public static ApplyTbl ConvertFromBlApplyToApply(BlApply branch)
        {
            ApplyTbl c = new();
            c.Id = branch.Id;
            c.Confirmed = branch.Confirmed;
            c.Date = branch.Date;
            c.CustId = branch.CustId;
            c.PostId = branch.PostId;
            return c;
        }
        public static BlBranches ConvertFromBranchToBlBranch(BranchesTbl branch)
        {
            BlBranches c = new();
            c.Description = branch.Description;
            c.Id = branch.Id;
            branch.PositionsTbls.ToList().ForEach(x => c.PositionsTbls.Add(ConvertFromPositionToBlPosition(x)));
            return c;
        }
        public static BranchesTbl ConvertFromBlBranchToBranch(BlBranches branch)
        {
            BranchesTbl c = new();
            c.Description = branch.Description;
            c.Id = branch.Id;
            return c;
        }
        public static CompaniesTbl ConvertFromBlCompaniesToCompanies(BlCompanies companies)
        {
            CompaniesTbl c = new();
            c.Id = companies.Id;
            c.Name = companies.Name;
            c.Email = companies.Email;
            c.UserId = companies.UserId;

            return c;
        }
        public static BlCompanies ConvertFromCompaniesToBlCompanies(CompaniesTbl companies)
        {
            BlCompanies c = new();
            c.Id = companies.Id;
            c.Name = companies.Name;
            c.Email = companies.Email;
            c.UserId = companies.UserId;

            return c;

        }
        public static CustomersTbl ConvertFromBlCustomerToCustomer(BlCustomer customer)
        {
            CustomersTbl c = new();
            c.Id = customer.Id.Trim().Substring(0, Math.Min(9, customer.Id.Length));
            c.CreatedAt = customer.CreatedAt;
            //c.Id = customer.Id.Trim();
            c.Address = customer.Address;
            c.City = customer.City;
            c.Email = customer.Email;//////////////////////
            c.BornDate = customer.BornDate;
            c.FirstName = customer.FirstName;
            c.LastName = customer.LastName;
            c.NumOfChildren = customer.NumOfChildren;
            c.Phone = customer.Phone;
            c.BranchId = customer.BranchId;
            c.FileName = customer.FileName;
            c.Url = customer.Url;
            c.UserId = customer.UserId;
            //c.Status = customer.Status;
            return c;
        }
        public static BlCustomer ConvertFromCustomerToBlCustomer(CustomersTbl customer)
        {
            BlCustomer c = new();
            c.Id = customer.Id;
            c.CreatedAt = customer.CreatedAt;

            c.Address = customer.Address;
            c.City = customer.City;
            c.Email = customer.Email;//////////////////////
            c.BornDate = customer.BornDate;
            c.FirstName = customer.FirstName;
            c.LastName = customer.LastName;
            c.NumOfChildren = customer.NumOfChildren;
            c.Phone = customer.Phone;
            c.BranchId = customer.BranchId;
            c.FileName = customer.FileName;
            c.Url = customer.Url;
            c.UserId = customer.UserId;
            customer.ApplyTbls.ToList().ForEach(x => c.Applies.Add(ConvertFromApplyToBlApply(x)));
            customer.TestsTbls.ToList().ForEach(x => c.Tests.Add(ConvertFromTestToBlTest(x)));
            //customer.Tests.ToList().ForEach(x => x.Cust = c.);

            return c;
        }
        public static ManagersTbl ConvertFromBlManagerTomanager(BlManagers manager)
        {
            ManagersTbl m = new();
            m.LastName = manager.LastName;
            m.FirstName = manager.FirstName;
            m.Id = manager.Id;
            m.UserId = manager.UserId;
            return m;
        }
        public static BlManagers ConvertFromManagerToBlManager(ManagersTbl manager)
        {
            BlManagers m = new();
            m.LastName = manager.LastName;
            m.FirstName = manager.FirstName;
            m.Id = manager.Id;
            m.UserId = manager.UserId;
            return m;
        }
        public static PositionsTbl ConvertFromBlPositionToPosition(BlPositions pos)
        {
            PositionsTbl p = new();
            p.Id = pos.Id;
            p.BranchId = pos.BranchId;
            p.Description = pos.Description;
            return p;
        }
        public static BlPositions ConvertFromPositionToBlPosition(PositionsTbl pos)
        {

            BlPositions p = new();
            p.Id = pos.Id;
            p.BranchId = pos.BranchId;
            p.Description = pos.Description;
            return p;
        }
        public static PostsTbl ConvertFromBlPostToPost(BlPosts customer)
        {
            PostsTbl c = new();
            c.Id = customer.Id;
            c.City = customer.City;
            c.IsConfirmed = customer.IsConfirmed;
            c.Date = customer.Date;
            c.IsAvailble = customer.IsAvailble;
            c.Salary = customer.Salary;
            c.CompanyId = customer.CompanyId;
            c.MaxCadidated = customer.MaxCadidated;
            c.PositionId = customer.PositionId;
            customer.Requests.ForEach(x => c.RequestsTbls.Add(ConvertFromBlRequestToRequest(x)));

            return c;
        }
        public static BlPosts ConvertFromPostToBlPost(PostsTbl post)
        {
            BlPosts c = new BlPosts();
            c.Id = post.Id;
            c.City = post.City;
            c.IsConfirmed = post.IsConfirmed;
            c.Date = post.Date;
            c.IsAvailble = post.IsAvailble;
            c.Salary = post.Salary;
            c.CompanyId = post.CompanyId;
            c.MaxCadidated = post.MaxCadidated;
            c.PositionId = post.PositionId;


            if (post.RequestsTbls != null && post.RequestsTbls.Any())
            {
                c.Requests = post.RequestsTbls
                    .Select(x => ConvertFromRequestToBlRequest(x))
                    .ToList();
            }

            return c;
        }
        public static PropertiesTbl ConvertFromBlPropertiesToProperties(BlProperties properties)
        {
            PropertiesTbl p = new();
            p.Id = properties.Id;
            p.Description = properties.Description;

            //c.Status = customer.Status;
            return p;
        }
        public static BlProperties ConvertFromPropertiesToBlPropeties(PropertiesTbl properties)
        {
            BlProperties p = new();
            p.Id = properties.Id;
            p.Description = properties.Description;
            return p;
        }
        public static RequestsTbl ConvertFromBlRequestToRequest(BlRequest request)
        {
            RequestsTbl r = new();
            r.Id = request.Id;
            r.PostId = request.PostId;
            r.PropertyId = request.PropertyId;
            r.MinGradeProperty = request.MinGradeProperty;
            //r.Property = 
            //r.Property = ConvertFromBlPropertiesToProperties(request.Property);
            return r;
        }
        public static BlRequest ConvertFromRequestToBlRequest(RequestsTbl request)
        {
            BlRequest r = new();
            r.Id = request.Id;
            r.PostId = request.PostId;
            r.MinGradeProperty = request.MinGradeProperty;
            //r.Post = request.Post;//////////////

            //r.Property = ConvertFromPropertiesToBlProperties(request.Property);
            return r;
        }
        public static PointsTestTbl ConvertFromBlPointsTestToPointsTest(BlPointsTest p)
        {
            PointsTestTbl r = new();
            r.Id = p.Id;
            r.PropertyId = p.PropertyId;
            r.GradeProperty = p.GradeProperty;
            r.TestId = p.TestId;
            //r.Property = ConvertFromBlPropertiesToProperties(p.Property);
            //r.Test = 
            return r;
        }
        public static BlPointsTest ConvertFromPointsTestToBlPointsTest(PointsTestTbl p)
        {
            BlPointsTest r = new();
            r.Id = p.Id;
            r.PropertyId = p.PropertyId;
            r.TestId = p.TestId;
            r.GradeProperty = p.GradeProperty;
            //r.Property = ConvertFromPropertiesToBlPropeties(p.Property);
            //r.Test =
            return r;
        }
        public static TestsTbl ConvertFromBlTestToTest(BlTest t)
        {
            TestsTbl r = new();
            r.TestId = t.TestId;
            r.CustId = t.CustId;
            r.Grade = t.Grade;
            
            //r.Cust = ConvertFromBlCustomerToCustomer(t.Cust);

            //r.PointsTestTbls
            return r;
        }
        public static BlTest ConvertFromTestToBlTest(TestsTbl t)
        {
            BlTest r = new();
            r.TestId = t.TestId;
            r.CustId = t.CustId;
            r.Grade = t.Grade;
            //r.Cust
            t.PointsTestTbls.ToList().ForEach(x => r.PointsTest.Add(ConvertFromPointsTestToBlPointsTest(x)));
            return r;
        }
        public static StatusTbl ConvertFromBlStatusToStatus(BlStatus t)
        {
            StatusTbl r = new();
            r.Id = t.Id;
            r.Description = t.Description;
            //r.CustomersTbls = 
            return r;
        }
        public static BlStatus ConvertFromStatusToBlStatus(StatusTbl t)
        {
            BlStatus r = new();
            r.Id = t.Id;
            r.Description = t.Description;
            //r.Customers = 
            return r;
        }
        public static UserVerificationToken ConvertFromBlUserVerificationTokenToUserVerificationToken(BlUserVertificationToken t)
        {
            UserVerificationToken r = new();
            r.Id = t.Id;
            r.UserId = t.UserId;
            r.Token = t.Token;
            r.CreationTime = t.CreationTime;
            r.ExpirationTime = t.ExpirationTime;
            r.IsVerified = t.IsVerified;
            //r.User = ConvertFromBlCustomerToCustomer(t.User);
            return r;
        }
        public static BlUserVertificationToken ConvertFromUserVerificationTokenToBlUserVerificationToken(UserVerificationToken t)
        {
            BlUserVertificationToken r = new();
            r.Id = t.Id;
            r.UserId = t.UserId;
            r.Token = t.Token;
            r.CreationTime = t.CreationTime;
            r.ExpirationTime = t.ExpirationTime;
            r.IsVerified = t.IsVerified;
            r.User = ConvertFromCustomerToBlCustomer(t.User);
            return r;
        }

        public static AnswersTbl ConvertFromBlAnswerToAnswer(BlAnswers blAnswer)
        {
            if (blAnswer == null)
                return null;

            return new AnswersTbl
            {
                Id = blAnswer.Id,
                QuestionId = blAnswer.QuestionId,
                Text = blAnswer.Text,
                IsCorrect = blAnswer.IsCorrect
            };
        }

        public static BlAnswers ConvertFromAnswerToBlAnswer(AnswersTbl answer)
        {
            if (answer == null)
                return null;

            return new BlAnswers
            {
                Id = answer.Id,
                QuestionId = answer.QuestionId,
                Text = answer.Text,
                IsCorrect = answer.IsCorrect
            };
        }




        public static QuestionsTbl ConvertFromBlQuestionToQuestion(BlQuestions blQuestion)
        {
            if (blQuestion == null)
                return null;

            return new QuestionsTbl
            {
                Id = blQuestion.Id,
                Text = blQuestion.Text,
                PropertyId = blQuestion.PropertyId,
                Score = blQuestion.Score,
                IsAmerican = blQuestion.IsAmerican
            };
        }

        public static BlQuestions ConvertFromQuestionToBlQuestion(QuestionsTbl question)
        {
            if (question == null)
                return null;

            return new BlQuestions
            {
                Id = question.Id,
                Text = question.Text,
                PropertyId = question.PropertyId,
                Score = question.Score,
                IsAmerican = question.IsAmerican
            };
        }
        public static BlUser ConvertFromUserToBlUser(User question)
        {
            if (question == null)
                return null;

            return new BlUser
            {
                Id = question.Id,
                UserName = question.UserName,
                Password = question.Password,
                StatusId = question.StatusId,
            };
        }
        public static User ConvertFromBlUserToUser(BlUser question)
        {
            if (question == null)
                return null;

            return new User
            {
                Id = question.Id,
                UserName = question.UserName,
                Password = question.Password,
                StatusId = question.StatusId,
            };
        }
    }
}

