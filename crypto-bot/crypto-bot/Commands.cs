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
        [Command("e")]
        public async Task SHOW_EXCHANGES()
        {
            try
            {
                Console.WriteLine("**LOGFILE: USER: " + Context.User.Username + ", <SHOW EXCHANGES MODULE>");
                var user = Context.User as SocketGuildUser;                
                Cryptowatch cryptowatch = new Cryptowatch();
                var res = await cryptowatch.getExchanges();
                if (res.Count > 0)
                {
                    res.Sort();
                    string output = "```";
                    for (int i = 0; i < res.Count; i++)
                    {
                        output = output + res[i] + "\n";
                    }
                    await Context.Channel.SendMessageAsync("__The current available exchanges are:__");
                    await Context.Channel.SendMessageAsync(output + "```");
                }
                else
                {
                    await Context.Channel.SendMessageAsync("I couldn't find any available exchanges, please check back later.");
                }
            }
            catch (Exception a)
            {
                Console.Write("+++SHOW EXCHANGES ERROR: CRASH: " + a);
            }
        }

        [Command("m")]
        public async Task SHOW_MARKETS([Remainder] string paramInput = "0")
        {
            try
            {
                Console.WriteLine("**LOGFILE: USER: " + Context.User.Username + ", <SHOW MARKETS MODULE>");
                var user = Context.User as SocketGuildUser;
                Cryptowatch cryptowatch = new Cryptowatch();
                var res = await cryptowatch.getMarket(paramInput);
                if (res.Count > 0)
                {
                    res.Sort();
                    string output = "```";
                    for (int i = 0; i < res.Count; i++)
                    {
                        output = output + res[i].ToString().ToUpper() + "\n";
                    }
                    await Context.Channel.SendMessageAsync("__The "+paramInput+" exchange is offering:__");
                    await Context.Channel.SendMessageAsync(output + "```");
                }
                else
                {
                    await Context.Channel.SendMessageAsync("I couldn't find any market information about that exchange, please recheck the exchange name you gave me.");
                }
            }
            catch (Exception a)
            {
                Console.Write("+++SHOW MARKETS ERROR: CRASH: " + a);
            }
        }
       
    }
}