using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
    public interface IDal
    {
        public ICustomer Customers { get; }
        public IManager Managers { get; }
        public IPosts Posts { get; }
        public ICompanies Companies { get; }
        public IBranches Branches { get; }
        public IPositions Positions { get; }
        public IProperties Properties { get; }
        public IRequests Requests { get; }
        public IPointsTest PointsTests { get; }
        public IStatus Status { get; }
        public ITest Tests { get; }
        public IApply Apply { get; }
        public IQuestions Questions { get; }
        public IAnswars Answers { get; }
        public IUsers Users { get; }
        public IUserVertificationToken UserVertificationTokens { get; }
    }
}
