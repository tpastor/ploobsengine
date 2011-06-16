sampler TextureSampler;


struct PixelInput
{
 float2 TexCoord : TEXCOORD0;
};


float4 Sharpen(PixelInput input) : COLOR
{
float sharpAmount = 15.0f;
float4 color = tex2D( TextureSampler, input.TexCoord);
color += tex2D( TextureSampler, input.TexCoord - 0.0001) * sharpAmount;
color -= tex2D( TextureSampler, input.TexCoord + 0.0001) * sharpAmount;
return( color );
}


technique SharpenTech
{
 pass P0
 {
  PixelShader = compile ps_2_0 Sharpen();
 }
}