using AutoMapper;
using CheckListApi.Models;
using CheckListWPF.MVVM.Model;
using CheckListWPF.MVVM.ViewModel;
using CheckListWPF.Resources;
using CheckListWPF.Resources.Helpers;
using CheckListWPF.Resources.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prism.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CheckListWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            AppHost = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
            {
                var config = new MapperConfiguration(myConfig =>
                {
                    myConfig.CreateMap<Canvas, CanvasDisplayModel>();
                    myConfig.CreateMap<CanvasDisplayModel, Canvas>();
                    myConfig.CreateMap<TaskBoard, TaskBoardDisplayModel>();
                    myConfig.CreateMap<TaskBoardDisplayModel, TaskBoard>();
                    myConfig.CreateMap<MyTask, TaskDisplayModel>();
                    myConfig.CreateMap<TaskDisplayModel, MyTask>();
                });

                var mapper = config.CreateMapper();

                services.AddSingleton(mapper);
                services.AddSingleton<MainWindow>();
                services.AddSingleton<IEventAggregator, EventAggregator>();
                services.AddSingleton<ILoggedInUser, LoggedInUser>();
                services.AddSingleton<IApiHelper, ApiHelper>();
                services.AddTransient<IDataModel, DataModel>();
                services.AddScoped<ICheckListEndpoint, CheckListEndpoint>();

            }).Build();
        }

        public static IHost? AppHost { get; private set; }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();
            var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
            startupForm!.Show();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnExit(e);
        }

    }
}
