using System.ComponentModel.DataAnnotations;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Prong;

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

    private int xDaBola = 0;
    private int yDaBola = 0;
    private int tamanhoDaBola = 20;
    private int velocidadeDaBolaEmX = 3;
    private int velocidadeDaBolaEmY = 3;

    private int yDoJogador1 = 0;
    private int yDoJogador2 = 0;

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
        
        xDaBola = xDaBola + velocidadeDaBolaEmX;
        yDaBola = yDaBola + velocidadeDaBolaEmY;

        if (xDaBola + tamanhoDaBola / 2 > xDoJogador2() - larguraDosJogadores() / 2 &&
            yDaBola - tamanhoDaBola / 2 < yDoJogador2 + alturaDosJogadores() / 2 &&
            yDaBola + tamanhoDaBola / 2 > yDoJogador2 - alturaDosJogadores() / 2)
        {
            velocidadeDaBolaEmX = -velocidadeDaBolaEmX;
        }
        if (xDaBola - tamanhoDaBola / 2 < xDoJogador1() + larguraDosJogadores() / 2 &&
            yDaBola - tamanhoDaBola / 2 < yDoJogador1 + alturaDosJogadores() / 2 &&
            yDaBola + tamanhoDaBola / 2 > yDoJogador1 - alturaDosJogadores() / 2)
        {
            velocidadeDaBolaEmX = -velocidadeDaBolaEmX;
        }
        if (yDaBola + tamanhoDaBola / 2 > ClientSize.Y / 2)
        {
            velocidadeDaBolaEmY = -velocidadeDaBolaEmY;
        }
        if (yDaBola - tamanhoDaBola / 2 < -ClientSize.Y / 2)
        {
            velocidadeDaBolaEmY = -velocidadeDaBolaEmY;
        }

        if (xDaBola < -ClientSize.X / 2 || xDaBola > ClientSize.X / 2)
        {
            xDaBola = 0;
            yDaBola = 0;
        }

        if (KeyboardState.IsKeyDown(Keys.W))
        {
            yDoJogador1 = yDoJogador1 + 5;
        }
        if (KeyboardState.IsKeyDown(Keys.S))
        {
            yDoJogador1 = yDoJogador1 - 5;
        }

        if (KeyboardState.IsKeyDown(Keys.Up))
        {
            yDoJogador2 = yDoJogador2 + 5;
        }
        if (KeyboardState.IsKeyDown(Keys.Down))
        {
            yDoJogador2 = yDoJogador2 - 5;
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

        DesenharRetangulo(xDaBola, yDaBola, tamanhoDaBola, tamanhoDaBola, 1.0f, 1.0f, 0.0f);
        DesenharRetangulo(xDoJogador1(), yDoJogador1, larguraDosJogadores(), alturaDosJogadores(), 1.0f, 0.0f, 0.0f);
        DesenharRetangulo(xDoJogador2(), yDoJogador2, larguraDosJogadores(), alturaDosJogadores(), 0.0f, 0.0f, 1.0f);

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

    private int xDoJogador1()
    {
        return -ClientSize.X / 2 + larguraDosJogadores() / 2;
    }

    private int xDoJogador2()
    {
        return ClientSize.X / 2 - larguraDosJogadores() / 2;
    }

    private int larguraDosJogadores()
    {
        return tamanhoDaBola;
    }

    private int alturaDosJogadores()
    {
        return 3 * tamanhoDaBola;
    }
}
