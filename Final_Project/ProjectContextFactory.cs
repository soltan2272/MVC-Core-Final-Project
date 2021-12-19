using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project
{
    public class ProjectContextFactory : IDesignTimeDbContextFactory<Project_Context>
    {
        public Project_Context CreateDbContext(string[] args)
        {
            IConfigurationRoot SettingsObj
                = new ConfigurationBuilder().SetBasePath
                (Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            DbContextOptionsBuilder<Project_Context>
                 Builder
                 = new DbContextOptionsBuilder<Project_Context>();
            Builder.UseSqlServer(SettingsObj.GetConnectionString("KaraKeep"));

            Project_Context Context
                = new Project_Context(Builder.Options);
            return Context;


        }
    }
}
