float4x4 World;
float4x4 View;
float4x4 Projection;
float id;

texture Cubemap;
sampler SamplerCubemap = sampler_state
{
	Texture = <Cubemap>;
	MinFilter = LINEAR;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
};


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

struct VertexShaderInput
{
    float4 Position : POSITION0;
    float3 Normal : NORMAL0;
    float2 TexCoord : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;
    float3 Normal : TEXCOORD1;    
    float3 Position3D : TEXCOORD2;
	float3 Vector_refl : TEXCOORD3;	
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);
	output.Position3D = output.Position ;

    output.TexCoord = input.TexCoord;                             //pass the texture coordinates further
    output.Normal =mul(input.Normal,World);                       //get normal into world space            
    
    float3 posicionCamara = mul(-View._m30_m31_m32, transpose(View));    
    output.Vector_refl = reflect(normalize(worldPosition - posicionCamara),normalize(mul(input.Normal,World)));       
    return output;
}


float4 PixelShaderFunctionPerfectMirror(VertexShaderOutput input) : color0
{    
	   input.Normal = normalize(input.Normal);
	   float3 posicionCamara = mul(-View._m30_m31_m32, transpose(View));    
	   float3 dir = normalize(posicionCamara  - input.Position3D);
     
       // determines whether we're processing at the center (1) or the edges (near 0)
       float cosang = abs((dot(dir,input.Normal))); 
	   cosang = pow(cosang, 2); 

       return float4(texCUBE(SamplerCubemap,input.Vector_refl).rgb,cosang);					       
}


technique PerfectMirror
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunctionPerfectMirror();
    }
}

