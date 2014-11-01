﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicodeInformation.Tests
{
	internal sealed class FileUcdSource : IUcdSource
	{
		private readonly string baseDirectory;

		public FileUcdSource(string baseDirectory)
		{
			this.baseDirectory = Path.GetFullPath(baseDirectory);
		}

		public Task<Stream> OpenDataFileAsync(string fileName)
		{
			return Task.FromResult<Stream>(File.OpenRead(Path.Combine(baseDirectory, fileName)));
		}
	}
}