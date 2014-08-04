using System;
using System.Threading;
using System.Threading.Tasks;
using EdgeJs;
using ServiceWithEdge.Models;

namespace ServiceWithEdge
{
	class Program
	{
		static void Main(string[] args)
		{
			var store = new ModelStore();

			RunSomeService(store);
			RunWebui(store);

			Console.ReadKey();
		}

		static void RunSomeService(ModelStore store)
		{
			Task.Run(() =>
			{
				var model = new IndexModel();
				store.Register(model);

				while (true)
				{
					Thread.Sleep(500);
					Console.WriteLine(model.Iterations++);
				}
			});
		}

		private static void RunWebui(ModelStore store)
		{
			var func = Edge.Func(@"
				var app = require('../webui/app');
				var com = require('../webui/communicator');

				app.set('port', process.env.PORT || 3000);

				var server = app.listen(app.get('port'));
	
				return function(options, callback) {
					com.set(options);
				};
			");

			var getModel = (Func<object, Task<object>>)(async (message) =>
			{
				return store.GetModel((string)message);
			});


			Task.Run(() => func(new
			{
				getModel
			}));
		}
	}
}
