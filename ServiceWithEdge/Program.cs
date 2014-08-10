using System;
using System.IO;
using System.Reflection;
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
			try
			{

				var store = new ModelStore();

				RunSomeService(store);
				RunWebui(store);

			}
			catch (Exception ex)
			{
				Console.Write(ex);
			}

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
					model.Iterations++;
				}
			});
		}

		private static string GetApp()
		{
			var asm = Assembly.GetExecutingAssembly();
			var name = "ServiceWithEdge.webui.app.js";

			using (var s = asm.GetManifestResourceStream(name))
			using (var reader = new StreamReader(s))
			{
				return reader.ReadToEnd();
			}
		}

		private static void RunWebui(ModelStore store)
		{
			var router = new RouterGenerator(store);

			var app = GetApp().Replace("//{Models}", router.Generate());
			var func = Edge.Func(app);

			var getModel = (Func<object, Task<object>>)(async (message) =>
			{
				return store.GetModel((string)message);
			});


			Task.Run(() =>
			{
				try
				{
					func(new
					{
						getModel,
						port = 3000,
					});
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
				}
			});
		}
	}
}
