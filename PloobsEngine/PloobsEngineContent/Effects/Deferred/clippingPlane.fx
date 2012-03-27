float4x4 WVP;
float4 clippingPlane; 
bool isClip;

sampler DiffuseSampler : register(s0);


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
    output.Position = mul(float4(input.Position,1), WVP);
	output.texturecoord = input.texturecoord;
	output.clipping = 0;	
	float4 clp = output.Position;	
	output.clipping.x = dot(clp,clippingPlane) ;
    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	if(isClip)
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
