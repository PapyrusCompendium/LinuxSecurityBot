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
		// Monitor Server is the main server the bot should be in, and the only server the bot should be in.
		private MonitorServer _monitorServer { get; set; }
		private DiscordSocketClient _discordClient { get; set; }
		public DiscordBot()
		{
			_discordClient = new DiscordSocketClient();
			_monitorServer = new MonitorServer(_discordClient);
			_discordClient.Ready += DiscordClient_Ready;
			_discordClient.MessageReceived += _monitorServer.ShellCommand;

			_discordClient.LoginAsync(TokenType.Bot, Configuration.Token);
			_discordClient.StartAsync();
		}

		private Task DiscordClient_Ready() =>
		Task.Run(() =>
		{
			Log.Info($"{_discordClient.CurrentUser.Username} Discord bot ready!");
		});
	}
}