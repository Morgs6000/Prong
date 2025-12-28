using OpenTK.Windowing.Desktop;

namespace Prong.common;

/// <summary>
/// Fornece acesso às informações exibidas.
/// </summary>
public class Screen
{
    private static GameWindow _gameWindow = null!;

    /// <summary>
    /// Largura atual da janela da tela em pixels (somente leitura).
    /// </summary>
    public static int widht
    {
        get
        {
            return _gameWindow.ClientSize.X;
        }
    }

    /// <summary>
    /// Altura atual da janela da tela em pixels (somente leitura).
    /// </summary>
    public static int height
    {
        get
        {
            return _gameWindow.ClientSize.Y;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="gameWindow"></param>
    public static void Init(GameWindow gameWindow)
    {
        _gameWindow = gameWindow;
    }
}
