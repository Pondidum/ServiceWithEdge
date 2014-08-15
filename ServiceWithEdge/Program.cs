using System;
using Dash;
using ServiceWithEdge.Models;

namespace ServiceWithEdge
{
	class Program
	{
		static void Main(string[] args)
		{
			RunWebui();
			Console.WriteLine("Press any key to exit");
			Console.ReadKey();
		}

		private static void RunWebui()
		{
			var ui = new DashWebUI();

			ui.Register(() => new IndexModel());
			ui.Start();
		}
	}
}
