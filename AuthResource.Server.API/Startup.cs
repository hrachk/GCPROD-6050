using AuthResource.Server.API.Models;
using AuthTutorial.Auth.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AuthResource.Server.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.   
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            var authOptions = Configuration.GetSection("Auth").Get<AuthOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.RequireHttpsMetadata = false; //RequireHttpsMetadata - (if false) then  allows to validate a token received via http, (if true) then https in real server make true
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true, // indicates`  whether the publisher will be validated when validating the token
                        ValidIssuer = authOptions.Issuer, //a string representing the publisher

                        ValidateAudience = true, //indicates` whether the consumer of the token will be validated
                        ValidAudience = authOptions.Audience, //token consumer setting

                        ValidateLifetime = true, //whether the lifetime of the token will be validated

                        IssuerSigningKey = authOptions.GetSymmetricSecurityKey(), // HS256  //Installing the Security Key
                        ValidateIssuerSigningKey = true //indicates` Whether the specified security key will be validated
                    };              
                                
                });


            services.AddCors(options => {

            options.AddDefaultPolicy(builder=>{

                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();  
                
                });   
                
            });

            services.AddSingleton(new CustomerHistory());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuthResource.Server.API", Version = "v1.0" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthResource.Server.API v1.0"));
            }

            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                // configure Pipeline
                endpoints.MapControllers();
            });
        }
    }
}
