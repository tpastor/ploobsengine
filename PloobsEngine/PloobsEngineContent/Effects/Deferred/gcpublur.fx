#define RADIUS  7
#define KERNEL_SIZE (RADIUS * 2 + 1)

//-----------------------------------------------------------------------------
// Globals.
//-----------------------------------------------------------------------------

float weights[KERNEL_SIZE];
float2 offsets[KERNEL_SIZE];


float2 halfPixel;
texture2D SourceTexture0;
sampler2D LinearSampler0 = sampler_state
{
    Texture = <SourceTexture0>;
    MinFilter = linear;
    MagFilter = linear;
    MipFilter = linear;    
    AddressU = CLAMP;
    AddressV = CLAMP;
};

float4 DownscalePS (	in float2 in_vTexCoord			: TEXCOORD0)	: COLOR0
{
	float4 color = float4(0.0f, 0.0f, 0.0f, 0.0f);
    
    for (int i = 0; i < KERNEL_SIZE; ++i)
       color += tex2D(LinearSampler0, in_vTexCoord + offsets[i]) * weights[i];
        
    return color;    
}


void PostProcessVS (	in float3 in_vPositionOS		: POSITION,
						in float3 in_vTexCoord			: TEXCOORD0,					
						out float4 out_vPositionCS		: POSITION,
						out float2 out_vTexCoord		: TEXCOORD0	)
{
	out_vPositionCS = float4(in_vPositionOS, 1.0f);		
	out_vTexCoord = in_vTexCoord.xy - halfPixel;
}	




technique TOSCREEN
{
    pass p0
    {
        VertexShader = compile vs_2_0 PostProcessVS();
        PixelShader = compile ps_2_0 DownscalePS();        
    }
}	