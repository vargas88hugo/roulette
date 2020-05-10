using System;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RouletteApi.Helpers;
using RouletteApi.Interfaces;
using RouletteApi.Models;
using RouletteApi.Repositories;
using RouletteApi.Services;

namespace RouletteApi
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
      services.Configure<Settings>(options =>
      {
        options.ConnectionString
            = Configuration.GetSection("MongoConnection:ConnectionString").Value;
        options.Database
            = Configuration.GetSection("MongoConnection:Database").Value;
      });
      services.AddTransient<IUserRepository, UserRepository>();
      services.AddTransient<IRouletteRepository, RouletteRepository>();
      services.AddTransient<IUserService, UserService>();
      services.AddTransient<IAuthService, AuthService>();
      services.AddTransient<IRouletteService, RouletteService>();
      services.AddTransient<IBetService, BetService>();
      services.AddControllers();
      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

      var appSettingsSection = Configuration.GetSection("AppSettings");
      services.Configure<AppSettings>(appSettingsSection);
      var appSettings = appSettingsSection.Get<AppSettings>();
      var key = Encoding.ASCII.GetBytes(appSettings.Secret);
      services.AddAuthentication(x =>
      {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(options => StartupHelper.ValidateToken(options, key));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
        app.UseDeveloperExceptionPage();

      app.UseHttpsRedirection();
      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();
      app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
  }
}
