float4x4 wvp;
texture colorMap;
uniform const bool useTextureAlpha = false;
float alpha;

sampler colorSampler = sampler_state
{
    Texture = (colorMap);
    AddressU = CLAMP;
    AddressV = CLAMP;
    MagFilter = LINEAR;
    MinFilter = LINEAR;
    Mipfilter = LINEAR;
};

struct VertexShaderInput
{
    float3 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;
    
    output.Position = mul(float4(input.Position,1),wvp);
    output.TexCoord = input.TexCoord;    
    return output;
}

float4 PixelShaderFunctionNormal(VertexShaderOutput input) : COLOR0
{		
	if(useTextureAlpha == true )
	{
	return tex2D(colorSampler,input.TexCoord).rgba;
	
	}
	else
	{
    float4 cor = tex2D(colorSampler,input.TexCoord).rgba;
	cor.a = alpha;
	return cor;
    }
}

technique NormalTechnich
{
    pass Pass1
    {
        VertexShader = compile vs_4_0 VertexShaderFunction();
        PixelShader = compile ps_4_0 PixelShaderFunctionNormal();
    }
}
