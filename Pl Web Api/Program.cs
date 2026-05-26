
////////using AutoMapper;
////////using Bl;
////////using Bl.Api;
////////using Bl.Services;
////////using Dal.Api;
////////using Dal.Models;
////////using Dal.Services;
////////using Microsoft.EntityFrameworkCore;
////////using Microsoft.Extensions.DependencyInjection;
////////using Microsoft.Extensions.FileProviders;
////////using System;
////////using Microsoft.Extensions.FileProviders;

////////namespace Pl_Web_Api
////////{
////////    public class Program
////////    {
////////        public static void Main(string[] args)
////////        {

////////            //var config = new MapperConfiguration(cfg =>
////////            //{
////////            //    cfg.CreateMap<BlCustomer, CustomersTbl>();

////////            //});
////////            //m = config.CreateMapper();

////////            var builder = WebApplication.CreateBuilder(args);

////////            //        builder.Services.AddDbContext<DbManager>(options =>
////////            //options.UseSqlServer("YourConnectionStringHere"));

////////            //        builder.Services.AddScoped<IVerificationCodes, VerificationCodesService>();
////////            //builder.Services.AddScoped<VerificationCodeBL>();

////////            // Add services to the container.
////////            builder.Services.AddSingleton<IBl, BlManager>();// new blmanager

////////            builder.Services.AddControllers();
////////            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
////////            builder.Services.AddEndpointsApiExplorer();
////////            builder.Services.AddSwaggerGen();
////////            var MyAllowSpecificOrigins = "myAllowSpecificOrigins";

////////            builder.Services.AddCors(options =>
////////            {
////////                options.AddPolicy(name: MyAllowSpecificOrigins,
////////                    builder =>
////////                    {
////////                        builder.WithOrigins("http://localhost:4200")
////////                               .AllowAnyHeader()
////////                               .AllowAnyMethod();
////////                    });
////////            });

////////            var app = builder.Build();

////////            /////////////////////
////////            ///





////////            //var builder = WebApplication.CreateBuilder(args);


////////            // Configure the HTTP request pipeline.
////////            if (app.Environment.IsDevelopment())
////////            {
////////                app.UseSwagger();
////////                app.UseSwaggerUI();
////////            }

////////            app.UseHttpsRedirection();

////////            app.UseAuthorization();
////////            ///////
////////            app.UseStaticFiles();
////////            app.UseRouting();
////////            app.UseCors("myAllowSpecificOrigins");
////////            /////
////////            ///


////////            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

////////            app.UseStaticFiles(new StaticFileOptions
////////            {
////////                FileProvider = new PhysicalFileProvider(uploadsPath),
////////                RequestPath = "/Uploads"
////////            });
////////            app.MapControllers();
////////            app.UseStaticFiles();
////////            app.Run();


////////        }
////////    }
////////}
////////using Microsoft.AspNetCore.Authentication.JwtBearer;
////////using Microsoft.Extensions.DependencyInjection;
////////using Microsoft.Extensions.FileProviders;
////////using Microsoft.IdentityModel.Tokens;
////////using Microsoft.OpenApi.Models;
////////using System.Text;
//////////using AutoMapper;
////////using Bl;
////////using Bl.Api;
////////using Bl.Services;
////////using Dal.Api;
////////using Dal.Models;
////////using Dal.Services;
////////using Microsoft.EntityFrameworkCore;
////////using Microsoft.Extensions.DependencyInjection;
////////using Microsoft.Extensions.FileProviders;
////////using System;
////////using Microsoft.Extensions.FileProviders;

////////namespace Pl_Web_Api
////////{
////////    public class Program
////////    {
////////        public static void Main(string[] args)
////////        {
////////            var builder = WebApplication.CreateBuilder(args);

////////            // קביעת הגדרות השירותים
////////            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
////////                .AddJwtBearer(options =>
////////                {
////////                    options.TokenValidationParameters = new TokenValidationParameters
////////                    {
////////                        ValidateIssuer = true,
////////                        ValidateAudience = true,
////////                        ValidateLifetime = true,
////////                        ValidateIssuerSigningKey = true,
////////                        ValidIssuer = "yourIssuer",  // הגדרת המנפיק (issuer)
////////                        ValidAudience = "yourAudience",  // הגדרת הקהל (audience)
////////                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("yourSecretKey"))  // המפתח הסודי (תשמור עליו בסוד!)
////////                    };
////////                });

