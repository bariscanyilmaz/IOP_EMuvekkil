using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EMuvekkil.Mappings;
using EMuvekkil.Models;
using EMuvekkil.Models.Abstract;
using EMuvekkil.Models.Concrete;
using EMuvekkil.Services;
using EMuvekkil.Services.Abstract;
using EMuvekkil.Services.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Hangfire;
using Hangfire.SqlServer;
using System;
using Hangfire.Common;

namespace EMuvekkil
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var key = Encoding.ASCII.GetBytes(Configuration["Application:Secret"]);
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));


            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 6;

            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            var mapConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MessageProfile());
                mc.AddProfile(new DavaProfile());
                mc.AddProfile(new UserProfile());
                mc.AddProfile(new CompanyProfile());
                mc.AddProfile(new DocumentProfile());
                mc.AddProfile(new MasrafProfile());
                mc.AddProfile(new DavaStateProfile());
                mc.AddProfile(new ReportListProfile());
                mc.AddProfile(new NotificationProfile());
            });

            IMapper mapper = mapConfig.CreateMapper();
            services.AddHangfire(config => config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = System.TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                })
            );

            GlobalConfiguration.Configuration.UseSerializerSettings(new Newtonsoft.Json.JsonSerializerSettings{
                ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });

            services.AddHangfireServer();
            services.AddSingleton(mapper);
            services.AddTransient<IMailService, MailService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IReminderService, ReminderService>();
            
            services.AddTransient<INotificationRepository, EfNotificationRepository>();
            services.AddTransient<IMessageRepository, EfMessageRepository>();
            services.AddTransient<IDavaRepository, EfDavaRepository>();
            services.AddTransient<IMasrafRepository, EfMasrafRepository>();
            services.AddTransient<IDocumentRepository, EfDocumentRepository>();
            services.AddTransient<ICompanyRepository, EfCompanyRepository>();
            services.AddTransient<IDavaStateRepository, EfDavaStateRepository>();
            services.AddTransient<IEventRepository, EfEventRepository>();
            services.AddTransient<IEventUsersRepository, EfEventUsersRepository>();
            services.AddCors();
            services.AddMvc().AddJsonOptions(x=>x.SerializerSettings.ReferenceLoopHandling= Newtonsoft.Json.ReferenceLoopHandling.Ignore)
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        // In production, the Angular files will be served from this directory
        services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
public void Configure(IApplicationBuilder app, IHostingEnvironment env, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    CreateDefaultRoles(roleManager).Wait();
    CreateDefaultAdminUser(userManager).Wait();
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseSpaStaticFiles();
    app.UseAuthentication();

    app.UseCors(opt =>
        opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
    );

    app.UseMvc(routes =>
    {
        routes.MapRoute(
            name: "default",
            template: "{controller}/{action=Index}/{id?}");

        routes.MapRoute(
            name: "Api",
            template: "api/{controller=Dava}/{action=GetDavas}/{id?}"
        );

    });

    app.UseSpa(spa =>
    {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

        if (env.IsDevelopment())
        {
            spa.UseAngularCliServer(npmScript: "start");
        }
    });

}

public static async Task CreateDefaultRoles(RoleManager<IdentityRole> roleManager)
{
    var isExist = await roleManager.RoleExistsAsync("admin");
    if (isExist) return;
    await roleManager.CreateAsync(new IdentityRole("admin"));

    isExist = await roleManager.RoleExistsAsync("muvekkil");
    if (isExist) return;
    await roleManager.CreateAsync(new IdentityRole("muvekkil"));

    isExist = await roleManager.RoleExistsAsync("avukat");
    if (isExist) return;
    await roleManager.CreateAsync(new IdentityRole("avukat"));

}

public static async Task CreateDefaultAdminUser(UserManager<ApplicationUser> userManager)
{

    var defaultAdmin = await userManager.FindByEmailAsync("admin@iop.com.tr");
    if (defaultAdmin != null)
    {
        return;
    }
    defaultAdmin = new ApplicationUser() { UserName = "admin@iop.com.tr", Email = "admin@iop.com.tr", IdentityNumber = "11223344556", NameSurname = "Barış Can Yılmaz" };
    await userManager.CreateAsync(defaultAdmin, "admin123456");
    await userManager.AddToRoleAsync(defaultAdmin, Role.admin.ToString());
}

    }
}
