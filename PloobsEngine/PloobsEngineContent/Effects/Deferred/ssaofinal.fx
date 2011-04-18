const float2 halfPixel = 0.5f /float2(800,600);
const float weight = 1;

texture SceneTexture;

sampler2D baseSampler = sampler_state
{
	Texture = <SceneTexture>;
    ADDRESSU = CLAMP;
	ADDRESSV = CLAMP;	
};

texture SSAOTex;

sampler2D SSAOSampler = sampler_state
{
	Texture = <SSAOTex>;
    ADDRESSU = CLAMP;
	ADDRESSV = CLAMP;
	MAGFILTER = POINT;
	MINFILTER = POINT;
};

void PostProcessVS (	in float3 in_vPositionOS		: POSITION,
						in float3 in_vTexCoord			: TEXCOORD0,					
						out float4 out_vPositionCS		: POSITION,
						out float2 out_vTexCoord		: TEXCOORD0	)
{
	out_vPositionCS = float4(in_vPositionOS, 1.0f);	
	out_vTexCoord = in_vTexCoord.xy - halfPixel;
}	

float4 PixelShaderFunction(float2 TexCoord :TEXCOORD0) : COLOR0
{	
	return tex2D( SSAOSampler, TexCoord ) * (tex2D(baseSampler,TexCoord) ) * weight;    
	//return float4(0,1,0,1);
}


technique Merge
{
    pass Pass1
    {
		VertexShader = compile vs_2_0 PostProcessVS();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}