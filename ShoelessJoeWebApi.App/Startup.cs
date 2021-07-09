using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ShoelessJoeWebApi.App.Helpers;
using ShoelessJoeWebApi.Core.Interfaces;
using ShoelessJoeWebApi.DataAccess.DataModels;
using ShoelessJoeWebApi.DataAccess.Services;

namespace ShoelessJoeWebApi.App
{
    public class Startup
    {
        private readonly static string _corsPolicy = "ShoelessJoePolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //Database connection
            services.AddDbContext<ShoelessdevContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SholessJoe")));
            //Dependency Injection
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IFriendService, FriendService>();
            services.AddScoped<IManufacterService, ManufacterService>();
            services.AddScoped<IModelService, ModelService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IReplyService, ReplyService>();
            services.AddScoped<ISchoolService, SchoolServcie>();
            services.AddScoped<IShoeService, ShoeService>();
            services.AddScoped<IShoeImageService, ShoeImageService>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<IUserService, UserService>();
            //Swagger
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ShoelessJoe Api",
                    Description = "An Api for ShoelessJoe.com"
                });
            });
            //CORS
            services.AddCors(options =>
            {
                options.AddPolicy(_corsPolicy, builder =>
                    builder.WithOrigins("https://localhost:44303")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Swagger
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShoelessJoe V1");
            });
            //CORS
            app.UseCors(_corsPolicy);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<JwtMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
