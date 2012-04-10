sampler TextureSampler;
float sharpAmount = 15.0f;
float amount;

struct PixelInput
{
	float2 TexCoord : TEXCOORD0;
};

float4 Sharpen(PixelInput input) : COLOR
{
	
	float4 color = tex2D( TextureSampler, input.TexCoord);
	color += tex2D( TextureSampler, input.TexCoord - amount) * sharpAmount;
	color -= tex2D( TextureSampler, input.TexCoord + amount) * sharpAmount;
	color.a = 1;
	return( color );
}

technique SharpenTech
{
	 pass P0
	 {
		 PixelShader = compile ps_2_0 Sharpen();
	 }
}