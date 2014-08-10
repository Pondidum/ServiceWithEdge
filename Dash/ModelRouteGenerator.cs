using System.Text;

namespace Dash
{
	public class ModelRouteGenerator
	{
		private readonly ResourceReader _reader;
		private readonly ModelStore _modelStore;

		public ModelRouteGenerator(ResourceReader reader, ModelStore modelStore)
		{
			_reader = reader;
			_modelStore = modelStore;
		}

		public string Generate()
		{
			var template = _reader.Read("routes.js");

			var sb = new StringBuilder();

			sb.AppendLine("var models = express.Router();");
			sb.AppendLine();

			foreach (var model in _modelStore.GetAllModelNames())
			{
				var modelName = model;
				var viewName = "";

				sb.AppendLine(template.Replace("{modelName}", modelName));
			}

			sb.AppendLine("app.use('/models', models);");

			return sb.ToString();
		}
	}
}
