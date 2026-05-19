//using AutoMapper;
using Bl.Api;
using Bl.Models;
using Dal.Api;
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Services
{
    public class BlCustomerService : IBlCustomer
    {
        IDal dal;

        //IMapper m;
        //private readonly IMapper _mapper;


        //public BlCustomerService(IDal dal, IMapper mapper)
        //{
        //    this.dal = dal;

        //    this._mapper = mapper;
        //    //var config = new MapperConfiguration(cfg =>
        //    //{
        //    //    cfg.CreateMap<  BlProperties, PropertiesTbl>();
        //    //   //cfg.CreateMap<BlCustomer, CustomersTbl>();

        //    //});
        //    //m = new Mapper(config);// config.CreateMapper();
        //}

        public BlCustomerService(IDal dal)
        {
            this.dal = dal;
        }

        public async Task<bool> Create(BlCustomer t)
        {
            CustomersTbl customer = Converts.ConvertFromBlCustomerToCustomer(t);

            customer.Branch = dal.Branches.GetById(customer.BranchId).Result;

            return await dal.Customers.Create(customer);
        }
        public async Task<bool> Delete(BlCustomer t)
        {
            return await dal.Customers.Delete(Converts.ConvertFromBlCustomerToCustomer(t));
        }
        public async Task<BlCustomer> Get(string id)
        {
            return Converts.ConvertFromCustomerToBlCustomer(dal.Customers.Get(id).Result);
        }
        public async Task<List<BlCustomer>> GetAll()
        {
            List<BlCustomer> blList = new List<BlCustomer>();
            dal.Customers.GetAll().Result.ForEach(c => blList.Add(Converts.ConvertFromCustomerToBlCustomer(c)));
            //dal.Customers.GetAll().Result.ForEach(x => x.Tests.ToList().ForEach(c => c.Cust = Converts.ConvertFromCustomerToBlCustomer(dal.Customers.GetById(x.Id))))


            return blList;
        }
        public async Task<bool> Update(BlCustomer t)
        {
            return await dal.Customers.Update(Converts.ConvertFromBlCustomerToCustomer(t));
        }
        public async Task<BlCustomer> GetById(int t)
        {
            throw new NotImplementedException();
        }

        public async Task<BlCustomer> GetByUserId(int userId)
        {
           return Converts.ConvertFromCustomerToBlCustomer(dal.Customers.GetByUserId(userId).Result);
        }
    }
}
//using Bl.Api;
//using Bl.Models;
//using Dal.Api;
//using Dal.Models;
////using Services;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace Bl.Services
//{
//    public class BlCustomerService : IBlCustomer
//    {
//        private readonly IDal _dal;
//        private readonly IEmailService _emailService;

//        // כאן ה־DI כולל גם את שירות המייל
//        public BlCustomerService(IDal dal, IEmailService emailService)
//        {
//            _dal = dal;
//            _emailService = emailService;
//        }

//        // -------------------------
//        // CRUD בסיסי
//        // -------------------------

//        public async Task<bool> Create(BlCustomer t)
//        {
//            CustomersTbl customer = Converts.ConvertFromBlCustomerToCustomer(t);
//            customer.Status = await _dal.Status.GetById(customer.StatusId);
//            customer.Branch = await _dal.Branches.GetById(customer.BranchId);
//            return await _dal.Customers.Create(customer);
//        }

//        public async Task<bool> Update(BlCustomer t)
//        {
//            return await _dal.Customers.Update(Converts.ConvertFromBlCustomerToCustomer(t));
//        }

//        public async Task<bool> Delete(BlCustomer t)
//        {
//            return await _dal.Customers.Delete(Converts.ConvertFromBlCustomerToCustomer(t));
//        }

//        public async Task<BlCustomer> Get(string id)
//        {
//            var c = await _dal.Customers.Get(id);
//            return Converts.ConvertFromCustomerToBlCustomer(c);
//        }

//        public async Task<List<BlCustomer>> GetAll()
//        {
//            var list = await _dal.Customers.GetAll();
//            var blList = new List<BlCustomer>();
//            list.ForEach(c => blList.Add(Converts.ConvertFromCustomerToBlCustomer(c)));
//            return blList;
//        }

//        // -------------------------
//        // קוד אימות למייל
//        // -------------------------
//        public async Task<string> GenerateAndSendVerificationCode(string email)
//        {
//            // 1. יצירת קוד אקראי בן 6 ספרות
//            var code = new Random().Next(100000, 999999).ToString();

//            // 2. שליחת המייל
//            await _emailService.SendEmailAsync(email, "קוד אימות", $"הקוד שלך הוא: {code}");

//            // 3. שמירת הקוד בבסיס הנתונים (טבלה VerificationCodesTbl)
//            var verificationEntry = new VerificationCodesTbl
//            {
//                Email = email,
//                Code = code,
//                CreatedAt = DateTime.UtcNow,
//                ExpireAt = DateTime.UtcNow.AddMinutes(5) // תקפות הקוד
//            };
//            //await _dal.ver.Create(verificationEntry);

//            return code;
//        }

//        // -------------------------
//        // אימות קוד
//        // -------------------------
//        public async Task<bool> VerifyCode(string email, string code)
//        {
//            //var entry = await _dal.VerificationCodes.GetByEmail(email);
//            if (entry == null) return false;

//            // בודקים אם הקוד נכון ועדיין בתוקף
//            if (entry.Code == code && entry.ExpireAt > DateTime.UtcNow)
//            {
//                // אפשר למחוק את הקוד לאחר אימות
//                //await _dal.VerificationCodes.Delete(entry);
//                return true;
//            }

//            return false;
//        }

//        public Task<BlCustomer> GetById(int t)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}