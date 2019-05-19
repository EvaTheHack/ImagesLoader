using System;

namespace ImagesLoader.Console
{
    class Program
    {
        private static ConfigProvider _config = new ConfigProvider();

        private const string EXIT_REQUEST = "exit";   

        static void Main(string[] args)
        {
            var config = new Config();
            
            var currentRequest = string.Empty;

            while ((currentRequest = GetNonEmptyStringFromConsole("type new command here -> ")) != EXIT_REQUEST)
                ProcessRequest(config, currentRequest);
        }
        private static void ProcessRequest(Config config, string request)
        {
            try
            {
                Core.ILogger logger = new ConsoleLogger();

                var requestItems = request.Split(' ');

                var result = _config.FromArgs(requestItems);

                var loader = new Core.ImagesLoader(logger);
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
