using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypto_bot
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("hi")]
        public async Task Greeting()
        {
            try
            {
                var user = Context.User as SocketGuildUser;
                await Context.Channel.SendMessageAsync("Hi! " + user.Username);
            }catch(Exception a)
            {
                Console.Write("+++GREETING ERROR: CRASH: " + a);
            }
       }
    }
}
