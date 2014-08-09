using System;
using System.Collections.Generic;

namespace ServiceWithEdge
{
	public class ModelStore
	{
		private readonly Dictionary<string, Func<object>> _models;

		public ModelStore()
		{
			_models = new Dictionary<string, Func<object>>();
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
			return _models.Keys;
		}
	}
}
