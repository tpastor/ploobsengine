sampler ColorMapSampler : register(s0);

float fTimer;


float fNoiseAmount;


int iSeed;


float4 PShader(float2 Tex: TEXCOORD0) : COLOR
{
	// Distortion factor
	float NoiseX = iSeed * fTimer * sin(Tex.x * Tex.y+fTimer);
	NoiseX=fmod(NoiseX,8) * fmod(NoiseX,4);	

	// Use our distortion factor to compute how much it will affect each
	// texture coordinate
	float DistortX = fmod(NoiseX,fNoiseAmount);
	float DistortY = fmod(NoiseX,fNoiseAmount+0.002);
	
	// Create our new texture coordinate based on our distortion factor
	float2 DistortTex = float2(DistortX,DistortY);
	
	// Use our new texture coordinate to look-up a pixel in ColorMapSampler.
	float4 Color=tex2D(ColorMapSampler, Tex+DistortTex);
	
	// Keep our alphachannel at 1.
	Color.a = 1.0f;

    return Color;
}

technique PostProcess
{
	pass P0
	{
		// A post process shader only needs a pixel shader.
		PixelShader = compile ps_4_0 PShader();
	}
}