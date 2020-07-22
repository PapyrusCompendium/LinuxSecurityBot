using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinuxSecurityBot.Discord
{
	public static class MessageFormatting
	{
		public static Embed ShellCreated(string shellLocation, string processUser, string processName)
		{
			EmbedBuilder embed = new EmbedBuilder()
			{
				Title = $"Shell Created - {Environment.UserName}@{Environment.MachineName}",
				Color = Color.Orange
			};

			embed.AddField("User:", processUser);
			embed.AddField("Process:", processName);
			embed.AddField("Shell Location:", shellLocation);
			return embed.Build();
		}

		public static Embed PamAuthentication(string username, string ipAddress)
		{
			EmbedBuilder embed = new EmbedBuilder()
			{
				Title = $"PAM Authentication - {Environment.UserName}@{Environment.MachineName}",
				Color = Color.DarkRed
			};

			embed.AddField("User:", username);
			embed.AddField("IP Address:", ipAddress);
			return embed.Build();
		}

		public static Embed ShellResponse(string command, string response)
		{
			EmbedBuilder embed = new EmbedBuilder()
			{
				Title = $"{Environment.UserName}@{Environment.MachineName}:$ {command}",
				Color = Color.Purple
			};

			embed.Description = response;
			return embed.Build();
		}
	}
}