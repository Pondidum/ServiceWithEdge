using System;
using System.Threading.Tasks;
using EdgeJs;

namespace Dash
{
	public class DashWebUI
	{
		private readonly ResourceReader _reader;
		private readonly ModelStore _modelStore;

		public DashWebUI()
		{
			_reader = new ResourceReader();
			_modelStore = new ModelStore();
		}

		public void Register<T>(Func<T> getModel)
		{
			_modelStore.Register(getModel);
		}

		public Task Start()
		{
			var models = new ModelRouteGenerator(_reader, _modelStore).Generate();
			var app = _reader.Read("app.js");
			var start = _reader.Read("start.js");

			var func = Edge.Func(
				app + Environment.NewLine + 
				models + Environment.NewLine + 
				start + Environment.NewLine);
			
			var getModel = (Func<object, Task<object>>)(async (message) =>
			{
				return _modelStore.GetModel((string)message);
			});

			var args = new
			{
				port = 3000,
				getModel = getModel
			};
			

			return Task.Run(() => func(args));

		}
	}
}
