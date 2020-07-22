using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinuxSecurityBot.LinuxSystem
{
	public static class Shell
	{
		public static string Execute(string[] command)
		{
			Process shellExecution = new Process()
			{
				StartInfo = new ProcessStartInfo(command[0], string.Join(" ", command.Skip(1)))
				{ 
					UseShellExecute = true,
					RedirectStandardError = true,
					RedirectStandardOutput = true
				}
			};

			shellExecution.Start();
			return shellExecution.StandardOutput.ReadToEnd();
		}
	}
}