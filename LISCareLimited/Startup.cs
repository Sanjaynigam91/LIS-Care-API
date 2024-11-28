using LISCare.Implementation;
using LISCare.Interface;
using LISCareBussiness.Implementation;
using LISCareBussiness.Interface;
using LISCareDataAccess.LISCareDbContext;
using LISCareDTO;
using LISCareRepository.Implementation;
using LISCareRepository.Interface;
using LISCareReposotiory.Implementation;
using LISCareReposotiory.Interface;
using LISCareUtility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace LISCareLimited
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IUser, UserBAL>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMetaData, MetaDataBAL>();
            services.AddScoped<IMetaDataRepository, MetaDataRepository>();
            services.AddScoped<ILISRole, LISRoleBAL>();
            services.AddScoped<ILISRoleRepository, LISRoleRepository>();
            services.AddScoped<IGlobalRoleAccess, GlobalRoleAccessBAL>();
            services.AddScoped<IGlobalRoleAccessRepository, GlobalRoleAccessRepository>();
            services.AddScoped<ISampleCollection, SampleCollectionBAL>();
            services.AddScoped<ISampleCollectionRepository, SampleCollectionRepository>();
            services.AddScoped<ITestMgmt, TestMgmtBAL>();
            services.AddScoped<ITestMgmtRepository, TestMgmtRepository>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder
                    .WithOrigins("http://localhost:4200") // Allow requests from this origin
                    .AllowAnyMethod() // Allow all HTTP methods
                        .AllowAnyHeader() // Allow all headers
                );
            });

            // Configure database connection strings.
            services.AddDbContext<LISCareDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(ConstantResource.LISCareDbConnection)));

            services.Configure<UploadImagePath>(Configuration.GetSection("UploadImagePath"));

            // Configure strongly typed setting objects
            var appSettingsSection = Configuration.GetSection(ConstantResource.TokenModel);
            services.Configure<TokenModel>(appSettingsSection);
          
            services.AddSwaggerGen();
            // Configure jwt authentication
            var appSettings = appSettingsSection.Get<TokenModel>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
          
     
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(op => op.SwaggerEndpoint("/swagger/v1/swagger.json", "LIS Care API"));
            app.UseHttpsRedirection();          
            app.UseAuthentication();

            app.UseRouting();
            app.UseCors("AllowSpecificOrigin"); // This must be placed between UseRouting and UseEndpoints


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // Other endpoints mapping
            });

            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //        Path.Combine(Directory.GetCurrentDirectory(), ConstantResource.ACLImages, ConstantResource.Images)),
            //    RequestPath = ConstantResource.ImagePath
            //});
        }

    }
}
