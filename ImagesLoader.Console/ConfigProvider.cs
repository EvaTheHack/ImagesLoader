using Fclp;
using System;

namespace ImagesLoader.Console
{    
    public class ConfigProvider
    {
        private readonly FluentCommandLineParser<Config> _parser;
        private readonly Config _config = new Config();

        public ConfigProvider()
        {
            _parser = new FluentCommandLineParser<Config>();

            _parser.Setup ( arg => arg.Url )
                .As ( 'u', "source-url" )
                .Required ()
                .WithDescription ( "Represent url source to download images" );

            _parser.Setup ( arg => arg.DirectoryForSaveFiles )
                .As ( 'd', "directory" )
                .Required ()
                .WithDescription ( "Represents directory where downloaded files will be saved" );

            _parser.SetupHelp("h", "help", "?")
                .Callback(text => System.Console.WriteLine(_config.HelpContent));
        }

        public Config FromArgs(string[] args)
        {
            
            var provider = new ConfigProvider();

            var result = provider._parser.Parse(args);

            if (result.HelpCalled)
            {
                throw new Exception(_config.HelpContent);
            }

            if (result.HasErrors)
            {
                throw new Exception("Commang line arguments was not provided or has incorrect format.");
            }

            return provider._parser.Object;
        }
    }
}
