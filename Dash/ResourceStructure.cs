using System;
using System.IO;
using System.Linq;

namespace Dash
{
	public class ResourceStructure
	{
		private readonly ResourceReader _reader;
		private readonly string _namespace;

		public ResourceStructure(ResourceReader reader, string ns)
		{
			_reader = reader;
			_namespace = ns;
		}

		public void Write()
		{
			var asm = GetType().Assembly;

			var resources = asm
				.GetManifestResourceNames()
				.Where(name => name.StartsWith(_namespace, StringComparison.OrdinalIgnoreCase))
				.ToDictionary(
					name => PathFromNamespace(name.Substring(0, _namespace.Length)),
					name => _reader.Read(name));
			resources
				.Keys
				.Select(Path.GetDirectoryName)
				.Distinct()
				.ToList()
				.ForEach(path => Directory.CreateDirectory(path));

			resources
				.ToList()
				.ForEach(pair => File.WriteAllText(pair.Key, pair.Value));
		}

		private string PathFromNamespace(string ns)
		{
			var chars = ns.ToList();

			while (chars.Count(c => c == '.') > 1)
			{
				var index = chars.IndexOf('.');
				chars[index] = '\\';
			}

			return new string(chars.ToArray());
		}
	}
}
