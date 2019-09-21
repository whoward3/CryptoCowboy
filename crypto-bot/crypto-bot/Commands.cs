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
        [Command("m")]
        public async Task SHOW_MARKETS()
        {
            try
            {
                Console.WriteLine("**LOGFILE: USER: " + Context.User.Username + ", <SHOW MARKETS MODULE>");
                var user = Context.User as SocketGuildUser;
                await Context.Channel.SendMessageAsync("__The current available markets are:__");
            }
            catch (Exception a)
            {
                Console.Write("+++SHOW MARKETS ERROR: CRASH: " + a);
            }
        }
        [Command("e")]
        public async Task SHOW_EXCHANGES()
        {
            try
            {
                Console.WriteLine("**LOGFILE: USER: " + Context.User.Username + ", <SHOW EXCHANGES MODULE>");
                var user = Context.User as SocketGuildUser;
                await Context.Channel.SendMessageAsync("__The current available exchanges are:__");
            }
            catch (Exception a)
            {
                Console.Write("+++SHOW EXCHANGES ERROR: CRASH: " + a);
            }
        }
    }
}
