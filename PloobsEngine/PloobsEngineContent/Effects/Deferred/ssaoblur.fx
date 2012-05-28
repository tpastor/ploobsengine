
//sampler baseSampler : register(s0);

float2 blurDirection;

texture depthTexture;
float farPlane;

sampler2D DepthMap = sampler_state
{
	Texture = <depthTexture>;
    ADDRESSU = CLAMP;
	ADDRESSV = CLAMP;
	MAGFILTER = LINEAR;
	MINFILTER = LINEAR;
};

texture SSAOTexture;

sampler2D baseSampler = sampler_state
{
	Texture = <SSAOTexture>;
    ADDRESSU = CLAMP;
	ADDRESSV = CLAMP;
	MAGFILTER = LINEAR;
	MINFILTER = LINEAR;
};

texture normal;
sampler2D normalsampler = sampler_state
{
	Texture = <normal>;
    ADDRESSU = WRAP;
	ADDRESSV = WRAP;
	MAGFILTER = LINEAR;
	MINFILTER = LINEAR;
};

float4 PixelShaderFunction(float2 TexCoord :TEXCOORD0) : COLOR0
{
	TexCoord.x += 1.0/1600.0;
	TexCoord.y += 1.0/1200.0;
    float depth = 1-tex2D( DepthMap, TexCoord).r /  farPlane ;
    float3 normal = tex2D( normalsampler, TexCoord).rgb;
    float color = tex2D( baseSampler, TexCoord).r;
   
    float num = 1;
    int blurSamples = 8; 
	
	for( int i = -blurSamples/2; i <= blurSamples/2; i+=1)
	{
		float4 newTexCoord = float4(TexCoord + i * blurDirection.xy, 0, 0);
		
		float sample = tex2D(baseSampler, newTexCoord).r;
		float3 samplenormal = tex2D(normalsampler, newTexCoord).rgb;
			
		if (dot(samplenormal, normal) > 0.99 )	
		{
			num += (blurSamples/2 - abs(i));
			color += sample * (blurSamples/2 - abs(i));
		}
	}

	return color / num;
}

technique SSAOBlur
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}

