float2 halfPixel;

texture EXTRA;
sampler depthSampler = sampler_state
{
    Texture = (EXTRA);
    AddressU = CLAMP;
    AddressV = CLAMP;
    MagFilter = POINT;
    MinFilter = POINT;    
};

texture color;
sampler colorSampler = sampler_state
{
    Texture = (color);
    AddressU = CLAMP;
    AddressV = CLAMP;
    MagFilter = anisotropic;
    MinFilter = anisotropic;    
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
    output.Position = float4(input.Position,1);
    output.TexCoord = input.TexCoord - halfPixel;    
    return output;
}

float4 PixelShaderFunctionNormal(VertexShaderOutput input) : COLOR0
{	
	int depthVal = round(tex2D(depthSampler,input.TexCoord).a * 255 );
    float4 c = tex2D(colorSampler ,input.TexCoord );        
	bool isBackGround = fmod(depthVal, 4) >= 2; 
    
    if(isBackGround)
    {
       return c;
    }
    else
    {
    return 0;
    }
	
}

technique NormalTechnich
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunctionNormal();
    }
}
