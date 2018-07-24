texture ScreenTexture;

texture AATexture;
texture NonAATexture;
float WeightFactor;

sampler ScreenSampler =
sampler_state
{
	Texture = <ScreenTexture>;
	MipFilter = LINEAR;
	MinFilter = LINEAR;
	MagFilter = LINEAR;
};

sampler AASampler =
sampler_state
{
	Texture = <AATexture>;
	MipFilter = POINT;
	MinFilter = POINT;
	MagFilter = POINT;
};
sampler NonAASampler =
sampler_state
{
	Texture = <NonAATexture>;
	MipFilter = POINT;
	MinFilter = POINT;
	MagFilter = POINT;
};

//--------------------------------------------------------------------------------------
// GBuffer generation
//--------------------------------------------------------------------------------------

struct VS_GBUFFEROUTPUT
{
	float4 Pos : POSITION;
	float3 Normal: TEXCOORD2;
	float2 Tex : TEXCOORD0;
	float4 ViewPos: TEXCOORD1;
	
};

struct PS_GBUFFEROUTPUT
{
	float4 Col0 : COLOR0;
	float4 Col1 : COLOR1;
	float4 Col2 : COLOR2;
};


float4 DeferredAAPS(float2 Tex:TEXCOORD0):COLOR0
{
	float4 Col;
	float3 NonAA;
	float2 Offset = float2(1/800, 1/600)*0.75;
	NonAA = abs(normalize(tex2D(NonAASampler, Tex-Offset).rgb));
	float3 AAResult = tex2D(AASampler, Tex).rgb;
	
	float Weight;
	float3 Term1, Term2;
	Term1 = AAResult-NonAA;
	
	Weight =  saturate(length(Term1))*WeightFactor; // The weightfactor is set in the main program so I can turn it on and off.
	
	Col.rgb =	tex2D(ScreenSampler, Tex+Offset).rgb * Weight+
				tex2D(ScreenSampler, Tex-Offset).rgb * (1-Weight);
				
	Col.a = 1;
//	Col.rgb = Weight;
	return Col;

}


//--------------------------------------------------------------------------------------
// Technique Summary
//--------------------------------------------------------------------------------------

technique DeferredAA
{
	pass P0
	{
		PixelShader = compile ps_4_0 DeferredAAPS();
	}
}