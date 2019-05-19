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
            var pathProvider = new PathProvider();
            var pathForSave = pathProvider.PrepareDirectoryForUsing(targetDirectory, url);
            var imageUrls = GetAllImageUrls(url);


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

        private List<string> GetAllImageUrls(string url)
        {
            try
            {
                var document = new HtmlWeb().Load(url);
                return document.DocumentNode
                               .Descendants("img")
                               .Select(e => e.GetAttributeValue("src", null))
                               .Where(s => !string.IsNullOrEmpty(s))
                               .ToList();
            }
            catch
            {
                logger.ShowMessage(url + " has error or incorect");
                return new List<string>();
            }
        }

        private void DownloadFromLinks(string url, string link, string pathToSave)
        {
            var pathProvider = new PathProvider();

            if (link.StartsWith("//"))
            { 
                link = "https:" + link;
            }

            if (link.StartsWith("/"))
            {
                link = url + link;
            }
                
            var fileName = pathToSave + @"\" + pathProvider.PrepareUrl(link);
            using (var client = new WebClient())
            {
                client.DownloadFile(link, fileName);
            }
        }

    }
}
