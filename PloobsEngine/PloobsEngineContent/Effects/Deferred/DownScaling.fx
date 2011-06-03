float2	g_vSourceDimensions;
float2	g_vDestinationDimensions;

texture2D SourceTexture0;
sampler2D PointSampler0 = sampler_state
{
    Texture = <SourceTexture0>;
    MinFilter = point;
    MagFilter = point;
    MipFilter = point;
    MaxAnisotropy = 1;
    AddressU = CLAMP;
    AddressV = CLAMP;
};

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




static const float g_vOffsets[4] = {-1.5f, -0.5f, 0.5f, 1.5f};


// Downscales to 1/16 size, using 16 samples
float4 DownscalePS (	in float2 in_vTexCoord			: TEXCOORD0, uniform bool bDecodeLuminance	)	: COLOR0
{
	float4 vColor = 0;
	for (int x = 0; x < 4; x++)
	{
		for (int y = 0; y < 4; y++)
		{
			float2 vOffset;
			vOffset = float2(g_vOffsets[x], g_vOffsets[y]) / g_vSourceDimensions;
			float4 vSample = tex2D(PointSampler0, in_vTexCoord + vOffset);
			vColor += vSample;
		}
	}

	vColor /= 16.0f;
	if (bDecodeLuminance)
		vColor = float4(exp(vColor.r), exp(vColor.r), exp(vColor.r), exp(vColor.r));	
	return vColor;
}



void PostProcessVS (	in float3 in_vPositionOS		: POSITION,
						in float3 in_vTexCoord			: TEXCOORD0,					
						out float4 out_vPositionCS		: POSITION,
						out float2 out_vTexCoord		: TEXCOORD0	)
{
	out_vPositionCS = float4(in_vPositionOS, 1.0f);
	float2 vOffset = 0.5f / g_vDestinationDimensions;
	out_vTexCoord = in_vTexCoord.xy - vOffset;
}	

// Upscales or downscales using hardware bilinear filtering
float4 HWScalePS (	in float2 in_vTexCoord			: TEXCOORD0	)	: COLOR0
{
	return tex2D(LinearSampler0, in_vTexCoord);
}

technique Downscale4
{
    pass p0
    {
        VertexShader = compile vs_2_0 PostProcessVS();
        PixelShader = compile ps_2_0 DownscalePS(false);
        
        ZEnable = false;
        ZWriteEnable = false;
        AlphaBlendEnable = false;
        
        StencilEnable = false;
    }
}

///NAo funciona em alguns pcs como o meu q nao conseguie filtrar texturas em ponto flutuante (tipo as halfvector4)
technique ScaleHW
{
    pass p0
    {
        VertexShader = compile vs_2_0 PostProcessVS();
        PixelShader = compile ps_2_0 HWScalePS();
        
        ZEnable = false;
        ZWriteEnable = false;
        AlphaBlendEnable = false;        
        StencilEnable = false;
    }
}

technique Downscale4Luminance
{
    pass p0
    {
        VertexShader = compile vs_2_0 PostProcessVS();
        PixelShader = compile ps_2_0 DownscalePS(true);
        
        ZEnable = false;
        ZWriteEnable = false;
        AlphaBlendEnable = false;        
        StencilEnable = false;
    }
}	