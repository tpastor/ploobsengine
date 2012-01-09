sampler postTex;
float4 vecSkill1;
float4 vecTime;


struct PS_INPUT
{
	float2 TexCoord : TEXCOORD0;
};

float4 UnderWater( PS_INPUT Input ) : COLOR0
{
	float4 Color;
	
	Input.TexCoord.xy -= 0.5;
	Input.TexCoord.xy *= 1-(sin(vecTime.w*vecSkill1[2])*vecSkill1[3]+vecSkill1[3])*0.5;
	Input.TexCoord.xy += 0.5;
	
	Color = tex2D( postTex, Input.TexCoord.xy);

	Color += tex2D( postTex, Input.TexCoord.xy+0.001*vecSkill1[0]);
	Color += tex2D( postTex, Input.TexCoord.xy+0.003*vecSkill1[0]);
	Color += tex2D( postTex, Input.TexCoord.xy+0.005*vecSkill1[0]);
	Color += tex2D( postTex, Input.TexCoord.xy+0.007*vecSkill1[0]);
	Color += tex2D( postTex, Input.TexCoord.xy+0.009*vecSkill1[0]);
	Color += tex2D( postTex, Input.TexCoord.xy+0.011*vecSkill1[0]);

	Color += tex2D( postTex, Input.TexCoord.xy-0.001*vecSkill1[0]);
	Color += tex2D( postTex, Input.TexCoord.xy-0.003*vecSkill1[0]);
	Color += tex2D( postTex, Input.TexCoord.xy-0.005*vecSkill1[0]);
	Color += tex2D( postTex, Input.TexCoord.xy-0.007*vecSkill1[0]);
	Color += tex2D( postTex, Input.TexCoord.xy-0.009*vecSkill1[0]);
	Color += tex2D( postTex, Input.TexCoord.xy-0.011*vecSkill1[0]);

	Color.rgb = (Color.r+Color.g+Color.b)/3.0f;

	Color /= vecSkill1[1];
	Color.a = 1; 
	return Color;

}

technique UnderWaterTech
{ 
	pass P0
	{ 
		PixelShader = compile ps_2_0 UnderWater(); 
	} 
}