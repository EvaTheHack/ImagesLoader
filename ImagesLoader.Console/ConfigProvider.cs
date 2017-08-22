using Fclp;
using System;


namespace ImagesLoader.Console {
    
    class ConfigProvider {
        private readonly FluentCommandLineParser<Config> parser;
        

        private ConfigProvider() {

            parser = new FluentCommandLineParser<Config> ();

            parser.Setup ( arg => arg.Url )
                .As ( 'u', "source-url" )
                .Required ()
                .WithDescription ( "Represent url source to download images" );

            parser.Setup ( arg => arg.DirectoryForSaveFiles )
                .As ( 'd', "directory" )
                .Required ()
                .WithDescription ( "Represents directory where downloaded files will be saved" );

            parser.SetupHelp("h", "help", "?")
                .Callback(val => val = val);

            
        }

        public static Config FromArgs( string[] args ) {
            var config = new Config();
            var provider = new ConfigProvider ();

            var result = provider.parser.Parse ( args );
            
            if(result.HelpCalled)
                throw new Exception(config.HelpContent);



            if (result.HasErrors)
                throw new Exception("Commang line arguments was not provided or has incorrect format.");


            return provider.parser.Object;

        }
    }
}
