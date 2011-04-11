float4x4 World;
float4x4 View;
float4x4 Projection;
float4 clippingPlane; 

Texture diffuse;
sampler DiffuseSampler = sampler_state 
{ 
	texture = <diffuse> ; 
	magfilter = LINEAR; 
	minfilter = LINEAR; 
	mipfilter = LINEAR; 
	AddressU = clamp; 
	AddressV = clamp;
};


struct VertexShaderInput
{
    float3 Position : POSITION0;
	float4 texturecoord :  TEXCOORD0;    
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float4 texturecoord :  TEXCOORD0;
	float4 clipping :  TEXCOORD1;    
};


VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(float4(input.Position,1), World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);
	output.texturecoord = input.texturecoord;
	output.clipping = 0;	
	float4 clp = output.Position;
	//clp  /= clp.w;
	output.clipping.x = dot(clp,clippingPlane) ;
    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    clip(input.clipping.x);
	    
    return tex2D(DiffuseSampler, input.texturecoord);
}

technique Technique1
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
