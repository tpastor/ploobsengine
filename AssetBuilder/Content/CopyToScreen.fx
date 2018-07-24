float2	g_vSourceDimensions;
float2	g_vDestinationDimensions;

texture2D SourceTexture0;
sampler2D LinearSampler0 = sampler_state
{
    Texture = <SourceTexture0>;
    MinFilter = linear;
    MagFilter = linear;
    MipFilter = point;
    MaxAnisotropy = 1;
    AddressU = CLAMP;
    AddressV = CLAMP;
};

// Downscales to 1/16 size, using 16 samples
float4 DownscalePS (	in float2 in_vTexCoord			: TEXCOORD0)	: COLOR0
{
	float4 vSample = tex2D(LinearSampler0, in_vTexCoord);
	return vSample;
}


void PostProcessVS (	in float3 in_vPositionOS		: POSITION,
						in float3 in_vTexCoord			: TEXCOORD0,					
						out float4 out_vPositionCS		: POSITION,
						out float2 out_vTexCoord		: TEXCOORD0	)
{
	out_vPositionCS = float4(in_vPositionOS, 1.0f);
	float2 vOffset = 0.5f / g_vDestinationDimensions;
	out_vTexCoord = in_vTexCoord.xy + vOffset;
}	




technique TOSCREEN
{
    pass p0
    {
        VertexShader = compile vs_4_0 PostProcessVS();
        PixelShader = compile ps_4_0 DownscalePS();
        
        ZEnable = false;
        ZWriteEnable = false;
        AlphaBlendEnable = false;        
        StencilEnable = false;
    }
}	