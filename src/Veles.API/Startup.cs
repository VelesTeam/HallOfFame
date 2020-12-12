namespace Veles.API
{
   using Microsoft.AspNetCore.Builder;
   using Microsoft.AspNetCore.Hosting;
   using Microsoft.Extensions.Configuration;
   using Microsoft.Extensions.DependencyInjection;
   using Microsoft.Extensions.Hosting;
   using Veles.Infrastructure.Authentication.Extensions;
   using Veles.Infrastructure.CQRS;
   using Veles.Infrastructure.Mongo;

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
         services.AddControllers();
         services.AddMemoryCache();
         services.AddCQRS();
         services.AddMongoDB(Configuration);
         services.AddInternalAuthentication(Configuration);
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if(env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }

         app.UseHttpsRedirection();

         app.UseRouting();

         app.UseAuthorization();
         app.UseAuthentication();

         app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
      }
   }
}