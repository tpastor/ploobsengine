sampler TextureSampler : register(s0);

float2 Center = { 0.5, 0.5 };
float BlurStart = 1.0f;
float BlurWidth = -0.1;

float4 PS_RadialBlur(float2 UV	: TEXCOORD0 ) : COLOR
{
    UV -= Center;
    float4 c = 0;
    // this loop will be unrolled by compiler and the constants precalculated:
    for(int i=0; i<24; i++) {
    	float scale = BlurStart + BlurWidth*(i/(float) (24-1));
    	c += tex2D(TextureSampler, UV * scale + Center );
   	}
   	c /= 24;
    return c;
} 

technique AdvancedRadialBlur
{
    pass p0
    {
			PixelShader  = compile ps_4_0 PS_RadialBlur();
    }
}