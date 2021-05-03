using Exchange.Contracts;
using Exchange.Core.Middlewares;
using Exchange.Data;
using Exchange.Models;
using Exchange.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Linq;
using Exchange.Api.Mappings;
using Exchange.Core.ExchangeRate.Currencies;

namespace Exchange.Api
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
            services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));

            var appSettings = services.BuildServiceProvider().GetRequiredService<IOptions<AppSettings>>();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(appSettings.Value.ConnectionStrings.SingleOrDefault(x => x.Name == "ExchangeDb").Value));
            StaticConnectionString.ConnectionString = appSettings.Value.ConnectionStrings.SingleOrDefault(x => x.Name == "ExchangeDb").Value;

            services.AddCors(o =>
            {
                o.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddTransient<UnitOfWork<AppDbContext>>();
            services.AddTransient<ILog, LogService>();
            services.AddAutoMapper(typeof(Maps));
            services.AddHttpClient();
            services.AddTransient<HttpService>();
            services.AddTransient<ICurrencyExchange, ProvinciaCurrencyExchange>();
            services.AddTransient<PurchaseLimitService>();
            services.AddTransient<PurchaseService>();
            services.AddTransient<UserService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddOpenApiDocument(config =>
            {
                config.Title = "Exchange Rate - VirtualMind";
            });
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddExchangeRate<USDRateProvider>(ISOCurrencyEnum.USD.ToString());
            services.AddExchangeRate<BRLRateProvider>(ISOCurrencyEnum.BRL.ToString());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<HttpStatusCodeExceptionMiddleware>();

            app.UseOpenApi();

            app.UseSwaggerUi3();

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
