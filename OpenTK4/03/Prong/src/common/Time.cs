using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Prong.common;

/// <summary>
/// Fornece uma interface para obter informações de tempo.
/// </summary>
public class Time
{
    /// <summary>
    /// Intervalo em segundos entre o último quadro e o atual (somente leitura).
    /// </summary>
    public static float deltaTime { get; private set; } = 0.0f;

    private static float lastFrame = 0.0f;

    /// <summary>
    /// Atualiza as informações de tempo. Deve ser chamado uma vez por quadro.
    /// </summary>
    public static void Update()
    {
        float currentFrame = (float)GLFW.GetTime();

        deltaTime = currentFrame - lastFrame;
        lastFrame = currentFrame;
    }
}
