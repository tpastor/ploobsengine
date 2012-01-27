sampler TextureSampler;

float timervalue=1.0;

struct PS_INPUT
{
	float2 TexCoord : TEXCOORD0;
};

float4 UnderWater( PS_INPUT Input ) : COLOR0
{
    float4 Color;
    float2 tex = Input.TexCoord;
    tex.y = tex.y + (sin(timervalue)*0.01);
    tex.x = tex.x + (cos(timervalue)*0.01);
    Color = tex2D( TextureSampler, tex);
    return Color;
}

technique UnderWaterTech
{ 
	pass P0
	{ 
		PixelShader = compile ps_2_0 UnderWater(); 
	} 
}