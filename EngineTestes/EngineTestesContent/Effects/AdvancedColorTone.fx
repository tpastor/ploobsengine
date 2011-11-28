sampler SceneSampler : register(s0);  

float Desat = 0.5f;
float Toned = 1.0f;
float3 LightColor = {1,0.9,0.5};
float3 DarkColor = {0.2,0.05,0};  

struct QuadVertexOutput
{
	float2 UV	: TEXCOORD0;
}; 

float4 sepiaPS(QuadVertexOutput IN) : COLOR0
{   
    float3 scnColor = LightColor * tex2D(SceneSampler, IN.UV).xyz;
    float3 grayXfer = float3(0.3,0.59,0.11);
    float gray = dot(grayXfer,scnColor);
    float3 muted = lerp(scnColor,gray.xxx,Desat);
    float3 sepia = lerp(DarkColor,LightColor,gray);
    float3 result = lerp(muted,sepia,Toned);
    return float4(result,1);
}

technique AdvancedColorTone
{
	pass dpass
	{
		PixelShader = compile ps_2_0 sepiaPS();
	}
}