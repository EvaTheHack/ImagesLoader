using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ImagesLoader.Core
{
    public class ImagesLoader {

        public ImagesLoader(ILogger logger)
        {
            this.logger = logger;
        }

        private readonly ILogger logger;

        public void FromUrl(string url, string targetDirectory)
        {
            PathProvider pathProvider = new PathProvider();
            string pathForSave = pathProvider.PrepareDirectoryForUsing(targetDirectory, url);
            List<string> imageUrls = GetAllImageUrls(url);

            if (imageUrls != null)
            {
                foreach (var imageUrl in imageUrls)
                {
                    try
                    {
                        DownloadFromLinks(url, imageUrl, pathForSave);
                        logger.ShowMessage($"Image from url[{ imageUrl}] was downloaded");
                    }
                    catch
                    {
                        logger.ShowMessage(imageUrl + " has error or incorect");
                    }
                }
            }
        }

        private List<string> GetAllImageUrls ( string url ) {
            
            try
            {
                var document = new HtmlWeb().Load(url);
                var urls = document.DocumentNode.Descendants("img")
                                                .Select(e => e.GetAttributeValue("src", null))
                                                .Where(s => !String.IsNullOrEmpty(s));
                return urls.ToList();
            }
            catch
            {
                logger.ShowMessage(url + " has error or incorect");
                return null;
            }
        }

        private void DownloadFromLinks(string url, string link, string pathToSave)
        {
            PathProvider pathProvider = new PathProvider();

            if (link.StartsWith("//"))
                link = "https:" + link;

            if (link.StartsWith("/"))
                link = url + link;

            
            string fileName = pathToSave + @"\" + pathProvider.redactionUrl(link);
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(link, fileName);
            }
        }

    }
}
