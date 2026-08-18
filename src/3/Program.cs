using System.ComponentModel.DataAnnotations;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Prong;

public class Retangulo
{
    public int x;
    public int y;

    public int largura;
    public int altura;
}

public class Program : GameWindow
{
    // configurações
    private const int SCR_WIDTH = 800;
    private const int SCR_HEIGHT = 600;

    private const string _vertexShaderSource = 
    @"
        #version 330 core
        layout (location = 0) in vec3 aPos;

        uniform mat4 projection;
        uniform mat4 model;

        void main()
        {
            gl_Position = projection * model * vec4(aPos.x, aPos.y, aPos.z, 1.0);
        }
    ";

    private const string _fragmentShaderSource =
    @"
        #version 330 core
        out vec4 FragColor;

        uniform vec4 ourColor = vec4(1.0f, 1.0f, 1.0f, 1.0f);

        void main()
        {
            FragColor = ourColor;
        } 
    ";

    private int _shaderProgram;

    private uint _vertexArrayObject;
    private uint _vertexBufferObject;

    private Retangulo bola = null!;

    private int velocidadeDaBolaEmX = 3;
    private int velocidadeDaBolaEmY = 3;

    private Retangulo jogador1 = null!;
    private Retangulo jogador2 = null!;

    private static void Main(string[] args)
    {
        // criação da janela glfw
        // --------------------------------------------------
        GameWindowSettings gws = GameWindowSettings.Default;
        NativeWindowSettings nws = NativeWindowSettings.Default;

        nws.ClientSize = new Vector2i(SCR_WIDTH, SCR_HEIGHT);
        nws.Title = "Prong";
        nws.StartVisible = false;
        nws.Vsync = VSyncMode.On;

        using (Program program = new Program(gws, nws))
        {
            if (OperatingSystem.IsWindows())
            {
                program.CenterWindow();
            }
            program.IsVisible = true;

            program.bola = CriarRetangulo(0, 0, 20, 20);

            int larguraDosJogadores = program.bola.largura;
            int alturaDosJogadores = program.bola.altura * 3;

            program.jogador1 = CriarRetangulo(
                x:       -program.ClientSize.X / 2 + larguraDosJogadores / 2, 
                y:        0, 
                largura:  larguraDosJogadores, 
                altura:   alturaDosJogadores
            );

            program.jogador2 = CriarRetangulo(
                x:        program.ClientSize.X / 2 - larguraDosJogadores / 2, 
                y:        0, 
                largura:  larguraDosJogadores, 
                altura:   alturaDosJogadores
            );

            program.Run();
        }
    }

    public Program(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
    {
        
    }

    protected override void OnLoad()
    {
        // construir e compilar nosso programa de shader
        // --------------------------------------------------

        // vertex shader
        int vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, _vertexShaderSource);
        GL.CompileShader(vertexShader);

        // verificar erros de compilação de shader
        int success;
        string infoLog;

        GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out success);
        if (success == 0)
        {
            GL.GetShaderInfoLog(vertexShader, out infoLog);
            Console.WriteLine("ERROR::SHADER::VERTEX::COMPILATION_FAILED\n" + infoLog);
        }

