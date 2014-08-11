using System;
using System.Threading.Tasks;
using EdgeJs;

namespace Dash
{
	public class DashWebUI
	{
		private readonly ResourceReader _reader;
		private readonly ModelStore _modelStore;
		private readonly ViewWriter _views;

		public DashWebUI()
		{
			_reader = new ResourceReader();
			_modelStore = new ModelStore();
			_views = new ViewWriter(_reader);

			_views.FromNamespace(typeof(DashWebUI).Assembly, "Views");
		}

		public void Register<T>(Func<T> getModel)
		{
			_modelStore.Register(getModel);
		}

		public Task Start()
		{
			_views.WriteViews();

			var models = new ModelRouteGenerator(_reader, _modelStore).Generate();
			var app = _reader.Read("Dash.Fragments.app.js");
			var start = _reader.Read("Dash.Fragments.start.js");

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
