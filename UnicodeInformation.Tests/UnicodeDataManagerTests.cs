﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.IO.Compression;

namespace UnicodeInformation.Tests
{
	[TestClass]
	public class UnicodeDataManagerTests
	{
		private const string UcdDirectoryName = "UCD";

        [TestInitialize]
		public void Initialize()
		{
			var directoryName = Path.GetFullPath(UcdDirectoryName);

			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);

				var fileName = Path.Combine(directoryName, "UCD.zip");

				if (!File.Exists(fileName))
				{
					new WebClient().DownloadFile("http://www.unicode.org/Public/UCD/latest/ucd/UCD.zip", fileName);
					ZipFile.ExtractToDirectory(fileName, directoryName);
				}
			}
		}

		[TestMethod]
		public async Task DownloadAndBuildDataAsync()
		{
			var source = new FileUcdSource(UcdDirectoryName);

			await UnicodeDataManager.DownloadAndBuildDataAsync(source);
		}
	}
}
