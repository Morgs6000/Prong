using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

namespace Prong;

public class Program
{
    private static GameWindowSettings gameWindowSettings
    {
        get
        {
            GameWindowSettings gws = GameWindowSettings.Default;

            return gws;
        }
    }

    private static NativeWindowSettings nativeWindowSettings
    {
        get
        {
            NativeWindowSettings nws = NativeWindowSettings.Default;

            nws.ClientSize = new Vector2i(640, 480);
            nws.Title = "OpenTK Game Window";
            nws.StartVisible = false;

            return nws;
        }
    }

    private static void Main(string[] args)
    {
        using(Window window = new Window(gameWindowSettings, nativeWindowSettings))
        {
            window.CenterWindow();
            window.IsVisible = true;

            window.Run();
        }
    }
}
