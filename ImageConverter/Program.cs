using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommandDotNet;
using CommandDotNet.DataAnnotations;
using CommandDotNet.IoC.MicrosoftDependencyInjection;
using ImageConverter.Readers;
using ImageConverter.Writers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ImageConverter
{
    class Program
    {
        static int Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IApp, App>();
                    services.AddTransient<IReadStrategy, ReadStrategy>();
                    services.AddTransient<IWriteStrategy, WriteStrategy>();
                    AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(s => s.GetTypes())
                        .Where(p => typeof(IReader).IsAssignableFrom(p) && !p.IsInterface)
                        .ToList()
                        .ForEach(t => services.AddTransient(typeof(IReader), t));
                    AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(s => s.GetTypes())
                        .Where(p => typeof(IWriter).IsAssignableFrom(p) && !p.IsInterface)
                        .ToList()
                        .ForEach(t => services.AddTransient(typeof(IWriter), t));
                }).Build();

            return new AppRunner<IApp>()
                .UseDataAnnotationValidations()
                .UseMicrosoftDependencyInjection(host.Services).Run(args);
        }
    }
}
