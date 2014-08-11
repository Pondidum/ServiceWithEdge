﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Dash
{
	public class ViewWriter
	{
		private readonly ResourceReader _reader;
		private readonly List<string> _views;

		public ViewWriter(ResourceReader reader)
		{
			_reader = reader;
			_views = new List<string>();
		}

		public void FromNamespace(Assembly assembly, string ns)
		{
			var fullNamespace = typeof (ViewWriter).Namespace + "." + ns;

			_views.AddRange(assembly
				.GetManifestResourceNames()
				.Where(res => res.StartsWith(fullNamespace, StringComparison.OrdinalIgnoreCase)));
		}

		public void WriteViews()		//IFileSystem fs
		{
			var paths = _views
				.ToDictionary(ns => ns, PathFromNamespace);

			paths
				.Values
				.Select(Path.GetDirectoryName)
				.Distinct()
				.ToList()
				.ForEach(path => Directory.CreateDirectory(path));

			paths
				.ToDictionary(p => _reader.Read(p.Key), p => p.Value)
				.ToList()
				.ForEach(pair => File.WriteAllText(pair.Value, pair.Key));
		}

		private string PathFromNamespace(string ns)
		{
			var chars = ns.ToList();

			while (chars.Count(c => c == '.') > 1)
			{
				var index = chars.IndexOf('.');
				chars[index] = '\\';
			}

			return new string(chars.ToArray()).Replace("Dash\\", "");
		}
	}
}