using System;
using System.Collections.Generic;
using System.Linq;

namespace ImagesLoader.Console {
    class Program {

        private const string EXIT_REQUEST = "exit";
        

        static void Main(string[] args)
        {
            Config config = new Config();
            
            string currentRequest = String.Empty;

            while ((currentRequest = GetNonEmptyStringFromConsole("type new command here -> ")) != EXIT_REQUEST)
                ProcessRequest(config, currentRequest);
        }
        private static void ProcessRequest(Config config, string request)
        {
            try
            {
                Core.ILogger logger = new ConsoleLogger();

                string[] requestItems = request.Split(' ');

                Config result = ConfigProvider.FromArgs(requestItems);

                Core.ImagesLoader loader = new Core.ImagesLoader(logger);
                loader.FromUrl(result.Url, result.DirectoryForSaveFiles);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        private static string GetNonEmptyStringFromConsole(string message)
        {
            while (true)
            {
                try
                {
                    System.Console.WriteLine();
                    System.Console.Write(message);
                    string request = System.Console.ReadLine();

                    if (string.IsNullOrEmpty(request))
                    {
                        System.Console.WriteLine("Uncorrect request. Try again. Use command --help");
                        continue;
                    }
                    return request;
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
