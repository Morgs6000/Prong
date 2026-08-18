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

        void main()
        {
            FragColor = vec4(1.0f, 1.0f, 1.0f, 1.0f);
        } 
    ";

    private int _shaderProgram;

    private uint _vertexArrayObject;
    private uint _vertexBufferObject;

    private static void Main(string[] args)
    {
        // criação da janela glfw
        // --------------------------------------------------
        GameWindowSettings gws = GameWindowSettings.Default;
        NativeWindowSettings nws = NativeWindowSettings.Default;

        nws.ClientSize = new Vector2i(SCR_WIDTH, SCR_HEIGHT);
        nws.Title = "Prong";
        nws.StartVisible = false;

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

        DesenharRetangulo(0, 0, 20, 20);
        DesenharRetangulo(-390, 0, 20, 40);
        DesenharRetangulo(390, 0, 20, 40);

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

    private void DesenharRetangulo(int x, int y, int largura, int altura)
    {
        Matrix4 model = Matrix4.Identity;
        model *= Matrix4.CreateScale(largura, altura, 0.0f);
        model *= Matrix4.CreateTranslation(x, y, 0.0f);
        int modelLoc = GL.GetUniformLocation(_shaderProgram, "model");
        GL.UniformMatrix4(modelLoc, false, ref model);

        GL.BindVertexArray(_vertexArrayObject);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        GL.BindVertexArray(0);
    }
}
