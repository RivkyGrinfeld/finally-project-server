using Bl.Services;

namespace Bl.Api
{
    public interface IBl
    {
        IBlCustomer Customers { get; }
        IBlManager Managers { get; }
        IBlPosts Posts { get; }
        IBlCompanies Companies { get; }
        IBlBranches Branches { get; }
        IBlPositions Positions { get; }
        IBLProperties Properties { get; }
        IBlRequest Request { get; }
        IBlPointsTest PointsTest { get; }
        IBlStatus Status { get; }
        IBlTest Test { get; } 
        IBlApply Apply { get; }
        IBlAnswers Answers { get; }
        IBlQuestions Questions { get; }
        IBlUser Users { get; }
        IBlUserVertificationToken UserVertificationTokens { get; }
    }
}
