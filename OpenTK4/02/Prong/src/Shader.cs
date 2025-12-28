using System.Text.RegularExpressions;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Prong.common;

namespace Prong;

public class Shader
{
    private int ID;
    
    public Shader(string vertexPath, string fragmentPath)
    {
        string vShaderCode = RemoveComments(File.ReadAllText(vertexPath));
        string fShaderCode = RemoveComments(File.ReadAllText(fragmentPath));

        int vertex, fragment;
        int success;
        string infoLog;

        vertex = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertex, vShaderCode);
        GL.CompileShader(vertex);

        GL.GetShader(vertex, ShaderParameter.CompileStatus, out success);
        if (success == 0)
        {
            GL.GetShaderInfoLog(vertex, out infoLog);
            Debug.LogError("ERROR::SHADER::VERTEX::COMPILATION_FAILED\n" + infoLog);
        }

        fragment = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragment, fShaderCode);
        GL.CompileShader(fragment);

        GL.GetShader(fragment, ShaderParameter.CompileStatus, out success);
        if (success == 0)
        {
            GL.GetShaderInfoLog(fragment, out infoLog);
            Debug.LogError("ERROR::SHADER::FRAGMENT::COMPILATION_FAILED\n" + infoLog);
        }

        ID = GL.CreateProgram();

        GL.AttachShader(ID, vertex);
        GL.AttachShader(ID, fragment);
        GL.LinkProgram(ID);

        GL.GetProgram(ID, GetProgramParameterName.LinkStatus, out success);
        if (success == 0)
        {
            GL.GetProgramInfoLog(ID, out infoLog);
            Debug.LogError("ERROR::SHADER::PROGRAM::LINKING_FAILED\n" + infoLog);
        }

        GL.DeleteShader(vertex);
        GL.DeleteShader(fragment);
    }

    public void Use()
    {
        GL.UseProgram(ID);
    }

    public int GetUniformLocation(string name)
    {
        return GL.GetUniformLocation(ID, name);
    }

    public void SetBool(string name, bool value)
    {
        int location = GetUniformLocation(name);
        GL.Uniform1(location, value ? 1 : 0);
    }

    public void SetInt(string name, int value)
    {
        int location = GetUniformLocation(name);
        GL.Uniform1(location, value);
    }

    public void SetFloat(string name, float value)
    {
        int location = GetUniformLocation(name);
        GL.Uniform1(location, value);
    }

    public void SetMatrix4(string name, Matrix4 matrix)
    {
        int location = GetUniformLocation(name);
        GL.UniformMatrix4(location, true, ref matrix);
    }

    public void SetVector4(string name, Vector4 vector)
    {
        int location = GetUniformLocation(name);
        GL.Uniform4(location, vector);
    }

    public void SetVector4(string name, float x, float y, float z, float w)
    {
        int location = GetUniformLocation(name);
        GL.Uniform4(location, x, y, z, w);
    }

    // Método para remover comentários de código GLSL
    private string RemoveComments(string code)
    {
        if (string.IsNullOrEmpty(code))
            return code;

        // Remover comentários de linha única (// comentário)
        string pattern = @"//.*?$";
        string result = Regex.Replace(code, pattern, "", RegexOptions.Multiline);
        
        // Remover comentários multi-linha (/* comentário */)
        pattern = @"/\*.*?\*/";
        result = Regex.Replace(result, pattern, "", RegexOptions.Singleline);
        
        // Remover linhas vazias extras para limpeza
        result = Regex.Replace(result, @"^\s*$\n", "", RegexOptions.Multiline);
        
        return result.Trim();
    }
}
