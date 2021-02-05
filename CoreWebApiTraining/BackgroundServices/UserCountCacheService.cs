using CoreWebApiTraining.AppSettings;
using CoreWebApiTraining.Core.Caching;
using CoreWebApiTraining.Core.Utility;
using CoreWebApiTraining.Data.Entities;
using CoreWebApiTraining.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreWebApiTraining.BackgroundServices
{
    public class UserCountCacheService : BackgroundService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly IOptions<EagerCacheSettings> settings;

        private readonly int minutesToCache;
        private readonly int refreshInterval;

        public UserCountCacheService(IServiceScopeFactory serviceScopeFactory, IOptions<EagerCacheSettings> settings)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.settings = settings;

            this.minutesToCache = settings.Value.minutesToCache;
            this.refreshInterval = settings.Value.refreshInterval;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                int count;
                using (var scope = this.serviceScopeFactory.CreateScope())
                {
                    IGenericRepository<Users> usersRepo = scope.ServiceProvider.GetRequiredService<IGenericRepository<Users>>();
                    count = await usersRepo.Count();

                    ICachedObjectProvider<int> cachedObjectProvider = scope.ServiceProvider.GetRequiredService<ICachedObjectProvider<int>>();
                    cachedObjectProvider.Set(Constants.TotalUserCount, count, TimeSpan.FromMinutes(this.minutesToCache));
                }

                
                await Task.Delay(TimeSpan.FromMinutes(this.refreshInterval), stoppingToken);
            }
        }
    }
}