////////            // הוספת שירותי API ו-Swagger
////////            builder.Services.AddControllers();
////////            builder.Services.AddEndpointsApiExplorer();
////////            builder.Services.AddSwaggerGen(c =>
////////            {
////////                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pl_Web_Api", Version = "v1" });
////////            });

////////            // הגדרת CORS
////////            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
////////            builder.Services.AddCors(options =>
////////            {
////////                options.AddPolicy(name: MyAllowSpecificOrigins,
////////                    policy =>
////////                    {
////////                        policy.WithOrigins("http://localhost:4200") // אפשר גישה רק לכתובת הזו
////////                              .AllowAnyHeader()
////////                              .AllowAnyMethod();
////////                    });
////////            });

////////            // יצירת היישום
////////            var app = builder.Build();

////////            // קונפיגורציה עבור Swagger UI בפיתוח
////////            if (app.Environment.IsDevelopment())
////////            {
////////                app.UseSwagger();
////////                app.UseSwaggerUI(c =>
////////                {
////////                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pl_Web_Api v1");
////////                });
////////            }

////////            // הוספת תמיכה בפרוטוקול https
////////            app.UseHttpsRedirection();

////////            // הפעלת שימוש בהגדרות קורס
////////            app.UseCors(MyAllowSpecificOrigins);

////////            // הפעלת Authorization ו-Authentication (אימות)
////////            app.UseAuthentication();
////////            app.UseAuthorization();

////////            // הגדרת Static Files - תיקיית Uploads
////////            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
////////            app.UseStaticFiles(new StaticFileOptions
////////            {
////////                FileProvider = new PhysicalFileProvider(uploadsPath),
////////                RequestPath = "/Uploads"
////////            });

////////            // קביעת מסלול לשליחה של Controllers
////////            app.MapControllers();

////////            // הרצת היישום
////////            app.Run();



////////        }
////////    }
////////}



//////using AutoMapper;
//////using Bl;
//////using Bl.Api;
//////using Bl.Services;
//////using Dal.Api;
//////using Dal.Models;
//////using Dal.Services;
//////using Microsoft.EntityFrameworkCore;
//////using Microsoft.Extensions.DependencyInjection;
//////using Microsoft.Extensions.FileProviders;
//////using System;
//////using Microsoft.Extensions.FileProviders;

//////namespace Pl_Web_Api
//////{
//////    public class Program
//////    {
//////        public static void Main(string[] args)
//////        {

//////            //var config = new MapperConfiguration(cfg =>
//////            //{
//////            //    cfg.CreateMap<BlCustomer, CustomersTbl>();

//////            //});
//////            //m = config.CreateMapper();

//////            var builder = WebApplication.CreateBuilder(args);

//////            //        builder.Services.AddDbContext<DbManager>(options =>
//////            //options.UseSqlServer("YourConnectionStringHere"));

//////            //        builder.Services.AddScoped<IVerificationCodes, VerificationCodesService>();
//////            //builder.Services.AddScoped<VerificationCodeBL>();

//////            // Add services to the container.
//////            builder.Services.AddSingleton<IBl, BlManager>();// new blmanager

//////            builder.Services.AddControllers();
//////            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//////            builder.Services.AddEndpointsApiExplorer();
//////            builder.Services.AddSwaggerGen();
//////            var MyAllowSpecificOrigins = "myAllowSpecificOrigins";

//////            builder.Services.AddCors(options =>
//////            {
//////                options.AddPolicy(name: MyAllowSpecificOrigins,
//////                    builder =>
//////                    {
//////                        builder.WithOrigins("http://localhost:4200")
//////                               .AllowAnyHeader()
//////                               .AllowAnyMethod();
//////                    });
//////            });

//////            var app = builder.Build();

//////            /////////////////////
//////            ///





//////            //var builder = WebApplication.CreateBuilder(args);


