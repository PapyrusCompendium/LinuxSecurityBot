using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinuxSecurityBot.Discord
{
	public class MonitorServer
	{
		private DiscordSocketClient _discordClient { get; set; }
		public MonitorServer(DiscordSocketClient discordClient) => _discordClient = discordClient;

		public Task ShellCommand(SocketMessage arg) =>
		Task.Run(() =>
		{
			if (arg.Author.Id != Configuration.OperatorID)
				return;

			SendShellResponse(arg.Content, LinuxSystem.Shell.Execute(arg.Content.Split(' ')));
		});

		private void SendShellResponse(string command, string responseMessage) =>
			ServerManager.ChannelTypes[Configuration.UpdateType.ShellResponse].SendMessageAsync("", false, MessageFormatting.ShellResponse(command, responseMessage));
	}
}