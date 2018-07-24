sampler baseSampler : register(s0);

float saturation;

float4 PixelShader1( float2 Tex : TEXCOORD ) : COLOR0
{
	float3 Color =  tex2D(baseSampler, Tex); 
	float Lum = dot(Color,float3(0.2126, 0.7152, 0.0722)); 	
	Color = lerp(Lum.xxx, Color, saturation);	
	return float4(Color,0);
}

technique Saturation
{
    pass p0
    {     
        PixelShader = compile ps_4_0 PixelShader1();     
    }
}