        // fragment shader
        int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, _fragmentShaderSource);
        GL.CompileShader(fragmentShader);

        // verificar erros de compilação de shader
        GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out success);
        if (success == 0)
        {
            GL.GetShaderInfoLog(fragmentShader, out infoLog);
            Console.WriteLine("ERROR::SHADER::FRAGMENT::COMPILATION_FAILED\n" + infoLog);
        }

        // link shaders
        _shaderProgram = GL.CreateProgram();
        GL.AttachShader(_shaderProgram, vertexShader);
        GL.AttachShader(_shaderProgram, fragmentShader);
        GL.LinkProgram(_shaderProgram);

        // verificar erros de vinculação
        GL.GetProgram(_shaderProgram, GetProgramParameterName.LinkStatus, out success);
        if (success == 0)
        {
            GL.GetProgramInfoLog(_shaderProgram, out infoLog);
            Console.WriteLine("ERROR::SHADER::PROGRAM::LINKING_FAILED\n" + infoLog);
        }

        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);

        // configurar dados de vértice (e buffer(s)) e configurar atributos de vértice
        // --------------------------------------------------
        float[] vertices =
        {
            -0.5f, -0.5f,  0.0f,
             0.5f, -0.5f,  0.0f,
             0.5f,  0.5f,  0.0f,
            -0.5f, -0.5f,  0.0f,
             0.5f,  0.5f,  0.0f,
            -0.5f,  0.5f,  0.0f
        };

        GL.GenVertexArrays(1, out _vertexArrayObject);
        GL.GenBuffers(1, out _vertexBufferObject);

        GL.BindVertexArray(_vertexArrayObject);

        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        GL.BindVertexArray(0);
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        FramebufferSizeCallback(e.Width, e.Height);
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        // input
        // --------------------------------------------------
        ProcessInput();
        
        bola.x = bola.x + velocidadeDaBolaEmX;
        bola.y = bola.y + velocidadeDaBolaEmY;

        if (bola.x + bola.largura / 2 > jogador2.x - jogador2.largura / 2 &&
            bola.y - bola.altura / 2 < jogador2.y + jogador2.altura / 2 &&
            bola.y + bola.altura / 2 > jogador2.y - jogador2.altura / 2)
        {
            velocidadeDaBolaEmX = -velocidadeDaBolaEmX;
        }
        if (bola.x - bola.largura / 2 < jogador1.x + jogador1.largura / 2 &&
            bola.y - bola.altura / 2 < jogador1.y + jogador1.altura / 2 &&
            bola.y + bola.altura / 2 > jogador1.y - jogador1.altura / 2)
        {
            velocidadeDaBolaEmX = -velocidadeDaBolaEmX;
        }
        if (bola.y + bola.altura / 2 > ClientSize.Y / 2)
        {
            velocidadeDaBolaEmY = -velocidadeDaBolaEmY;
        }
        if (bola.y - bola.altura / 2 < -ClientSize.Y / 2)
        {
            velocidadeDaBolaEmY = -velocidadeDaBolaEmY;
        }

        if (bola.x < -ClientSize.X / 2 || bola.x > ClientSize.X / 2)
        {
            bola.x = 0;
            bola.y = 0;
        }

        if (KeyboardState.IsKeyDown(Keys.W))
        {
            jogador1.y = jogador1.y + 5;
        }
        if (KeyboardState.IsKeyDown(Keys.S))
        {
            jogador1.y = jogador1.y - 5;
        }

        if (KeyboardState.IsKeyDown(Keys.Up))
        {
            jogador2.y = jogador2.y + 5;
        }
        if (KeyboardState.IsKeyDown(Keys.Down))
        {
            jogador2.y = jogador2.y - 5;
        }
    }
    
    // loop de renderização
    // --------------------------------------------------
    protected override void OnRenderFrame(FrameEventArgs args)
    {
        // render
        // --------------------------------------------------
        GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        GL.UseProgram(_shaderProgram);

        Matrix4 projection = Matrix4.CreateOrthographic(
            width:     (float)ClientSize.X, 
            height:    (float)ClientSize.Y, 
            depthNear: 0.0f, 
            depthFar:  1.0f
        );
        int projectionLoc = GL.GetUniformLocation(_shaderProgram, "projection");
        GL.UniformMatrix4(projectionLoc, false, ref projection);

        DesenharRetangulo(bola.x, bola.y, bola.largura, bola.altura, 1.0f, 1.0f, 0.0f);
        DesenharRetangulo(jogador1.x, jogador1.y, jogador1.largura, jogador1.altura, 1.0f, 0.0f, 0.0f);
        DesenharRetangulo(jogador2.x, jogador2.y, jogador2.largura, jogador2.altura, 0.0f, 0.0f, 1.0f);

        // glfw: troca os buffers e processa eventos de E/S (teclas pressionadas/liberadas, movimento do mouse, etc.)
        // --------------------------------------------------
        SwapBuffers();
    }

    protected override void OnUnload()
    {
        
    }

    // processar toda a entrada: consultar a GLFW para saber se teclas relevantes foram pressionadas ou liberadas neste quadro e reagir de acordo
    // --------------------------------------------------
    private void ProcessInput()
    {
        if (KeyboardState.IsKeyPressed(Keys.Escape))
        {
            Close();
        }
    }

    // glfw: sempre que o tamanho da janela for alterado (pelo sistema operacional ou redimensionamento pelo usuário), esta função de retorno de chamada é executada.
    // --------------------------------------------------
    private void FramebufferSizeCallback(int width, int height)
    {
        // certifique-se de que a viewport corresponda às novas dimensões da janela; observe que a largura e
        // a altura serão significativamente maiores do que as especificadas em telas Retina.
        GL.Viewport(0, 0, width, height);
    }

    private void DesenharRetangulo(int x, int y, int largura, int altura, float r, float g, float b)
    {
        int colorLoc = GL.GetUniformLocation(_shaderProgram, "ourColor");
        GL.Uniform4(colorLoc, r, g, b, 1.0f);

        Matrix4 model = Matrix4.Identity;
        model *= Matrix4.CreateScale(largura, altura, 0.0f);
        model *= Matrix4.CreateTranslation(x, y, 0.0f);
        int modelLoc = GL.GetUniformLocation(_shaderProgram, "model");
        GL.UniformMatrix4(modelLoc, false, ref model);

        GL.BindVertexArray(_vertexArrayObject);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        GL.BindVertexArray(0);
    }

    private static Retangulo CriarRetangulo(int x, int y, int largura, int altura)
    {
        Retangulo r = new Retangulo();

        r.x = x;
        r.y = y;

        r.largura = largura;
        r.altura = altura;

        return r;
    }
}
