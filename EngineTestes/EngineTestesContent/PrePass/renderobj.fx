float4x4 World;
float4x4 View;
float4x4 Projection;
float FarClip;

struct VertexShaderInput
{
    float3 Position : POSITION0;
    float3 Normal : NORMAL0;
    float2 TexCoord : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position			: POSITION0;
    float2 TexCoord			: TEXCOORD0;
    float Depth				: TEXCOORD1;
	float3 Normal			: TEXCOORD2;	
};

VertexShaderOutput RenderToGBufferVertexCommon(VertexShaderInput input) 
{
    VertexShaderOutput output = (VertexShaderOutput)0;	
	float4 pos = float4(input.Position,1);

	float4x4 worldViewProjection = mul(mul(World ,View ) , Projection);
	float4x4 worldview =  mul(World ,View);	
	float3 viewSpacePos = mul( pos, worldview );
	output.Depth = viewSpacePos.z; 

    output.Position = mul(pos, worldViewProjection);
    output.TexCoord.xy = input.TexCoord; 	
	output.Normal = mul(input.Normal,worldview );
    return output;
}

//render to our 2 render targets, normal and depth 
struct PixelShaderOutput
{
    float4 Normal : COLOR0;
    float4 Depth : COLOR1;
};

PixelShaderOutput RenderToGBufferPixelShader(VertexShaderOutput input)
{
	PixelShaderOutput output = (PixelShaderOutput)1;   
    output.Normal.rgb =  (normalize(input.Normal) + 1) * 0.5f;	
	output.Depth.x = -input.Depth/ FarClip;		
	return output;
}

technique RenderToGBuffer
{
    pass RenderToGBufferPass
    {
	    VertexShader = compile vs_3_0 RenderToGBufferVertexCommon();
        PixelShader = compile ps_3_0 RenderToGBufferPixelShader();
    }
}
