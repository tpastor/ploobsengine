sampler baseSampler : register(s0);

float Contrast;

float4 PixelShader1( float2 Tex : TEXCOORD ) : COLOR0
{
float3 Color =  tex2D(baseSampler, Tex); 
Color = Color - Contrast * (Color - 1.0f) * Color *(Color - 0.5f);
return float4(Color,0);
}

technique contrast
{
    pass p0
    {     
        PixelShader = compile ps_4_0 PixelShader1();     
    }
}
