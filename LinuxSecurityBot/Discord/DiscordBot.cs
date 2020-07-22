using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinuxSecurityBot.Discord
{
	public class DiscordBot
	{
		private DiscordSocketClient discordClient { get; set; }
		public DiscordBot()
		{
			discordClient = new DiscordSocketClient();
			discordClient.Ready += DiscordClient_Ready;


			discordClient.LoginAsync(TokenType.Bot, Configuration.Token);
			discordClient.StartAsync();
		}

		private Task DiscordClient_Ready() =>
		Task.Run(() =>
		{

		});
	}
}