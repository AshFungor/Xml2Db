using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using Unity.Microsoft.Logging;
using Xml2Db.Desktop.Views;
using Xml2Db.Models;

namespace Xml2Db.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            using (var context = Container.Resolve<Context>())
            {
                context.Database.EnsureCreated();
            }
            return new MainWindow();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ILoggerFactory loggerFactory = new LoggerFactory();
            loggerFactory.AddFile("log.txt", false);
            containerRegistry.GetContainer().AddExtension(new LoggingExtension(loggerFactory));

            containerRegistry.Register<Context>();
            //containerRegistry.RegisterInstance(new DbContextOptionsBuilder<Context>().UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Xml2Db;Integrated Security=True;").Options);
            containerRegistry.RegisterInstance(new DbContextOptionsBuilder<Context>().UseSqlite(File.ReadAllText("settings.txt")).Options);
        }
    }
}
