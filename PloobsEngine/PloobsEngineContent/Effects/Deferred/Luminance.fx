float2	g_vSourceDimensions;
float2	g_vDestinationDimensions;
float g_fDT;
texture2D SourceTexture0;
texture2D SourceTexture1;

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

static const float3 LUM_CONVERT = float3(0.299f, 0.587f, 0.114f);


void PostProcessVS (	in float3 in_vPositionOS		: POSITION,
						in float3 in_vTexCoord			: TEXCOORD0,					
						out float4 out_vPositionCS		: POSITION,
						out float2 out_vTexCoord		: TEXCOORD0	)
{
	out_vPositionCS = float4(in_vPositionOS, 1.0f);
	float2 vOffset = 0.5f / g_vDestinationDimensions;
	out_vTexCoord = in_vTexCoord.xy + vOffset;
}	

float4 LuminancePS (	in float2 in_vTexCoord			: TEXCOORD0
						 )	: COLOR0
{						
    float4 vSample = tex2D(LinearSampler0, in_vTexCoord);
    float3 vColor;
    vColor = vSample.rgb;
   
    // calculate the luminance using a weighted average
    float fLuminance = dot(vColor, LUM_CONVERT);
                
    float fLogLuminace = log(1e-5 + fLuminance); 
        
    // Output the luminance to the render target
    return float4(fLogLuminace, 1.0f, 0.0f, 0.0f);
}

float4 CalcAdaptedLumPS (in float2 in_vTexCoord		: TEXCOORD0)	: COLOR0
{
	float fLastLum = tex2D(PointSampler1, float2(0.5f, 0.5f)).r;
    float fCurrentLum = tex2D(PointSampler0, float2(0.5f, 0.5f)).r;
    
    // Adapt the luminance using Pattanaik's technique
    const float fTau = 0.5f;
    float fAdaptedLum = fLastLum + (fCurrentLum - fLastLum) * (1 - exp(-g_fDT * fTau));
    
    return float4(fAdaptedLum, 1.0f, 1.0f, 1.0f);
}


technique CalcAdaptedLuminance
{
    pass p0
    {
        VertexShader = compile vs_2_0 PostProcessVS();
        PixelShader = compile ps_2_0 CalcAdaptedLumPS();
        
        ZEnable = false;
        ZWriteEnable = false;
        AlphaBlendEnable = false;        
        StencilEnable = false;
    }
}


technique Luminance
{
    pass p0
    {
        VertexShader = compile vs_2_0 PostProcessVS();
        PixelShader = compile ps_2_0 LuminancePS();        
        ZEnable = false;
        ZWriteEnable = false;                
        StencilEnable = false;
    }

}