using System;
using System.Collections.Generic;

namespace ServiceWithEdge
{
	public class ModelStore
	{
		private readonly Dictionary<string, object> _models;

		public ModelStore()
		{
			_models = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
		}

		public void Register(object model)
		{
			_models[model.GetType().Name] = model;
		}

		public object GetModel(string name)
		{
			object model;

			if (_models.TryGetValue(name + "Model", out model))
			{
				return model;
			}

			throw new Exception(string.Format("Cannot find a model called '{0}'", name));
		}
	}
}
