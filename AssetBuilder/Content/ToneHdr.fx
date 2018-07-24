float2	g_vDestinationDimensions;
float g_fBloomMultiplier;
float g_fThreshold = 0.7f;
float g_fMiddleGrey = 0.6f;
float g_fMaxLuminance = 16.0f;
static const float3 LUM_CONVERT = float3(0.299f, 0.587f, 0.114f);

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


texture2D SourceTexture1;
sampler2D PointSampler1 = sampler_state
{
    Texture = <SourceTexture1>;
    MinFilter = point;
    MagFilter = point;
    MipFilter = point;
    MaxAnisotropy = 1;
    AddressU = CLAMP;
    AddressV = CLAMP;
};

texture2D SourceTexture2;
sampler2D LinearSampler2 = sampler_state
{
    Texture = <SourceTexture2>;
    MinFilter = linear;
    MagFilter = linear;
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

float3 ToneMap(float3 vColor)
{
	// Get the calculated average luminance 
	float fLumAvg = tex2D(PointSampler1, float2(0.5f, 0.5f)).r;	
	
	// Calculate the luminance of the current pixel
	float fLumPixel = dot(vColor, LUM_CONVERT);	
	
	// Apply the modified operator (Eq. 4)
	float fLumScaled = (fLumPixel * g_fMiddleGrey) / fLumAvg;	
	float fLumCompressed = (fLumScaled * (1 + (fLumScaled / (g_fMaxLuminance * g_fMaxLuminance)))) / (1 + fLumScaled);
	return fLumCompressed * vColor;
}



float4 ToneMapPS (	in float2 in_vTexCoord			: TEXCOORD0,
					uniform bool bEncodeLogLuv )	: COLOR0
{
	// Sample the original HDR image
	float4 vSample = tex2D(PointSampler0, in_vTexCoord);
	float3 vHDRColor;
    vHDRColor = vSample.rgb;
		
	// Do the tone-mapping
	float3 vToneMapped = ToneMap(vHDRColor);
	
	// Add in the bloom component
	float3 vColor = vToneMapped + tex2D(LinearSampler2, in_vTexCoord).rgb * g_fBloomMultiplier;
	
	return float4(vColor, 1.0f);
}

technique Tone
{
    pass p0
    {
        VertexShader = compile vs_4_0 PostProcessVS();
        PixelShader = compile ps_4_0 ToneMapPS(false);
        
        ZEnable = false;
        ZWriteEnable = false;
        AlphaBlendEnable = false;        
        StencilEnable = false;
    }
}

