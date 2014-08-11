using System.IO;
using System.Reflection;

namespace Dash
{
	public class ResourceReader
	{
		public string Read(string resourceName)
		{
			var asm = Assembly.GetExecutingAssembly();
			var name = typeof (ResourceReader).Namespace + resourceName;

			using (var s = asm.GetManifestResourceStream(name))
			using (var reader = new StreamReader(s))
			{
				return reader.ReadToEnd();
			}
		} 
	}
}
