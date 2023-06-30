using AutoMapper;
using Confluent.Kafka;
using kafka_project_helper_library.Middleware;
using kafka_project_order_webapi.Profiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace kafka_project_order_webapi
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
            // var producerConfig = new ProducerConfig();
            // Configuration.Bind("ProducerConfig", producerConfig);
            // services.AddSingleton<ProducerConfig>(producerConfig);
            services.RegisterKafkaProducer(Configuration);

            services.RegisterOrdersDatabase();

            services.AddAutoMapper(typeof(OrderProfile));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
