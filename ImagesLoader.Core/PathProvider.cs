using System;
using System.IO;

namespace ImagesLoader.Core
{
    public class PathProvider
    {
        public string PrepareDirectoryForUsing(string directoryPath, string url)
        {
            var currentTime = PrepareTime();
            var correctUrl = PrepareUrl(url);
            directoryPath = CreateDirectoryPath(directoryPath, correctUrl, currentTime);
            Directory.CreateDirectory(directoryPath);

            return directoryPath;
        }
        public string PrepareUrl(string url)
        {
            var indexSecondSlash = url.IndexOf('/') + 2;
            url = url.Remove(0, indexSecondSlash);
            url = url.Replace(@"/", "_");
            url = url.Replace(@"\", "_");
            return url;
        }
        private string PrepareTime()
        {
            return DateTime.Now.ToString().Replace(':', '.');
        }
        private string CreateDirectoryPath(string directoryPath, string url,string currentTime)
        {
            return $"{directoryPath}\\{url}{currentTime}";
        }
    }
}
