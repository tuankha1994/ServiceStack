using System;
using System.IO;
using Funq;
using ContactApp.ServiceInterface;
using ServiceStack;
using ServiceStack.Configuration;
using ServiceStack.Razor;
using ServiceStack.Text;
using ContactApp.ServiceModel.Data.Repositories;
using ContactApp.ServiceModel.Data.Models;
using System.Web.Configuration;
using ContactApp.ServiceModel.BusinessServices;

namespace ContactApp
{
    public class AppHost : AppHostBase
    {
        /// <summary>
        /// Base constructor requires a Name and Assembly where web service implementation is located
        /// </summary>
        public AppHost()
            : base("ContactApp", typeof(ContactServices).Assembly)
        {
            var customSettings = new FileInfo(@"~/appsettings.txt".MapHostAbsolutePath());
            AppSettings = customSettings.Exists
                ? (IAppSettings)new TextFileSettings(customSettings.FullName)
                : new AppSettings();
        }

        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        public override void Configure(Container container)
        {
            //Customize JSON Responses
            JsConfig.EmitLowercaseUnderscoreNames = true;
            JsConfig<Guid>.SerializeFn = guid => guid.ToString();
            JsConfig<TimeSpan>.SerializeFn = time =>
                (time.Ticks < 0 ? "-" : "") + time.ToString("hh':'mm':'ss'.'fffffff");

            //Register dependencies
            container.AddScoped<IDBContext>(c => new DBContext(WebConfigurationManager.AppSettings["ConnectionString"]));            
            container.AddScoped<IUnitOfWork, UnitOfWork>();
            container.AddScoped<IRepository<Contact>, Repository<Contact>>();
            container.AddScoped<IRepository<PhoneNumber>, Repository<PhoneNumber>>();
            container.AddScoped<IContactBusinessService, ContactBusinessService>();
            container.AddScoped<IContactDetailBusinessService, ContactDetailBusinessService>();


            //Config examples
            //this.Plugins.Add(new PostmanFeature());
            //this.Plugins.Add(new CorsFeature());

            SetConfig(new HostConfig
            {
                DebugMode = AppSettings.Get("DebugMode", true),
                AddRedirectParamsToQueryString = true
            });

            this.Plugins.Add(new RazorFormat());
        }
    }
}