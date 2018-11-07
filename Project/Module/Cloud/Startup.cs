using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Module.Cloud
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCloudDataProtection(
            this IServiceCollection services, bool skipInDevelopmentMode)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IConfiguration>();
            var environment = serviceProvider.GetService<IHostingEnvironment>();

            if (skipInDevelopmentMode && environment.IsDevelopment())
            {
                // skip               
            }
            else
            { 
                services.AddDataProtection()
                  .SetApplicationName(configuration["DataProtection:ApplicationName"])
                  .PersistKeysToAzureBlobStorage(new Uri(configuration["DataProtection:CloudStorageUrl"]))
                  //.PersistKeysToFileSystem(new System.IO.DirectoryInfo(@"C:\keatkeat\projects\hkllimmotorsport-two\dotnetcore\Project\wwwroot")) // for 第一次 generate 然后 upload to Azure Blob
                  .ProtectKeysWithAzureKeyVault(configuration["DataProtection:CloudKeyUrl"], configuration["Cloud:AppClientId"], configuration["Cloud:AppClientSecret"]);
            }
            return services;
        }
    }

    public static class IWebHostBuilderExtensions
    {
        public static IWebHostBuilder ConfigureSecretFromCloud(
            this IWebHostBuilder builder, bool skipInDevelopmentMode)
        {
            builder.ConfigureAppConfiguration((context, configBuilder) =>
            {
                var config = configBuilder.Build();
                IConfiguration secretConfig;
                if (skipInDevelopmentMode && context.HostingEnvironment.IsDevelopment())
                {
                    // skip
                    secretConfig = config;
                }
                else
                {
                    var keyVaultConfigBuilder = new ConfigurationBuilder();
                    keyVaultConfigBuilder.AddAzureKeyVault(
                        $"https://{config["Cloud:KeyVault"]}.vault.azure.net/",
                        config["Cloud:AppClientId"],
                        config["Cloud:AppClientSecret"]);
                    secretConfig = keyVaultConfigBuilder.Build();
                }
                var mergeConfig = new Dictionary<string, string>
                {
                    ["Email:Password"] = secretConfig["EmailPassword"],
                    ["ConnectionStrings:DefaultConnection"] = string.Format(config["ConnectionStrings:DefaultConnection"], secretConfig["SqlPassword"] ),
                    ["IdentityServerSelfSignedCertificatePfxPassword"] = secretConfig["IdentityServerSelfSignedCertificatePfxPassword"]                   
                };
                var mergeConfigBuilder = new ConfigurationBuilder();
                mergeConfigBuilder.AddInMemoryCollection(mergeConfig);
                configBuilder.AddConfiguration(mergeConfigBuilder.Build());
            });
            return builder;
        }
    }
}
