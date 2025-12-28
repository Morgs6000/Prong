namespace Prong.common;

/// <summary>
/// Classe contendo métodos para facilitar a depuração durante o desenvolvimento de um jogo.
/// </summary>
public class Debug
{
    /// <summary>
    /// Registra uma mensagem no Console.
    /// </summary>
    /// <param name="mensage"></param>
    /// <param name="showMilliseconds"></param>
    public static void Log(object? mensage, bool showMilliseconds = false)
    {
        WriteFormated(mensage, null, showMilliseconds);
    }

    /// <summary>
    /// Uma variante de Debug.Log que registra uma mensagem de erro no console.
    /// </summary>
    /// <param name="mensage"></param>
    /// <param name="showMilliseconds"></param>
    public static void LogError(object? mensage, bool showMilliseconds = false)
    {
        WriteFormated(mensage, ConsoleColor.Red, showMilliseconds);
    }

    /// <summary>
    /// Uma variante de Debug.Log que registra uma mensagem de aviso no console.
    /// </summary>
    /// <param name="mensage"></param>
    /// <param name="showMilliseconds"></param>
    public static void LogWarning(object? mensage, bool showMilliseconds = false)
    {
        WriteFormated(mensage, ConsoleColor.Yellow, showMilliseconds);
    }

    /// <summary>
    /// Uma variante de Debug.Log que registra uma mensagem de sucesso no console.
    /// </summary>
    /// <param name="mensage"></param>
    /// <param name="showMilliseconds"></param>
    public static void LogSuccess(object? mensage, bool showMilliseconds = false)
    {
        WriteFormated(mensage, ConsoleColor.Green, showMilliseconds);
    }

    /// <summary>
    /// Uma variante de Debug.Log que registra uma mensagem de informação no console.
    /// </summary>
    /// <param name="mensage"></param>
    /// <param name="showMilliseconds"></param>
    public static void LogInfo(object? mensage, bool showMilliseconds = false)
    {
        WriteFormated(mensage, ConsoleColor.Cyan, showMilliseconds);
    }

    private static void WriteFormated(object? value, ConsoleColor? color = null, bool showMilliseconds = false)
    {
        string format = showMilliseconds ? "[HH:mm:ss.fff] " : "[HH:mm:ss] ";
        Console.Write(DateTime.Now.ToString(format));

        Console.ForegroundColor = color.HasValue ? color.Value : Console.ForegroundColor;
        Console.WriteLine(value);
        Console.ResetColor();
    }
}
