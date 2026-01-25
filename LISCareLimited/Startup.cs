using LISCare.Implementation;
using LISCare.Interface;
using LISCareBussiness.Implementation;
using LISCareBussiness.Interface;
using LISCareDataAccess.LISCareDbContext;
using LISCareDTO;
using LISCareReporting.LISBarcode;
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
using Microsoft.OpenApi.Models;
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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // =========================
            // Dependency Injection
            // =========================
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
            services.AddScoped<IProfile, ProfileBAL>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IAnalyzer, AnalyzerBAL>();
            services.AddScoped<IAnalyzerRepository, AnalyzerRepository>();
            services.AddScoped<ICenter, CenterBAL>();
            services.AddScoped<ICenterRepository, CenterRepository>();
            services.AddScoped<IClinc, ClinicBAL>();
            services.AddScoped<IClinicRepository, ClinicRepository>();
            services.AddScoped<IClient, ClientBAL>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IOutLab, OutLabBAL>();
            services.AddScoped<IOutLabRepository, OutLabRepository>();
            services.AddScoped<IEmployee, EmployeeBAL>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IBarcode, BarcodeBAL>();
            services.AddScoped<IBarcodeRepository, BarCodeRepository>();
            services.AddScoped<BulkBarcodeGenerator>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProject, ProjectBAL>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IPatient, PatientBAL>();
            services.AddScoped<IAccessionRepository, AccessionRepository>();
            services.AddScoped<IAccession, AccessionBAL>();
            services.AddScoped<IReporting, ReportingBAL>();
            services.AddScoped<IReportingRepository, ReportingRepository>();

            // =========================
            // CORS
            // =========================
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder =>
                {
                    builder.WithOrigins(
                            "http://localhost:4200",
                            "https://dev-lis-care-web-crb9euhzd7d0ezb8.centralindia-01.azurewebsites.net")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });

            // =========================
            // Database
            // =========================
            services.AddDbContext<LISCareDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString(ConstantResource.LISCareDbConnection)));

            // =========================
            // App Settings
            // =========================
            services.Configure<UploadImagePath>(
                Configuration.GetSection("UploadImagePath"));

            var tokenSection = Configuration.GetSection(ConstantResource.TokenModel);
            services.Configure<TokenModel>(tokenSection);

            var tokenSettings = tokenSection.Get<TokenModel>();
           // var key = Encoding.ASCII.GetBytes(tokenSettings.Secret);

            var key = Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),

                        ValidateIssuer = false,     // OK for now
                        ValidateAudience = false,   // OK for now

                        ClockSkew = TimeSpan.Zero
                    };

                    // Optional but VERY useful for debugging
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine("JWT ERROR: " + context.Exception.Message);
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization();


            // 🔥 REQUIRED FOR [Authorize]
            services.AddAuthorization();

            // =========================
            // Swagger + JWT
            // =========================
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Enter: Bearer {your JWT token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
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

            app.UseRouting();

            app.UseCors("AllowSpecificOrigin"); // This must be placed between UseRouting and UseEndpoints

            app.UseAuthentication();    // 🔑 Identify user
            app.UseAuthorization();     // ✅ REQUIRED for [Authorize]

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
