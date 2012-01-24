sampler TextureSampler;

texture backtex;
sampler backSamples = sampler_state
{
   Texture = <backtex>;   
   MipFilter = LINEAR;   
   AddressU  = Clamp;
   AddressV  = Clamp;
};

texture extrart;
sampler extrartSampler = sampler_state
{
   Texture = <extrart>;   
   MipFilter = POINT;   
   AddressU  = Clamp;
   AddressV  = Clamp;
};

struct PS_INPUT
{
	float2 TexCoord : TEXCOORD0;
};

float4 backtextureps( PS_INPUT Input ) : COLOR0
{
    float4 Color;
    float2 tex = Input.TexCoord;

	if (tex2D( extrartSampler, tex).a > 0.95f)
	{
		Color = tex2D( backSamples, tex);
	}
	else
	{
		Color = tex2D( TextureSampler, tex);
	}	
	
    return Color;
}

technique Tech
{ 
	pass P0
	{ 
		PixelShader = compile ps_2_0 backtextureps(); 
	} 
}