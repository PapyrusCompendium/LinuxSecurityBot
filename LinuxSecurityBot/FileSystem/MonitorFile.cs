using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinuxSecurityBot.FileSystem
{
	public class MonitorFile
	{
		public delegate void FileChnagedEvent(string deltaText);
		public event FileChnagedEvent FileUpdated;

		private string _fileName = "";
		private FileSystemWatcher _fileWatcher;
		private long _fileSize = 0;
		public MonitorFile(string fileName)
		{
			if (!File.Exists(fileName))
				throw new Exception("File does no exist!");

			_fileName = fileName;
			_fileSize = new FileInfo(_fileName).Length;
			_fileWatcher = new FileSystemWatcher(fileName);
			_fileWatcher.EnableRaisingEvents = true;
			_fileWatcher.Changed += _fileWatcher_Changed;
		}

		private void _fileWatcher_Changed(object sender, FileSystemEventArgs e)
		{
			byte[] deltaData = File.ReadAllBytes(_fileName).Skip((int)_fileSize).ToArray();
			FileUpdated.Invoke(Encoding.UTF8.GetString(deltaData));
		}
	}
}