using System;
using Dash;

namespace ServiceWithEdge
{
	class Program
	{
		static void Main(string[] args)
		{
			RunWebui();
			Console.ReadKey();
		}

		private static void RunWebui()
		{
			var ui = new DashWebUI();
			ui.Start();
			//var router = new RouterGenerator(store);

			//var app = GetApp().Replace("//{Models}", router.Generate());
			//var func = Edge.Func(app);

			//var getModel = (Func<object, Task<object>>)(async (message) =>
			//{
			//	return store.GetModel((string)message);
			//});
		}
	}
}
