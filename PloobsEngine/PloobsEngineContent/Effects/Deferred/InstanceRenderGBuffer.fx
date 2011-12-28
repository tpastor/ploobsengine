float specularIntensity = 0.8f;
float specularPower = 0.5f; 
float id = 0;

texture Texture;
sampler diffuseSampler = sampler_state
{
    Texture = (Texture);
    MAGFILTER = LINEAR;
    MINFILTER = LINEAR;
    MIPFILTER = LINEAR;
    AddressU = Wrap;
    AddressV = Wrap;
};

float4x4 WVP;

struct VertexShaderInput
{
    float4 Position : POSITION0;
    float3 Normal : NORMAL0;
    float2 TexCoord : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4  Position : POSITION0;
    float2 TexCoord : TEXCOORD0;
    float3 Normal : TEXCOORD1;
    float2 Depth : TEXCOORD2; 
    float3 Cor: TEXCOORD3; 
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input, float4x4 instanceTransform : TEXCOORD1, float4 colour : TEXCOORD5)
{
 VertexShaderOutput output;    
 float4 pos = input.Position;
 pos = mul(pos, transpose(instanceTransform));
 output.Position = mul(pos, WVP);
 output.TexCoord = input.TexCoord;	 
 output.Normal = mul(transpose(instanceTransform),input.Normal); 
 output.Depth.x = output.Position.z;
 output.Depth.y = output.Position.w;    
 output.Cor = colour ;
 return output;
}

struct PixelShaderOutput
{
    half4 Color : COLOR0;
    half4 Normal : COLOR1;
    half4 Depth : COLOR2;
    half4 Extra1 : COLOR3;
};

PixelShaderOutput PixelShaderFunctionTexture(VertexShaderOutput input)
{
    PixelShaderOutput output;
    output.Color = tex2D(diffuseSampler, input.TexCoord);					   //output Color
    //output.Color = float4(input.Cor,0);
    output.Color.a = specularIntensity;                                        //output SpecularIntensity
    output.Normal.rgb = 0.5f * (normalize(input.Normal) + 1.0f);               //transform normal domain
    output.Normal.a = specularPower;                                           //output SpecularPower
    output.Depth = input.Depth.x / input.Depth.y;                              //output Depth  
    output.Extra1.rgb =  0;  
    output.Extra1.a =  id;  
        
    return output;
}

PixelShaderOutput PixelShaderFunctionColor(VertexShaderOutput input)
{
    PixelShaderOutput output;
    //output.Color = tex2D(diffuseSampler, input.TexCoord);					   //output Color
    output.Color = float4(input.Cor,0);
    output.Color.a = specularIntensity;                                        //output SpecularIntensity
    output.Normal.rgb = 0.5f * (normalize(input.Normal) + 1.0f);               //transform normal domain
    output.Normal.a = specularPower;                                           //output SpecularPower
    output.Depth = input.Depth.x / input.Depth.y;                              //output Depth  
    output.Extra1.rgb =  0;  
    output.Extra1.a =  id;  
        
    return output;
}


technique TechniqueColor
{
    pass Pass1
    {
        VertexShader = compile vs_3_0 VertexShaderFunction();
        PixelShader = compile ps_3_0 PixelShaderFunctionColor();
    }
}


technique TechniqueTexture
{
    pass Pass1
    {
        VertexShader = compile vs_3_0 VertexShaderFunction();
        PixelShader = compile ps_3_0 PixelShaderFunctionTexture();
    }
}