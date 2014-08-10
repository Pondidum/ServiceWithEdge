using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceWithEdge
{
	public class ModelStore
	{
		private readonly Dictionary<string, Func<object>> _models;

		public ModelStore()
		{
			_models = new Dictionary<string, Func<object>>(StringComparer.OrdinalIgnoreCase);
		}

		public void Register(object model)
		{
			Register(model.GetType(), () => model);
		}

		public void Register<T>(Func<T> getModel)
		{
			Register(typeof(T), () => getModel());
		}

		private void Register(Type type, Func<object> getModel)
		{
			if (_models.ContainsKey(type.Name))
			{
				throw new Exception();
			}

			_models[type.Name] = getModel;
		}

		public IEnumerable<string> GetAllModelNames()
		{
			return _models.Keys.Select(m => m.Replace("Model", ""));
		}

		public object GetModel(string name)
		{
			return _models[name + "Model"].Invoke();
		}
	}
}
