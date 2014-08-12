using System.Text;

namespace Dash
{
	public class ResourceFragment : IFragment
	{
		protected ResourceReader Reader { get; set; }
		protected string ResourceName { get; set; }

		
		public ResourceFragment(ResourceReader reader, string resourceName)
		{
			Reader = reader;
			ResourceName = resourceName;
		}

		public virtual void AppendTo(StringBuilder sb)
		{
			sb.AppendLine();
			sb.AppendLine(Reader.Read(ResourceName));
		}
	}
}
