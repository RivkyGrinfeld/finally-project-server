//using AutoMapper;
using Bl.Api;
using Bl.Models;
using Bl.Services;
using Dal;
using Dal.Api;
using Dal.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Bl
{
    public class BlManager : IBl
    {
        public IBlCustomer Customers { get; }
        public IBlManager Managers { get; }
        public IBlPosts Posts { get; }
        public IBlCompanies Companies { get; }
        public IBlBranches Branches { get; }
        public IBlPositions Positions { get; }
        public IBLProperties Properties { get; }
        public IBlRequest Request { get; }
        public IBlPointsTest PointsTest { get; }
        public IBlStatus Status { get; }
        public IBlTest Test { get; }

        public IBlApply Apply { get; }
        public IBlAnswers Answers { get; }
        public IBlQuestions Questions { get; }
        public IBlUser Users { get; }
        public IBlUserVertificationToken UserVertificationTokens { get; }
        public BlManager()
        {

            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<IDal, DalManager>();
            services.AddSingleton<IBlCustomer, BlCustomerService>();
            services.AddSingleton<IBlManager, BlManagerService>();
            services.AddSingleton<IBlPosts, BlPostsService>();
            services.AddSingleton<IBlCompanies, BlCompaniesService>();
            services.AddSingleton<IBlBranches, BlBranchesService>();
            services.AddSingleton<IBlPositions, BlPositionsService>();
            services.AddSingleton<IBLProperties, BlPropertiesService>();
            services.AddSingleton<IBlRequest, BlRequestService>();
            services.AddSingleton<IBlPointsTest, BlPointsTestService>();
            services.AddSingleton<IBlStatus, BlStatusService>();
            services.AddSingleton<IBlTest, BlTestService>();
            services.AddSingleton<IBlApply, BlApplyService>();
            services.AddSingleton<IBlQuestions, BlQuestionsService>();
            services.AddSingleton<IBlAnswers, BlAnswersService>();
            services.AddSingleton<IBlUser, BlUserService>();
            services.AddSingleton<IBlUserVertificationToken, BlUserVertificationTokenService>();
            //services.AddSingleton<IMapper, Mapper>();
            //services.AddAutoMapper(typeof(Program).Assembly); // Scans the current assembly

            ServiceProvider servicesProvider = services.BuildServiceProvider();
            Customers = servicesProvider.GetRequiredService<IBlCustomer>();
            Managers = servicesProvider.GetRequiredService<IBlManager>();
            Posts = servicesProvider.GetRequiredService<IBlPosts>();
            Companies = servicesProvider.GetRequiredService<IBlCompanies>();
            Branches = servicesProvider.GetRequiredService<IBlBranches>();
            Positions = servicesProvider.GetRequiredService<IBlPositions>();
            Properties = servicesProvider.GetRequiredService<IBLProperties>();////////////////
            Request = servicesProvider.GetRequiredService<IBlRequest>();
            PointsTest = servicesProvider.GetRequiredService<IBlPointsTest>();
            Status = servicesProvider.GetRequiredService<IBlStatus>();
            Test = servicesProvider.GetRequiredService<IBlTest>();
            Apply = servicesProvider.GetRequiredService<IBlApply>();
            Answers = servicesProvider.GetRequiredService<IBlAnswers>();
            Questions = servicesProvider.GetRequiredService<IBlQuestions>();
            Users = servicesProvider.GetRequiredService<IBlUser>();
            UserVertificationTokens = servicesProvider.GetRequiredService<IBlUserVertificationToken>();
        }
    }
}
