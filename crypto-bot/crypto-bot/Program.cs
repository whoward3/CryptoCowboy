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
        private readonly string BOT_TOKEN = "";

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
            await client.SetGameAsync(name: "WyoHackathon 2019");
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
                        if (result.ErrorReason.Contains("Unknown command.") && !context.User.IsBot)
                        {
                        Console.WriteLine("**LOGFILE: USER: " + context.User.Username + ", <HELP MODULE>");
                        await context.Channel.SendMessageAsync("Howdy " + context.User.Mention + "! I'm CryptoCowboy and I'm here to help you integrate with cryptocurrencies using Cryptowatch by Kraken. Cryptowatch is a platform for the cryptocurrency markets.", true);
                        await context.Channel.SendMessageAsync("It provides a powerful interface to scan prices, analyze market movements, and make trades on every major exchange. For more information, please visit https://cryptowat.ch/about",true);
                        await context.Channel.SendMessageAsync("\n To get you started I've listed out some of my commands below. Go :cowboy:'s!\n \n", true);
                        string modules = ("__*CryptoCowboy Basic Commands*__\n(1) If you want to see __all available Exchanges__ just type in ``@CryptoCowboy E``\n(2) To see view the __Market for an Exchange__ just type in ``@CryptoCowboy M [Exchange Name]``\n(3) To display the __details of an Exchange Offer__ ``@CryptoCowboy O [Exchange Name] [Offer Name]``\n(4) Interested in the __Blockchain business in Wyoming__? Just type ``@CryptoCowboy Wyoming Blockchain`` for details!\n(5) Want to __learn something interesting__ about the Blockchain? Type ``@CryptoCowboy !`` to learn something new!");
                        await context.Channel.SendMessageAsync(modules);
                        }
                        else
                        {
                        Console.WriteLine(result.ErrorReason);
                        }
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
