using System.Threading;
using System.Threading.Tasks;
using EdgeJs;

namespace Dash
{
	public class DashWebUI
	{
		private readonly ResourceReader _reader;

		public DashWebUI()
		{
			_reader = new ResourceReader();
		}

		public Task Start()
		{
			var app = _reader.Read("app.js");
			var start = _reader.Read("start.js");

			var func = Edge.Func(app + start);

			return Task.Run(() => func(new { port = 3000 }));

		}
	}
}
