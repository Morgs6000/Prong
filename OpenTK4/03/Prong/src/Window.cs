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

    private Retangulo bola;
    private Retangulo jogador1;
    private Retangulo jogador2;

    private float velocidadeDaBolaEmX = 300.0f;
    private float velocidadeDaBolaEmY = 300.0f;

    public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
    {
        Screen.Init(this);

        string vertexPath = "res/shaders/default/vertex.glsl";
        string fragmentPath = "res/shaders/default/fragment.glsl";
        shader = new Shader(vertexPath, fragmentPath);

        mesh = new Mesh();

        shadedMode = ShadedMode.Shaded;

        bola = CriarRetangulo(0.0f, 0.0f, 20.0f, 20.0f);

        float larguraDosJogadores = bola.largura;
        float alturaDosJogadores = bola.altura * 3.0f;

        jogador1 = CriarRetangulo(-ClientSize.X / 2 + larguraDosJogadores / 2.0f, 0.0f, larguraDosJogadores, alturaDosJogadores);
        jogador2 = CriarRetangulo(ClientSize.X / 2 - larguraDosJogadores / 2.0f, 0.0f, larguraDosJogadores, alturaDosJogadores);
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

        bola.x += velocidadeDaBolaEmX * Time.deltaTime;
        bola.y += velocidadeDaBolaEmY * Time.deltaTime;

        if(bola.x + bola.largura / 2.0f > ClientSize.X / 2)
        {
            velocidadeDaBolaEmX = -velocidadeDaBolaEmX;
        }
        if(bola.x - bola.largura / 2.0f < -ClientSize.X / 2)
        {
            velocidadeDaBolaEmX = -velocidadeDaBolaEmX;
        }
        if(bola.y + bola.altura / 2.0f > ClientSize.Y / 2)
        {
            velocidadeDaBolaEmY = -velocidadeDaBolaEmY;
        }
        if (bola.y - bola.altura / 2.0f < -ClientSize.Y / 2)
        {
            velocidadeDaBolaEmY = -velocidadeDaBolaEmY;
        }
        if(bola.x + bola.largura / 2.0f > jogador2.x - jogador2.largura / 2.0f &&
           bola.y - bola.altura / 2.0f < jogador2.y + jogador2.altura / 2.0f &&
           bola.y + bola.altura / 2.0f > jogador2.y - jogador2.altura / 2.0f)
        {
            velocidadeDaBolaEmX = -velocidadeDaBolaEmX;
        }
        if (bola.x - bola.largura / 2.0f < jogador1.x + jogador1.largura / 2.0f &&
           bola.y - bola.altura / 2.0f < jogador1.y + jogador1.altura / 2.0f &&
           bola.y + bola.altura / 2.0f > jogador1.y- jogador1.altura / 2.0f)
        {
            velocidadeDaBolaEmX = -velocidadeDaBolaEmX;
        }
        if(bola.x - bola.largura / 2.0f < -ClientSize.X / 2 || bola.x + bola.largura / 2.0f > ClientSize.X / 2)
        {
            bola.x = 0.0f;
            bola.y = 0.0f;
        }

        if (KeyboardState.IsKeyDown(Keys.W))
        {
            jogador1.y += 500.0f * Time.deltaTime;
        }
        if (KeyboardState.IsKeyDown(Keys.S))
        {
            jogador1.y -= 500.0f * Time.deltaTime;
        }

        if (KeyboardState.IsKeyDown(Keys.Up))
        {
            jogador2.y += 500.0f * Time.deltaTime;
        }
        if (KeyboardState.IsKeyDown(Keys.Down))
        {
            jogador2.y -= 500.0f * Time.deltaTime;
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

        DesenharRetangulo(bola.x, bola.y, bola.largura, bola.altura, 1.0f, 1.0f, 0.0f);
        DesenharRetangulo(jogador1.x, jogador1.y, jogador1.largura, jogador1.altura, 1.0f, 0.0f, 0.0f);
        DesenharRetangulo(jogador2.x, jogador2.y, jogador2.largura, jogador2.altura, 0.0f, 0.0f, 1.0f);

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

    private Retangulo CriarRetangulo(float x, float y, float largura, float altura)
    {
        Retangulo r = new Retangulo();

        r.x = x;
        r.y = y;

        r.largura = largura;
        r.altura = altura;

        return r;
    }
}
