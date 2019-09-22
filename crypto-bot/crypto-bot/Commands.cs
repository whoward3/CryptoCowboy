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
        protected Random randomSeed = new Random();

        public static string[] facts = new string[] {"In the past two legislative sessions, Wyoming lawmakers have passed 13 new blockchain laws!"};

        [Command("e")]
        public async Task SHOW_EXCHANGES()
        {
            try
            {
                Console.WriteLine("**LOGFILE: USER: " + Context.User.Username + ", <SHOW EXCHANGES MODULE>");
                var user = Context.User as SocketGuildUser;                
                Cryptowatch cryptowatch = new Cryptowatch();
                var res = await cryptowatch.GetExchanges();
                if (res.Count > 0)
                {
                    res.Sort();
                    string output = "```";
                    for (int i = 0; i < res.Count; i++)
                    {
                        output = output + res[i].ToString().Replace(" ", "-") + "\n";
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
                //Sanitize Input for " "
                paramInput = paramInput.Replace(" ", "-");
                var res = await cryptowatch.GetMarket(paramInput);
                if (res.Count > 0)
                {
                    res.Sort();
                    string output = "```";
                    for (int i = 0; i < res.Count; i++)
                    {
                        output = output + res[i].ToString().ToUpper().Replace(" ","-") + "\n";
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

        [Command("o")]
        public async Task SHOW_OFFER([Remainder] string paramInput = "0")
        {
            try
            {
                Console.WriteLine("**LOGFILE: USER: " + Context.User.Username + ", <SHOW OFFER MODULE>");
                var user = Context.User as SocketGuildUser;
                Cryptowatch cryptowatch = new Cryptowatch();
                var @params = paramInput.Split(' ');
                string market = @params[0]; string pair = @params[1];
                var res = await cryptowatch.GetOffer(market,pair);
                if (res.Count > 5)
                {
                    /*Result Structure
                Exchange(0) | pair(1)
                    "Offer Volume:" volume(2) | "% Change:" change(3)
                            | "Last Price:" last(4) 
                            | "Max Price:" max(5)
                            | "Min Price:" min(6)
                 */
                    await Context.Channel.SendMessageAsync("__Detailed Offer Information For "+pair.ToUpper()+" Currency Exchanges From The "+market+" Exchange__");
                    await Context.Channel.SendMessageAsync("```"+res[0]+" | "+res[1]+"\n\t"+"Volume: "+ Math.Round((double)res[2],2)+" | "+"Change:"+Math.Round((double)res[3], 2)+"%\n\t | "+"Last Price:"+res[4]+"\n\t | "+"High Price: "+res[5]+ "\n\t | " + "Low Price: " + res[6]+"```");
                }
                else
                {
                    await Context.Channel.SendMessageAsync("I couldn't find detailed offer information for that offer and exchange, please recheck the exchange name and offer pair you gave me.");
                }
            }
            catch (Exception a)
            {
                Console.Write("+++SHOW MARKETS ERROR: CRASH: " + a);
            }
        }

        [Command("Wyoming Blockchain")]
        public async Task LEARN_WB()
        {
            try
            {
                Console.WriteLine("**LOGFILE: USER: " + Context.User.Username + ", <SHOW WB MODULE>");
                var user = Context.User as SocketGuildUser;
                await Context.Channel.SendMessageAsync("First of all, what is Blockchain Technology? Blockchain stores data, like transaction, sender, receiver, etc., in a block as a hash. Just like a cubical, it has different sides. The front side stores the previous hash, the backside stores hash for the current block. Wyoming is the only state in the U.S. that has laws set up for Blockchain. What a great opportunity! ");
            }
            catch (Exception a)
            {
                Console.Write("+++SHOW WB ERROR: CRASH: " + a);
            }
        }

        [Command("!")]
        public async Task LEARN_RANDOM()
        {
            try
            {
                Console.WriteLine("**LOGFILE: USER: " + Context.User.Username + ", <LEARN MODULE>");
                var user = Context.User as SocketGuildUser;
                int i = randomSeed.Next(facts.Length);
                await Context.Channel.SendMessageAsync("**Did you know:**\n"+facts[i]);            
            }
            catch (Exception a)
            {
                Console.Write("+++SHOW WB ERROR: CRASH: " + a);
            }
        }

    }
}