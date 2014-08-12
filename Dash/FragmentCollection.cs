using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dash.Fragments;

namespace Dash
{
	public class FragmentCollection
	{
		private readonly List<IFragment> _fragments;

		public FragmentCollection(IEnumerable<IFragment> fragments)
		{
			_fragments = fragments.ToList();
		}

		public string Build()
		{
			var sb = new StringBuilder();

			_fragments.ForEach(fragment => fragment.AppendTo(sb));

			return sb.ToString();
		}
	}
}
