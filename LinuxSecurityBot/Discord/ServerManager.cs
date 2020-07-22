using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinuxSecurityBot.Discord
{
	public static class ServerManager
	{
		public static Dictionary<Configuration.UpdateType, SocketTextChannel> ChannelTypes = new Dictionary<Configuration.UpdateType, SocketTextChannel>();

		private static SocketGuild _server { get; set; }
		public static void ConfigureServerChannels(SocketGuild server)
		{
			_server = server;
			ChannelTypes.Add(Configuration.UpdateType.ShellResponse, Appendchannel("Discord Shell"));
			ChannelTypes.Add(Configuration.UpdateType.PamAuth, Appendchannel("Pam Auths"));
			ChannelTypes.Add(Configuration.UpdateType.SaslAuth, Appendchannel("SASL Auths"));
			ChannelTypes.Add(Configuration.UpdateType.PythonProcess, Appendchannel("Python Processes"));
			ChannelTypes.Add(Configuration.UpdateType.ShellProcess, Appendchannel("shell Processes"));
			ChannelTypes.Add(Configuration.UpdateType.BlockedIP, Appendchannel("Blocked Addresses"));
			ChannelTypes.Add(Configuration.UpdateType.BoundPort, Appendchannel("Bound Ports"));
			ChannelTypes.Add(Configuration.UpdateType.Users, Appendchannel("User Delta"));
		}

		private static SocketTextChannel Appendchannel(string channelName)
		{
			if (!_server.TextChannels.Any(i => i.Name == channelName))
				return _server.TextChannels.First(i => i.Id == _server.CreateTextChannelAsync(channelName).Result.Id);

			return _server.TextChannels.First(i => i.Name == channelName);
		}
	}
}