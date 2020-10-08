﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Senparc.Ncf.XncfBase;
using Senparc.Xncf.XncfBuilder.Functions;
using Senparc.Xncf.XncfBuilder.Models.MultipleDatabase;
using System;

namespace Senparc.Xncf.XncfBuilder
{
    public partial class Register : IXncfDatabase
    {
        public const string DATABASE_PREFIX = "XncfBuilder";
        public string DatabaseUniquePrefix => DATABASE_PREFIX;

        //TODO：动态类型
        public Type XncfDatabaseDbContextType => typeof(XncfBuilderEntities_SqlServer);
        

        public void AddXncfDatabaseModule(IServiceCollection services)
        {
            services.AddScoped<Config>();
            services.AddScoped<BuildXncf.Parameters>();

            services.AddScoped<XncfBuilderEntities_SqlServer>();

            //AutoMap映射
            base.AddAutoMapMapping(profile =>
            {
                profile.CreateMap<Config, ConfigDto>();
                profile.CreateMap<ConfigDto, Config>();
                profile.CreateMap<BuildXncf.Parameters, Config>();
                profile.CreateMap<Config, BuildXncf.Parameters>();
            });
        }

        public void OnModelCreating(ModelBuilder modelBuilder)
        {
            //实现 [XncfAutoConfigurationMapping] 特性之后，可以自动执行，无需手动添加
            //modelBuilder.ApplyConfiguration(new DbConfig_WeixinUserConfigurationMapping());
        }
    }
}
