using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using RiseAssessment.API.Client.Refit.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseAssessment.API.Client.Refit.Dependency
{
    public class RefitApiServiceDependency
    {
        #region fields

        private static RefitApiServiceDependency shared;
        private static object obj = new object();

        #endregion


        internal static IServiceCollection Services { get; set; }
        internal static IServiceProvider ServiceProvider { get; private set; }
        internal static IConfiguration Configuration { get; set; }

        private static RefitApiServiceDependency Shared
        {
            get
            {
                if (shared == null)
                {
                    lock (obj)
                    {
                        if (shared == null)
                        {
                            shared = new RefitApiServiceDependency();
                        }
                    }
                }

                return shared;
            }
        }



        private IContactApi _ContactApi { get => ServiceProvider.GetRequiredService<IContactApi>(); }

        private IDirectoryApi _DirectoryApi { get => ServiceProvider.GetRequiredService<IDirectoryApi>(); }


        //Exposed public static props via RefitApiServiceDependency.Shared 
        public static IContactApi ContactApi { get => Shared._ContactApi; }
        public static IDirectoryApi DirectoryApi { get => Shared._DirectoryApi; }

        private RefitApiServiceDependency()
        {
            if (Services == null)
                Services = new ServiceCollection();

            Init();
        }

        private void Init()
        {
            ConfigureServices(Services);
            ServiceProvider = Services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<AppSettings>();

            //APP SETTINGS
            var configBuilder = new ConfigurationBuilder().AddJsonFile("FactorySettings.json", optional: true);
            var config = configBuilder.Build();
            services.Configure<AppSettings>(config);

            var baseaddress = new Uri("https://localhost:44352/api/v1");

            services.AddRefitClient<IContactApi>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = baseaddress;
            });

            services.AddRefitClient<IDirectoryApi>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = baseaddress;
            });
        }
    }
}
