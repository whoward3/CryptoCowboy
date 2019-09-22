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

        public static string[] facts = new string[] {"In the past two legislative sessions, Wyoming lawmakers have passed 13 new blockchain laws!", "According to NewsWeek, Wyoming is the **CRYPTOCURRENCY CAPITAL** of the U.S.",
           "H.B.0070 is the bill that the State of Wyoming just introduced regarding blockchain", "The first paper about blockchain was published by Satoshi Nakamoto",
            "Satoshi Nakamoto is the name used by the pseudonymous person or persons who developed bitcoin", "_Investopedia_ thinks 'there are three people who might be the bitcoin founder': Dorian Nakamoto, Craig Wright, and Nick Szabo"};

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
                    await Context.Channel.SendMessageAsync("I couldn't find any available exchanges, please check back later.",true);
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
                    await Context.Channel.SendMessageAsync("I couldn't find any market information about that exchange, please recheck the exchange name you gave me.",true);
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
                    await Context.Channel.SendMessageAsync("```"+res[0]+" | "+res[1]+"\n\t"+"Volume: "+ Math.Round((double)res[2],2)+" | "+"Change: "+Math.Round((double)res[3], 2)+"%\n\t | "+"Last Price: "+res[4]+"\n\t | "+"High Price: "+res[5]+ "\n\t | " + "Low Price: " + res[6]+"```");
                }
                else
                {
                    await Context.Channel.SendMessageAsync("I couldn't find detailed offer information for that offer and exchange, please recheck the exchange name and offer pair you gave me.",true);
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
                await Context.Channel.SendMessageAsync("First of all, what is Blockchain Technology? According to Investopedia, blockchain is a chain of blocks and they store information about transactions like the sender/receiver, date/time, and amount of your most recent purchase. It's a secure way to store data as no one (except the owner who owns more than 50% of the chain) has access.\n \n Now that we know some basics, let's take a look at the business side! Wyoming is the only state in the U.S. that has laws set up for Blockchain. What a great opportunity!  Caitlin Long, a Wall Street veteran, has some great thoughts about it in her article _What Do Wyoming's 13 New Blockchain Laws Mean ? _. \n All of these factors make Wyoming a perfect place to start a blockchain business, for example, create a cryptocurrency economy class that teaches people how it works and the current markets and exchanges, a wind power company that generates electricity for miners, etc. ");
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
                await Context.Channel.SendMessageAsync("**Did you know:**\n"+facts[i],true);            
            }
            catch (Exception a)
            {
                Console.Write("+++SHOW WB ERROR: CRASH: " + a);
            }
        }

    }
}