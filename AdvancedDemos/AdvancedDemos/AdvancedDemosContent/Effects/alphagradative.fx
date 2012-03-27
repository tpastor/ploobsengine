float4x4 wvp;
float4x4 w;
texture colorMap;
float3 camCenter;

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
	float3 Normal : Normal0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;
	float4 Position3D : TEXCOORD1;
	float3 Normal : TEXCOORD2;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;
    
    output.Position = mul(float4(input.Position,1),wvp);
	output.Position3D = output.Position ;
    output.TexCoord = input.TexCoord;    
	output.Normal     = (mul(input.Normal, (float3x3) w));
    return output;
}

float4 PixelShaderFunctionNormal(VertexShaderOutput input) : COLOR0
{		
       input.Normal = normalize(input.Normal);
	   float3 dir = normalize(camCenter - input.Position3D);
     
       // determines whether we're processing at the center (1) or the edges (near 0)
       float cosang = abs((dot(dir,input.Normal))); 
	   cosang = pow(cosang, 2); 
	   return float4(tex2D(colorSampler,input.TexCoord).rgb,cosang );
	
}

technique NormalTechnich
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunctionNormal();
    }
}
