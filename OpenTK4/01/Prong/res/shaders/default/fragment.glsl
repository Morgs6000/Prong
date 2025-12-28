#version 330 core
out vec4 FragColor;

in vec3 ourColor;
in vec2 TexCoord;

uniform bool hasWireframe;
uniform bool hasColor;
uniform bool hasTexture;
uniform bool hasCustomColor;

uniform sampler2D ourTexture;

uniform vec4 uniformColor = vec4(1.0f);

void main()
{
    FragColor = vec4(1.0f);

    if(hasWireframe)
    {
        FragColor *= vec4(0.0f);
    }
    if(hasColor)
    {
        FragColor *= vec4(ourColor, 1.0f);
    }
    if(hasTexture)
    {
        FragColor *= texture(ourTexture, TexCoord);
    }
    if(hasCustomColor)
    {
        FragColor *= uniformColor;
    }
} 
