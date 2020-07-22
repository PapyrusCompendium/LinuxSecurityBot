using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace LinuxSecurityBot
{
	public static class Configuration
	{
		private const string _saveLocation = "config.json";
		private static JavaScriptSerializer _json = new JavaScriptSerializer();
		[Flags]
		public enum UpdateType
		{
			SaslAuth,
			PamAuth,
			Mail,
			Login,
			BoundPort,
			BlockedIP,
			PythonProcess,
			ShellProcess,
			Permissions,
			Users
		}

		public struct Config
		{
			public string token;
			public ulong operatorID;
			public ulong serverID;
			public ulong[] shellUsers;
			public UpdateType enabledUpdates;
		}

		private static Config _config = new Config();

		public static string Token
		{
			get
			{
				if (_config.token == "")
					return "";

				if (!_config.token.StartsWith("enc."))
				{
					_config.token = $"enc.{AES.EncryptString(_config.token)}";
					Save();
				}

				return AES.DecryptString(_config.token);
			}
		}

		public static ulong OperatorID { get => _config.operatorID; set => _config.operatorID = value; }
		public static ulong ServerID { get => _config.serverID; set => _config.serverID = value; }
		public static ulong[] ShellUsers { get => _config.shellUsers; set => _config.shellUsers = value; }
		public static UpdateType EnabledUpdates { get => _config.enabledUpdates; set => _config.enabledUpdates = value; }

		public static void Save() => File.WriteAllText(_saveLocation, _json.Serialize(_config));
		public static void Load()
		{
			if (!File.Exists(_saveLocation))
				Save();

			_config = _json.Deserialize<Config>(File.ReadAllText(_saveLocation));
		}
	}
}