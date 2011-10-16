sampler TextureSampler;

float MaxIntensity =0.5f;

struct PS_INPUT
{
	float2 TexCoord : TEXCOORD0;
};

float4 Heat( PS_INPUT Input ) : COLOR0
{ 

  float3 tc = float3(1.0, 0.0, 0.0);  
    float3 pixcol = tex2D(TextureSampler, Input.TexCoord).rgb;
    float3 colors[3];
    colors[0] = float3(0.,0.,1.);
    colors[1] = float3(1.,1.,0.);
    colors[2] = float3(1.,0.,0.);
    //float lum = (pixcol.r+pixcol.g+pixcol.b)/3.;
	float lum = dot(float3(0.30, 0.59, 0.11), pixcol.rgb);
    int ix = (lum < MaxIntensity)? 0:1;
    tc = lerp(colors[ix],colors[ix+1],(lum-float(ix)*0.5)/0.5);  
	return float4(tc, 1.0);
}


technique UnderWaterTech
{ 
	pass P0
	{ 
		PixelShader = compile ps_2_0 Heat(); 
	} 
}