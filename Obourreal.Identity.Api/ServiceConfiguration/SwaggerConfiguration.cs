using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Obourreal.Identity.Api.ServiceConfiguration
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen();
            return services;
        }

        public static IApplicationBuilder UseSwaggerGen(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}