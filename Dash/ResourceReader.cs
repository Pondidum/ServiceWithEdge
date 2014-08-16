using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Dash
{
	public class ResourceReader
	{
		public string Read(string resourceName)
		{
			using (var s = GetStream(resourceName))
			using (var reader = new StreamReader(s))
			{
				return reader.ReadToEnd();
			}
		}

		public byte[] ReadArray(string resourceName)
		{
			using (var s = GetStream(resourceName))
			using (var ms = new MemoryStream())
			{
				s.CopyTo(ms);

				return ms.ToArray();
			}
		}

		private Stream GetStream(string resourcePath)
		{
			var asm = Assembly.GetExecutingAssembly();
			var resourceNames = asm.GetManifestResourceNames();

			var name = resourceNames.FirstOrDefault(n => n.Equals(resourcePath, StringComparison.OrdinalIgnoreCase));

			if (string.IsNullOrWhiteSpace(name))
			{
				return Stream.Null;
			}

			return asm.GetManifestResourceStream(name);
		}
	}
}
