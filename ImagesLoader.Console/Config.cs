using System;

namespace ImagesLoader.Console
{
    class Config
    {
        public string Url { get; set; }
        public string DirectoryForSaveFiles { get; set; }
        public string HelpContent => 
            $"This application is designed for downloading pictures from the site to the specified folder" +
            $"{Environment.NewLine}For example \"-u (some url) -d (directory for save)\"";
    }
}
