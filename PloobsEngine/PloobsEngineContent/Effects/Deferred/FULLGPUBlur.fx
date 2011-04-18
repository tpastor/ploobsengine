float2	g_vSourceDimensions;
float2	g_vDestinationDimensions;
float g_fSigma = 0.5f;


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


void PostProcessVS (	in float3 in_vPositionOS		: POSITION,
						in float3 in_vTexCoord			: TEXCOORD0,					
						out float4 out_vPositionCS		: POSITION,
						out float2 out_vTexCoord		: TEXCOORD0	)
{
	out_vPositionCS = float4(in_vPositionOS, 1.0f);
	float2 vOffset = 0.5f / g_vDestinationDimensions;
	out_vTexCoord = in_vTexCoord.xy - vOffset;
}	



float CalcGaussianWeight(int iSamplePoint)
{
	float g = 1.0f / sqrt(2.0f * 3.14159 * g_fSigma * g_fSigma);  
	return (g * exp(-(iSamplePoint * iSamplePoint) / (2 * g_fSigma * g_fSigma)));
}

float4 GaussianBlurH (	in float2 in_vTexCoord			: TEXCOORD0,
						uniform int iRadius,
						uniform int bEncodeLogLuv		)	: COLOR0
{
    float4 vColor = 0;
	float2 vTexCoord = in_vTexCoord;

    for (int i = -iRadius; i < iRadius; i++)
    {   
		float fWeight = CalcGaussianWeight(i);
		vTexCoord.x = in_vTexCoord.x + (i / g_vSourceDimensions.x);
		float4 vSample = tex2D(PointSampler0, vTexCoord);
		vColor += vSample * fWeight;
    }
	
	return vColor;
}

float4 GaussianBlurV (	in float2 in_vTexCoord			: TEXCOORD0,
						uniform int iRadius,
						uniform int bEncodeLogLuv		)	: COLOR0
{
    float4 vColor = 0;
	float2 vTexCoord = in_vTexCoord;

    for (int i = -iRadius; i < iRadius; i++)
    {   
		float fWeight = CalcGaussianWeight(i);
		vTexCoord.y = in_vTexCoord.y + (i / g_vSourceDimensions.y);
		float4 vSample = tex2D(PointSampler0, vTexCoord);
		vColor += vSample * fWeight;
    }

    return vColor;
}

technique GaussianBlurX
{
    pass p0
    {
        VertexShader = compile vs_2_0 PostProcessVS();
        PixelShader = compile ps_2_0 GaussianBlurH(6, false);
        
        ZEnable = false;
        ZWriteEnable = false;
        StencilEnable = false;
        AlphaBlendEnable = false;        
    }
}

technique GaussianBlurY
{
    pass p0
    {
        VertexShader = compile vs_2_0 PostProcessVS();
        PixelShader = compile ps_2_0 GaussianBlurV(6, false);
        
        ZEnable = false;
        ZWriteEnable = false;
        AlphaBlendEnable = false;        
        StencilEnable = false;
    }
}


