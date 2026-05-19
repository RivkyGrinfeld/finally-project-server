using Dal.Api;
using Dal.Models;
using Dal.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dal
{
    public class DalManager : IDal
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
        public DalManager()
        {

            ServiceCollection services = new ServiceCollection();
            services.AddDbContext<DbManager>();
            services.AddSingleton<ICustomer, CustomerService>();
            services.AddSingleton<IManager, ManagersService>();
            services.AddSingleton<IPosts, PostsService>();
            services.AddSingleton<ICompanies, CompaniesService>();
            services.AddSingleton<IBranches, BranchService>();
            services.AddSingleton<IPositions, PositionsService>();
            services.AddSingleton<IProperties, PropertiesService>();
            services.AddSingleton<IRequests, RequestService>();
            services.AddSingleton<IPointsTest, PointsTestService>();
            services.AddSingleton<IStatus, StatusService>();
            services.AddSingleton<ITest, TestsService>();
            services.AddSingleton<IApply, ApplyService>();
            services.AddSingleton<IAnswars, AnswersService>();
            services.AddSingleton<IQuestions, QuestionsService>();
            services.AddSingleton<IUserVertificationToken, UserVetificationTokenService>();
            services.AddSingleton<IUsers, UserService>();


            ServiceProvider servicesProvider = services.BuildServiceProvider();
            Customers = servicesProvider.GetRequiredService<ICustomer>();
            Managers = servicesProvider.GetRequiredService<IManager>();
            Posts = servicesProvider.GetRequiredService<IPosts>();
            Companies = servicesProvider.GetRequiredService<ICompanies>();
            Branches = servicesProvider.GetRequiredService<IBranches>();
            Positions = servicesProvider.GetRequiredService<IPositions>();
            Properties = servicesProvider.GetRequiredService<IProperties>();
            Requests = servicesProvider.GetRequiredService<IRequests>();
            PointsTests = servicesProvider.GetRequiredService<IPointsTest>();
            Status = servicesProvider.GetRequiredService<IStatus>();
            Tests = servicesProvider.GetRequiredService<ITest>();
            Apply = servicesProvider.GetRequiredService<IApply>();
            Questions = servicesProvider.GetRequiredService<IQuestions>();
            Answers = servicesProvider.GetRequiredService<IAnswars>();
            UserVertificationTokens = servicesProvider.GetRequiredService<IUserVertificationToken>();
            Users = servicesProvider.GetRequiredService<IUsers>();
        }
    }
}
