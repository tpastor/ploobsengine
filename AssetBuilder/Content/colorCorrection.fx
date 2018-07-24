sampler baseSampler : register(s0);

float3 ColorAdd;
float ColorCorrect;
float ColorToAddIntensity;

float4 PixelShader1( float2 Tex : TEXCOORD ) : COLOR0
{
	float3 Color =  tex2D(baseSampler, Tex); 
	
	Color = Color * ColorCorrect * 2.0 + ColorAdd * ColorToAddIntensity;
	return float4(Color,0);
}

technique Saturation
{
    pass p0
    {     
        PixelShader = compile ps_4_0 PixelShader1();     
    }
}
