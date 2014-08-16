using System;
using System.Threading.Tasks;
using Dash.Fragments;
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
			var fragments = new FragmentCollection(new IFragment[]
			{
				new AppFragment(_reader),
				new ViewEngineFragment(_reader),
				new RoutesFragment(_reader),
				new ModelRoutesFragment(_reader, _modelStore),
				new StartFragment(_reader)
			});

			var func = Edge.Func(fragments.Build());

			var getModel = (Func<object, Task<object>>)(async (message) =>
			{
				return _modelStore.GetModel((string)message);
			});

			var getStaticResource = (Func<object, Task<object>>)(async (arg) =>
			{
				var path = (string)arg;

				return _reader.ReadArray("Dash" + path.Replace("/", "."));
			});

			var args = new
			{
				port = 3000,
				getModel = getModel,
				getStatic = getStaticResource
			};

			return Task.Run(() => func(args));

		}
	}
}
