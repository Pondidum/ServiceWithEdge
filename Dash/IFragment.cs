using System.Text;

namespace Dash
{
	public interface IFragment
	{
		void AppendTo(StringBuilder sb);
	}
}
