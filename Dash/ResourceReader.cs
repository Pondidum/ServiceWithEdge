using System.IO;
using System.Reflection;

namespace Dash
{
	public class ResourceReader
	{
		public string Read(string resourceName)
		{
			var asm = Assembly.GetExecutingAssembly();

			using (var s = asm.GetManifestResourceStream(resourceName))
			using (var reader = new StreamReader(s))
			{
				return reader.ReadToEnd();
			}
		} 
	}
}
