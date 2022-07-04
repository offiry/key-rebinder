using Application.Contracts;
using Application.Handlers.Generic;
using Application.Handlers.Start;
using Bootstrapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppPresentation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            IServiceProvider serviceProvider = serviceCollection.RegisterAutofac();
            var mediatr = (IMediator)serviceProvider.GetService(typeof(IMediator));
            var initialSqlLiteDatabase = (IInitialSqlLiteDatabase)serviceProvider.GetService(typeof(IInitialSqlLiteDatabase));
            
            await Task.Delay(5000);


            //GenericTest(mediatr);

            await mediatr.Send(new StartGameBindingsQuery { GameId = 1, WindowGameName = "Company Of Heroes", GameName = "Company Of Heroes Relaunch" }, default(CancellationToken));
            Console.ReadLine();
        }

        private static void GenericTest(IMediator mediator)
        {
            var commandDataRequestDto = new GenericRequest
            {
                Command = typeof(GenericRequest),
                SenderGuid = "1",
                ServiceGuid = "2"
            };

            var serialized = JsonConvert.SerializeObject(commandDataRequestDto);
            var de_serialized = JsonConvert.DeserializeObject<GenericRequestBase>(serialized);
            var type = de_serialized.Command;
            var de_serialized2 = JsonConvert.DeserializeObject(serialized, type);

            mediator.Send(de_serialized2);
        }
    }
}
