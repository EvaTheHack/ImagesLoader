using ImagesLoader.Core;

namespace ImagesLoader.Console
{
    class ConsoleLogger : ILogger {
        public void ShowMessage ( string message ) {
            System.Console.WriteLine (message);
        }
    }
}
