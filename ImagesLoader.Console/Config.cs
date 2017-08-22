using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesLoader.Console {
    class Config {
        public string Url { get; set; }
        public string DirectoryForSaveFiles { get; set; }
        public string HelpContent {
            get
            {
                return "This application is designed for downloading pictures from the site to the specified folder" + Environment.NewLine + 
                       "For example \"-u (some url) -d (directory for save)\"";
            }
        }
    }
}
