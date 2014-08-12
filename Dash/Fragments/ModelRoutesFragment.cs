using System.Text;

namespace Dash.Fragments
{
	public class ModelRoutesFragment : ResourceFragment
	{
		private readonly ModelStore _modelStore;

		public ModelRoutesFragment(ResourceReader reader, ModelStore modelStore)
			: base(reader, "Dash.Fragments.modelroutes.js")
		{
			_modelStore = modelStore;
		}

		public override void AppendTo(StringBuilder sb)
		{
			var template = Reader.Read(ResourceName);

			sb.AppendLine("var models = express.Router();");
			sb.AppendLine();

			foreach (var model in _modelStore.GetAllModelNames())
			{
				var modelName = model;
				var viewName = "";

				sb.AppendLine(template.Replace("{modelName}", modelName));
			}

			sb.AppendLine("app.use('/models', models);");
		}
	}
}
