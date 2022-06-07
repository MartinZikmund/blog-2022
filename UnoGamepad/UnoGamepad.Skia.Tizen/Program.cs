using Tizen.Applications;
using Uno.UI.Runtime.Skia;

namespace UnoGamepad.Skia.Tizen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = new TizenHost(() => new UnoGamepad.App());
            host.Run();
        }
    }
}
