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

    private float xDaBola = 0.0f;
    private float yDaBola = 0.0f;
    private float tamanhoDaBola = 20.0f;
    private float velocidadeDaBolaEmX = 300.0f;
    private float velocidadeDaBolaEmY = 300.0f;

    private float yDoJogador1 = 0.0f;
    private float yDoJogador2 = 0.0f;

    private float xDoJogador1()
    {
        return -ClientSize.X / 2 + larguraDosJogadores() / 2.0f;
    }

    private float xDoJogador2()
    {
        return ClientSize.X / 2 - larguraDosJogadores() / 2.0f;
    }

    private float larguraDosJogadores()
    {
        return tamanhoDaBola;
    }
    
    private float alturaDosJogadores()
    {
        return 3.0f * tamanhoDaBola;
    }

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

        GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);

        mesh.Begin();

        mesh.Vertex2(-0.5f, -0.5f);
        mesh.Vertex2( 0.5f, -0.5f);
        mesh.Vertex2( 0.5f,  0.5f);
        mesh.Vertex2(-0.5f,  0.5f);

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

        xDaBola += velocidadeDaBolaEmX * Time.deltaTime;
        yDaBola += velocidadeDaBolaEmY * Time.deltaTime;

        if(xDaBola + tamanhoDaBola / 2.0f > ClientSize.X / 2)
        {
            velocidadeDaBolaEmX = -velocidadeDaBolaEmX;
        }
        if(xDaBola - tamanhoDaBola / 2.0f < -ClientSize.X / 2)
        {
            velocidadeDaBolaEmX = -velocidadeDaBolaEmX;
        }
        if(yDaBola + tamanhoDaBola / 2.0f > ClientSize.Y / 2)
        {
            velocidadeDaBolaEmY = -velocidadeDaBolaEmY;
        }
        if (yDaBola - tamanhoDaBola / 2.0f < -ClientSize.Y / 2)
        {
            velocidadeDaBolaEmY = -velocidadeDaBolaEmY;
        }
        if(xDaBola + tamanhoDaBola / 2.0f > xDoJogador2() - larguraDosJogadores() / 2.0f &&
           yDaBola - tamanhoDaBola / 2.0f < yDoJogador2 + alturaDosJogadores() / 2.0f &&
           yDaBola + tamanhoDaBola / 2.0f > yDoJogador2 - alturaDosJogadores() / 2.0f)
        {
            velocidadeDaBolaEmX = -velocidadeDaBolaEmX;
        }
        if (xDaBola - tamanhoDaBola / 2.0f < xDoJogador1() + larguraDosJogadores() / 2.0f &&
           yDaBola - tamanhoDaBola / 2.0f < yDoJogador1 + alturaDosJogadores() / 2.0f &&
           yDaBola + tamanhoDaBola / 2.0f > yDoJogador1 - alturaDosJogadores() / 2.0f)
        {
            velocidadeDaBolaEmX = -velocidadeDaBolaEmX;
        }
        if(xDaBola - tamanhoDaBola / 2.0f < -ClientSize.X / 2 || xDaBola + tamanhoDaBola / 2.0f > ClientSize.X / 2)
        {
            xDaBola = 0.0f;
            yDaBola = 0.0f;
        }

        if (KeyboardState.IsKeyDown(Keys.W))
        {
            yDoJogador1 += 500.0f * Time.deltaTime;
        }
        if (KeyboardState.IsKeyDown(Keys.S))
        {
            yDoJogador1 -= 500.0f * Time.deltaTime;
        }

        if (KeyboardState.IsKeyDown(Keys.Up))
        {
            yDoJogador2 += 500.0f * Time.deltaTime;
        }
        if (KeyboardState.IsKeyDown(Keys.Down))
        {
            yDoJogador2 -= 500.0f * Time.deltaTime;
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

        shader.SetBool("hasCustomColor", false);

        DesenharRetangulo(xDaBola, yDaBola, tamanhoDaBola, tamanhoDaBola, 1.0f, 1.0f, 0.0f);
        DesenharRetangulo(xDoJogador1(), yDoJogador1, larguraDosJogadores(), alturaDosJogadores(), 1.0f, 0.0f, 0.0f);
        DesenharRetangulo(xDoJogador2(), yDoJogador2, larguraDosJogadores(), alturaDosJogadores(), 0.0f, 0.0f, 1.0f);

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

    private void DesenharRetangulo(float x, float y, float largura, float altura, float r, float g, float b)
    {
        Matrix4 model = Matrix4.Identity;
        model *= Matrix4.CreateScale(largura, altura, 0.0f);
        model *= Matrix4.CreateTranslation(x, y, 0.0f);
        shader.SetMatrix4("model", model);

        shader.SetBool("hasCustomColor", true);
        shader.SetVector4("uniformColor", r, g, b, 1.0f);

        mesh.Draw(shader, shadedMode);
    }
}
