using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CM.Data;
using CM.DTOs.Mappers.Contracts;
using CM.DTOs.Mappers;
using CM.Models;
using CM.Services;
using CM.Services.Contracts;
using CM.Services.Providers.Contracts;
using CM.Services.Providers;
using CM.Web.Providers.Contracts;
using CM.Web.Providers.ViewModelMappers;
using CM.Web.Providers;
using CM.Web.Middlewares;

namespace CM.Web
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
            services.AddDbContext<CMContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<AppUser, AppRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<CMContext>()
                .AddDefaultUI() // Consider commenting out this as it was missing in master
                .AddDefaultTokenProviders();

            services.AddControllersWithViews();
            services.AddRazorPages();


            services.AddScoped<IAddressMapper, AddressMapper>();
            services.AddScoped<IBarMapper, BarMapper>();
            services.AddScoped<ICocktailMapper, CocktailMapper>();
            services.AddScoped<IIngredientMapper, IngredientMapper>();
            services.AddScoped<IUserMapper, UserMapper>();
            services.AddScoped<ICommentViewMapper, CommentViewMapper>();
            services.AddScoped<IRatingViewMapper, RatingViewMapper>();
            services.AddScoped<IIngredientViewMapper, IngredientViewMapper>();
            services.AddScoped<ICocktailViewMapper, CocktailViewMapper>();
            services.AddScoped<IBarViewMapper, BarViewMapper>();

            services.AddScoped<IAddressServices, AddressServices>();
            services.AddScoped<IBarServices, BarServices>();
            services.AddScoped<ICocktailServices, CocktailServices>();
            services.AddScoped<IIngredientServices, IngredientServices>();
            services.AddScoped<IRatingServices, RatingServices>();
            services.AddScoped<ICommentServices, CommentServices>();

            services.AddScoped<IStorageProvider, AppStorageProvider>();
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();

            services.AddMvc().AddNToastNotifyNoty();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<MissingMiddleware>();

            app.UseNToastNotify();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}