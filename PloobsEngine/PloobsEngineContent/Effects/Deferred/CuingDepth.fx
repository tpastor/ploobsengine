float2 halfPixel;
float Z_Near;
float Z_Far;
float4x4 InvertViewProjection;
float3 cameraPosition;

texture depth;
sampler depthSampler = sampler_state
{
    Texture = (depth);
    AddressU = CLAMP;
    AddressV = CLAMP;
    MagFilter = POINT;
    MinFilter = POINT;
    Mipfilter = POINT;
};

texture color;
sampler colorSampler = sampler_state
{
    Texture = (color);
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
    output.Position = float4(input.Position,1);
    output.TexCoord = input.TexCoord - halfPixel;    
    return output;
}

float4 PixelShaderFunctionNormal(VertexShaderOutput input) : COLOR0
{	
	float depthVal = tex2D(depthSampler,input.TexCoord).r;
    float4 c = tex2D(colorSampler ,input.TexCoord );

    //compute screen-space position
    float4 position;
    position.x = input.TexCoord.x * 2.0f - 1.0f;
    position.y = -(input.TexCoord.y * 2.0f - 1.0f);
    position.z = depthVal;
    position.w = 1.0f;
    //transform to world space
    position = mul(position, InvertViewProjection);
    position /= position.w;
    
	float d = length(position - cameraPosition);	
	float l = saturate((d-Z_Near)/(Z_Far-Z_Near));    


	float avg = (c.r + c.g + c.b) * 0.33f;
	float4 gray = float4(avg,avg,avg,1);
	return c * (1 - l ) + gray * l ;
}

technique NormalTechnich
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunctionNormal();
    }
}
