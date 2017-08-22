using System;
using System.IO;
using System.Globalization;

namespace ImagesLoader.Core {
    public class PathProvider {
        public string PrepareDirectoryForUsing (string directoryPath, string url ) {

            string timeNow = redactionTime();
            url = redactionUrl(url);
            directoryPath = createDirectoryPath(directoryPath, url, timeNow);
            Directory.CreateDirectory(directoryPath);

            return directoryPath;
            
        }
        public string redactionUrl(string url)
        {
            int indexSecondSlash = url.IndexOf('/') + 2;
            url = url.Remove(0, indexSecondSlash);
            url = url.Replace(@"/", "_");
            url = url.Replace(@"\", "_");
            return url;
        }
        private string redactionTime()
        {
            return DateTime.Now.ToString().Replace(':', '.');
        }
        private string createDirectoryPath(string directoryPath, string url,string timeNow)
        {
            return directoryPath + @"\" + url + timeNow;
        }
    }
}
