using get_sales_result.Application.Interfaces;
using get_sales_result.Application.Services;
using get_sales_result.Infra;
using get_sales_result.WebApi.Middlewares;

namespace get_sales_result.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<InMemorySalesRepository>();
            services.AddScoped<IVehicleSaleService, VehicleSaleService>();
            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            return services;
        }
    }

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExecutionTimeMiddleware>();
            app.UseMiddleware<GlobalExceptionMiddleware>();
            return app;
        }
    }
}
