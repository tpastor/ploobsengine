float sampleWeights[15];
float2 sampleOffsets[15];
 
Texture InputTexture;
 
sampler inputTexture = sampler_state 
{ 
    texture = <InputTexture>; 
    magfilter = LINEAR; 
    minfilter = LINEAR; 
    mipfilter = LINEAR; 
};
 
struct VS_OUTPUT
{
	float4 Position	: POSITION;
	float2 TexCoords : TEXCOORD0;
};
 
float4 GaussianBlur_PS (VS_OUTPUT Input) : COLOR0
{
	float4 color = float4(0, 0, 0, 1);
 
	for(int i = 0; i < 15; i++ )
		color += tex2D(inputTexture, Input.TexCoords + sampleOffsets[i]) * sampleWeights[i];
 
	return color;
}
 
technique Blur
{
	pass P0
	{
		PixelShader = compile ps_2_0 GaussianBlur_PS();
	}
}