//////            // Configure the HTTP request pipeline.
//////            if (app.Environment.IsDevelopment())
//////            {
//////                app.UseSwagger();
//////                app.UseSwaggerUI();
//////            }

//////            app.UseHttpsRedirection();

//////            app.UseAuthorization();
//////            ///////
//////            app.UseStaticFiles();
//////            app.UseRouting();
//////            app.UseCors("myAllowSpecificOrigins");
//////            /////
//////            ///


//////            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

//////            app.UseStaticFiles(new StaticFileOptions
//////            {
//////                FileProvider = new PhysicalFileProvider(uploadsPath),
//////                RequestPath = "/Uploads"
//////            });
//////            app.MapControllers();
//////            app.UseStaticFiles();
//////            app.Run();


//////        }
//////    }
//////}


//using AutoMapper;
//using Bl;
//using Bl.Api;
//using Bl.Services;
//using Dal.Api;
//using Dal.Models;
//using Dal.Services;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.FileProviders;
//using Microsoft.Extensions.FileProviders;
//using QuestPDF.Infrastructure;
//using System;

//namespace Pl_Web_Api
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {

//            //var config = new MapperConfiguration(cfg =>
//            //{
//            //    cfg.CreateMap<BlCustomer, CustomersTbl>();

//            //});
//            //m = config.CreateMapper();

//            var builder = WebApplication.CreateBuilder(args);

//            builder.Services.AddDbContext<DbManager>(options =>
//    options.UseSqlServer("YourConnectionStringHere"));

//            //        builder.Services.AddScoped<IVerificationCodes, VerificationCodesService>();
//            //builder.Services.AddScoped<VerificationCodeBL>();

//            // Add services to the container.
//            builder.Services.AddSingleton<IBl, BlManager>();// new blmanager
//            builder.Services.AddScoped<OpenAiService>();
//            QuestPDF.Settings.License = LicenseType.Community; // או Evaluation
//            builder.Services.AddControllers();
//            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//            builder.Services.AddEndpointsApiExplorer();
//            builder.Services.AddSwaggerGen();
//            var MyAllowSpecificOrigins = "myAllowSpecificOrigins";
//            builder.Services.AddCors(options =>
//            {
//                options.AddPolicy(name: MyAllowSpecificOrigins,
//                    builder =>
//                    {
//                        builder.WithOrigins("http://localhost:4200")
//                               .AllowAnyHeader()
//                               .AllowAnyMethod();
//                    });
//            });
//            builder.WebHost.UseUrls("https://localhost:7006");

//            var app = builder.Build();



//            //var builder = WebApplication.CreateBuilder(args);


//            // Configure the HTTP request pipeline.
//            if (app.Environment.IsDevelopment())
//            {
//                app.UseSwagger();
//                app.UseSwaggerUI();
//            }

//            app.UseHttpsRedirection();

//            app.UseAuthorization();
//            ///////
//            app.UseStaticFiles();
//            app.UseRouting();
//            app.UseCors("myAllowSpecificOrigins");
//            /////
//            ///


//            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

//            app.UseStaticFiles(new StaticFileOptions
//            {
//                FileProvider = new PhysicalFileProvider(uploadsPath),
//                RequestPath = "/Uploads"
//            });
//            app.MapControllers();
//            app.UseStaticFiles();
//            app.Run();


//        }
//    }
//}




////var builder = WebApplication.CreateBuilder(args);

////// Services
////builder.Services.AddSingleton<IBl, BlManager>();
////builder.Services.AddScoped<OpenAiService>();
////QuestPDF.Settings.License = LicenseType.Community;

////builder.Services.AddControllers();
////builder.Services.AddEndpointsApiExplorer();
////builder.Services.AddSwaggerGen();

////var MyAllowSpecificOrigins = "myAllowSpecificOrigins";
////builder.Services.AddCors(options =>
////{
////    options.AddPolicy(name: MyAllowSpecificOrigins,
////        policy =>
////        {
////            policy.WithOrigins("http://localhost:4200")
////                  .AllowAnyHeader()
////                  .AllowAnyMethod();
////        });
////});

////// Listen on HTTP port only
////builder.WebHost.UseUrls("https://localhost:7006");

////var app = builder.Build();

////// Swagger – תמיד נגיש
////app.UseSwagger();
////app.UseSwaggerUI(c =>
////{
////    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pl_Web_Api v1");
////});

////// Middleware
////app.UseRouting();
////app.UseCors(MyAllowSpecificOrigins);
////app.UseAuthorization();

////// Static files – wwwroot
////app.UseStaticFiles();

////// Static files – Uploads folder
////var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
////app.UseStaticFiles(new StaticFileOptions
////{
////    FileProvider = new PhysicalFileProvider(uploadsPath),
////    RequestPath = "/Uploads"
////});

////// Controllers
////app.MapControllers();

////app.Run()
///
/// 
/// 
/// ;
/// 


////using AutoMapper;
////using Bl;
////using Bl.Api;
////using Bl.Services;
////using Dal.Api;
////using Dal.Models;
////using Dal.Services;
////using Microsoft.EntityFrameworkCore;
////using Microsoft.Extensions.DependencyInjection;
////using Microsoft.Extensions.FileProviders;
////using System;
////using Microsoft.Extensions.FileProviders;

////namespace Pl_Web_Api
////{
////    public class Program
////    {
////        public static void Main(string[] args)
////        {

////            //var config = new MapperConfiguration(cfg =>
////            //{
////            //    cfg.CreateMap<BlCustomer, CustomersTbl>();

////            //});
////            //m = config.CreateMapper();

////            var builder = WebApplication.CreateBuilder(args);

////            //        builder.Services.AddDbContext<DbManager>(options =>
////            //options.UseSqlServer("YourConnectionStringHere"));

////            //        builder.Services.AddScoped<IVerificationCodes, VerificationCodesService>();
////            //builder.Services.AddScoped<VerificationCodeBL>();

////            // Add services to the container.
////            builder.Services.AddSingleton<IBl, BlManager>();// new blmanager

////            builder.Services.AddControllers();
////            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
////            builder.Services.AddEndpointsApiExplorer();
////            builder.Services.AddSwaggerGen();
////            var MyAllowSpecificOrigins = "myAllowSpecificOrigins";

////            builder.Services.AddCors(options =>
////            {
////                options.AddPolicy(name: MyAllowSpecificOrigins,
////                    builder =>
////                    {
////                        builder.WithOrigins("http://localhost:4200")
////                               .AllowAnyHeader()
////                               .AllowAnyMethod();
////                    });
////            });

////            var app = builder.Build();

////            /////////////////////
////            ///





////            //var builder = WebApplication.CreateBuilder(args);


////            // Configure the HTTP request pipeline.
////            if (app.Environment.IsDevelopment())
////            {
////                app.UseSwagger();
////                app.UseSwaggerUI();
////            }

////            app.UseHttpsRedirection();

////            app.UseAuthorization();
////            ///////
////            app.UseStaticFiles();
////            app.UseRouting();
////            app.UseCors("myAllowSpecificOrigins");
////            /////
////            ///


////            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

////            app.UseStaticFiles(new StaticFileOptions
////            {
////                FileProvider = new PhysicalFileProvider(uploadsPath),
////                RequestPath = "/Uploads"
////            });
////            app.MapControllers();
////            app.UseStaticFiles();
////            app.Run();


////        }
////    }
////}
////using Microsoft.AspNetCore.Authentication.JwtBearer;
////using Microsoft.Extensions.DependencyInjection;
////using Microsoft.Extensions.FileProviders;
////using Microsoft.IdentityModel.Tokens;
////using Microsoft.OpenApi.Models;
////using System.Text;
//////using AutoMapper;
////using Bl;
////using Bl.Api;
////using Bl.Services;
////using Dal.Api;
////using Dal.Models;
////using Dal.Services;
////using Microsoft.EntityFrameworkCore;
////using Microsoft.Extensions.DependencyInjection;
////using Microsoft.Extensions.FileProviders;
////using System;
////using Microsoft.Extensions.FileProviders;

////namespace Pl_Web_Api
////{
////    public class Program
////    {
////        public static void Main(string[] args)
////        {
////            var builder = WebApplication.CreateBuilder(args);

////            // קביעת הגדרות השירותים
////            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
////                .AddJwtBearer(options =>
////                {
////                    options.TokenValidationParameters = new TokenValidationParameters
////                    {
////                        ValidateIssuer = true,
////                        ValidateAudience = true,
////                        ValidateLifetime = true,
////                        ValidateIssuerSigningKey = true,
////                        ValidIssuer = "yourIssuer",  // הגדרת המנפיק (issuer)
////                        ValidAudience = "yourAudience",  // הגדרת הקהל (audience)
////                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("yourSecretKey"))  // המפתח הסודי (תשמור עליו בסוד!)
////                    };
////                });

////            // הוספת שירותי API ו-Swagger
////            builder.Services.AddControllers();
////            builder.Services.AddEndpointsApiExplorer();
////            builder.Services.AddSwaggerGen(c =>
////            {
////                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pl_Web_Api", Version = "v1" });
////            });

////            // הגדרת CORS
////            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
////            builder.Services.AddCors(options =>
////            {
////                options.AddPolicy(name: MyAllowSpecificOrigins,
////                    policy =>
////                    {
////                        policy.WithOrigins("http://localhost:4200") // אפשר גישה רק לכתובת הזו
////                              .AllowAnyHeader()
////                              .AllowAnyMethod();
////                    });
////            });

////            // יצירת היישום
////            var app = builder.Build();

////            // קונפיגורציה עבור Swagger UI בפיתוח
////            if (app.Environment.IsDevelopment())
////            {
////                app.UseSwagger();
////                app.UseSwaggerUI(c =>
////                {
////                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pl_Web_Api v1");
////                });
////            }

////            // הוספת תמיכה בפרוטוקול https
////            app.UseHttpsRedirection();

////            // הפעלת שימוש בהגדרות קורס
////            app.UseCors(MyAllowSpecificOrigins);

////            // הפעלת Authorization ו-Authentication (אימות)
////            app.UseAuthentication();
////            app.UseAuthorization();

////            // הגדרת Static Files - תיקיית Uploads
////            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
////            app.UseStaticFiles(new StaticFileOptions
////            {
////                FileProvider = new PhysicalFileProvider(uploadsPath),
////                RequestPath = "/Uploads"
////            });

////            // קביעת מסלול לשליחה של Controllers
////            app.MapControllers();

////            // הרצת היישום
////            app.Run();



////        }
////    }
////}



//using AutoMapper;
//using Bl;
//using Bl.Api;
//using Bl.Services;
//using Dal.Api;
//using Dal.Models;
//using Dal.Services;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.FileProviders;
//using System;
//using Microsoft.Extensions.FileProviders;

//namespace Pl_Web_Api
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {

//            //var config = new MapperConfiguration(cfg =>
//            //{
//            //    cfg.CreateMap<BlCustomer, CustomersTbl>();

//            //});
//            //m = config.CreateMapper();

//            var builder = WebApplication.CreateBuilder(args);

//            //        builder.Services.AddDbContext<DbManager>(options =>
//            //options.UseSqlServer("YourConnectionStringHere"));

//            //        builder.Services.AddScoped<IVerificationCodes, VerificationCodesService>();
//            //builder.Services.AddScoped<VerificationCodeBL>();

//            // Add services to the container.
//            builder.Services.AddSingleton<IBl, BlManager>();// new blmanager

//            builder.Services.AddControllers();
//            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//            builder.Services.AddEndpointsApiExplorer();
//            builder.Services.AddSwaggerGen();
//            var MyAllowSpecificOrigins = "myAllowSpecificOrigins";

//            builder.Services.AddCors(options =>
//            {
//                options.AddPolicy(name: MyAllowSpecificOrigins,
//                    builder =>
//                    {
//                        builder.WithOrigins("http://localhost:4200")
//                               .AllowAnyHeader()
//                               .AllowAnyMethod();
//                    });
//            });

//            var app = builder.Build();

//            /////////////////////
//            ///





//            //var builder = WebApplication.CreateBuilder(args);


//            // Configure the HTTP request pipeline.
//            if (app.Environment.IsDevelopment())
//            {
//                app.UseSwagger();
//                app.UseSwaggerUI();
//            }

//            app.UseHttpsRedirection();

//            app.UseAuthorization();
//            ///////
//            app.UseStaticFiles();
//            app.UseRouting();
//            app.UseCors("myAllowSpecificOrigins");
//            /////
//            ///


//            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

//            app.UseStaticFiles(new StaticFileOptions
//            {
//                FileProvider = new PhysicalFileProvider(uploadsPath),
//                RequestPath = "/Uploads"
//            });
//            app.MapControllers();
//            app.UseStaticFiles();
//            app.Run();


//        }
//    }
//}


//using AutoMapper;
//using Bl;
//using Bl.Api;
//using Bl.Services;
//using Dal.Api;
//using Dal.Models;
//using Dal.Services;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.FileProviders;
//using QuestPDF.Infrastructure;

//var builder = WebApplication.CreateBuilder(args);

//// Add DbContext correctly (Scoped by default)
//builder.Services.AddDbContext<DbManager>(options =>
//    options.UseSqlServer("Server=localhost;Database=YourDatabase;Trusted_Connection=True;"));

//// Add services
//builder.Services.AddScoped<IBl, BlManager>();
////builder.Services.AddSingleton<IBl, BlManager>();
////builder.Services.AddScoped<OpenAiService>();
////builder.Services.AddScoped<IPosts, PostsService>();

//QuestPDF.Settings.License = LicenseType.Community; // או Evaluation

//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var MyAllowSpecificOrigins = "myAllowSpecificOrigins";
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: MyAllowSpecificOrigins,
//        policy =>
//        {
//            policy.WithOrigins("https://localhost:4200")
//                  .AllowAnyHeader()
//                  .AllowAnyMethod();
//        });
//});

//// Configure Kestrel to listen on specific port
//builder.WebHost.UseUrls("https://localhost:7006");

//var app = builder.Build();

//// Swagger always available in development
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pl_Web_Api v1");
//    });
//}

//app.UseHttpsRedirection();
//app.UseRouting();
//app.UseCors(MyAllowSpecificOrigins);
//app.UseAuthorization();

//// Static files – wwwroot
//app.UseStaticFiles();

//// Static files – Uploads folder
//var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(uploadsPath),
//    RequestPath = "/Uploads"
//});

//// Map controllers
//app.MapControllers();

//// Run the application
//app.Run();






using AutoMapper;
using Bl;
using Bl.Api;
using Bl.Services;
using Dal.Api;
using Dal.Models;
using Dal.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders;
using QuestPDF.Infrastructure;
using System;

namespace Pl_Web_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //var config = new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<BlCustomer, CustomersTbl>();

            //});
            //m = config.CreateMapper();

            var builder = WebApplication.CreateBuilder(args);

            // Add DbContext correctly (Scoped by default)
            //builder.Services.AddDbContext<DbManager>(options =>
            //    options.UseSqlServer("Server=localhost;Database=YourDatabase;Trusted_Connection=True;"));

            //        builder.Services.AddDbContext<DbManager>(options =>
            //options.UseSqlServer("YourConnectionStringHere"));

            //        builder.Services.AddScoped<IVerificationCodes, VerificationCodesService>();
            //builder.Services.AddScoped<VerificationCodeBL>();

            // Add services to the container.
            builder.Services.AddHttpClient<OpenAiHandwritingService>();
            builder.Services.AddScoped<IBl, BlManager>();// new blmanager
            builder.Services.AddScoped<OpenAiService>();
            QuestPDF.Settings.License = LicenseType.Community; // או Evaluation
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var MyAllowSpecificOrigins = "myAllowSpecificOrigins";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });
            builder.WebHost.UseUrls("https://localhost:7006");


            var app = builder.Build();



            //var builder = WebApplication.CreateBuilder(args);


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            ///////
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("myAllowSpecificOrigins");
            /////
            ///


            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(uploadsPath),
                RequestPath = "/Uploads"
            });
            app.MapControllers();
            app.UseStaticFiles();
            app.Run();


        }
    }
}