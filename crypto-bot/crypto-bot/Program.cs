using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace crypto_bot
{
    class Program
    {
        //Discord Session Variables
        private CommandService commands;
        private DiscordSocketClient client;
        private IServiceProvider services;
        private string BOT_TOKEN = "";

        static void Main(string[] args) => new Program().RunSystem().GetAwaiter().GetResult();

        public async Task RunSystem()
        {
            var _config = new DiscordSocketConfig { AlwaysDownloadUsers = true };
            client = new DiscordSocketClient(_config);
            var config = new CommandServiceConfig { DefaultRunMode = RunMode.Async };
            commands = new CommandService(config);
            services = new ServiceCollection()
                .AddSingleton(client)
                .AddSingleton(commands)
                .BuildServiceProvider();

            //event subscriptions
            client.Log += Log;

            await RegisterCommandsAsync();
            await client.LoginAsync(TokenType.Bot, BOT_TOKEN);
            await client.StartAsync();
            await client.SetGameAsync(name: "WyoHackathon");
            await Task.Delay(-1);
        }

        public async Task RegisterCommandsAsync()
        {
            client.MessageReceived += HandleCommandAsync;
            await commands.AddModulesAsync(Assembly.GetEntryAssembly(), services);
        }

        public async Task HandleCommandAsync(SocketMessage msgParam)
        {
            var msg = msgParam as SocketUserMessage;
            if (msg == null) return;

            int argPos = 0;
            if (msg.HasMentionPrefix(client.CurrentUser, ref argPos))
            {
                    var context = new SocketCommandContext(client, msg);
                    var result = await commands.ExecuteAsync(context, argPos, services);
                    if (!result.IsSuccess)
                    {
                        Console.WriteLine(result.ErrorReason);
                    }
            }
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);

            return Task.CompletedTask;
        }
    }
}
