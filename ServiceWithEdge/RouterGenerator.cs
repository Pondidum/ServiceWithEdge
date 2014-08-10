using System.Text;

namespace ServiceWithEdge
{
	public class RouterGenerator
	{
		private readonly ModelStore _modelStore;

		public RouterGenerator(ModelStore modelStore)
		{
			_modelStore = modelStore;
		}

		public string Generate()
		{
			var sb = new StringBuilder();

			sb.AppendLine("var models = express.Router();");
			sb.AppendLine();

			foreach (var model in _modelStore.GetAllModelNames())
			{
				var modelName = model;
				var viewName = "";

				sb.AppendFormat(RouteTemplate, modelName);
			}

			sb.AppendLine("app.use('/models', models);");

			return sb.ToString();
		}

		private const string RouteTemplate = @"
			models.get('/{0}', function(req, res) {{
				communicator.getModel('{0}', function(model) {{
					res.json({{ model: model }});
				}});
			}});
		";
	}
}
