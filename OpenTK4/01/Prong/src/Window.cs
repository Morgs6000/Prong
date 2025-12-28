using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Prong.common;

namespace Prong;

public class Window : GameWindow
{
    private Shader shader;
    private Mesh mesh;
    private ShadedMode shadedMode;

    public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
    {
        Screen.Init(this);

        string vertexPath = "res/shaders/default/vertex.glsl";
        string fragmentPath = "res/shaders/default/fragment.glsl";
        shader = new Shader(vertexPath, fragmentPath);

        mesh = new Mesh();

        shadedMode = ShadedMode.Shaded;
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        mesh.Begin();

        DesenharRetangulo(0, 0, 20, 20);
        DesenharRetangulo(-310, 0, 20, 40);
        DesenharRetangulo(310, 0, 20, 40);

        mesh.End();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);

        Time.Update();
        Input.Update(this);

        if (KeyboardState.IsKeyDown(Keys.Escape))
        {
            Close();
        }
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit);

        shader.Use();

        Matrix4 model = Matrix4.Identity;
        shader.SetMatrix4("model", model);

        Matrix4 view = Matrix4.Identity;
        shader.SetMatrix4("view", view);

        Matrix4 projection = Matrix4.Identity;
        projection *= CreateOrthographic();
        shader.SetMatrix4("projection", projection);

        mesh.Draw(shader, shadedMode);

        SwapBuffers();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
    {
        base.OnFramebufferResize(e);

        GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
    }

    private Matrix4 CreateOrthographic()
    {
        float width = ClientSize.X;
        float height = ClientSize.Y;
        float depthNear = 0.0f;
        float depthFar = 1.0f;

        return Matrix4.CreateOrthographic(width, height, depthNear, depthFar);
    }

    private void DesenharRetangulo(int x, int y, int largura, int altura)
    {
        mesh.Vertex2(-0.5f * largura + x, -0.5f * altura + y);
        mesh.Vertex2( 0.5f * largura + x, -0.5f * altura + y);
        mesh.Vertex2( 0.5f * largura + x,  0.5f * altura + y);
        mesh.Vertex2(-0.5f * largura + x,  0.5f * altura + y);
    }
}
