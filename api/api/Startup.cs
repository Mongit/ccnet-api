using api.Properties.Handlers;
using api.Properties.Handlers.Proveedores;
using BO;
using DAL;
using DAL.Proveedores;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using System.Reflection;
using System.Text;

namespace api
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private ILog GetLog()
        {
            ILoggerRepository logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            ILog logger = LogManager.GetLogger(typeof(Program));
            return logger;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            services.Add(new ServiceDescriptor(typeof(ILog), GetLog()));
            services.Add(new ServiceDescriptor(typeof(IDAL<Cliente>), new ClientesDAL(Configuration, "")));
            services.Add(new ServiceDescriptor(typeof(IClientesHandler), new ClientesHandler(Configuration, new ClientesDAL(Configuration, ""))));
            services.Add(new ServiceDescriptor(typeof(ICotizacionesHandler), new CotizacionesHandler(Configuration, new CotizacionesDAL(Configuration, ""), new PresupuestosDAL(Configuration, ""), new PresupuestosItemDAL(Configuration))));
            services.Add(new ServiceDescriptor(typeof(IPresupuestosHandler), new PresupuestosHandler(Configuration, new PresupuestosDAL(Configuration, ""), new PresupuestosItemDAL(Configuration))));
            services.Add(new ServiceDescriptor(typeof(IUsuariosHandler), new UsuariosHandler(new UsuariosDAL(Configuration))));
            services.Add(new ServiceDescriptor(typeof(IProveedoresHandler), new ProveedoresHandler(Configuration, new ProveedoresDAL(Configuration, ""))));
            services.Add(new ServiceDescriptor(typeof(ICuentasHandler), new CuentasHandler(Configuration, new CuentasDAL(Configuration, ""))));


            services.AddSingleton<IConfiguration>(Configuration);
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
