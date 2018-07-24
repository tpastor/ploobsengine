struct PS_INPUT
{
   float2 texcoord0:     TEXCOORD0;
};

sampler samplerState; 

float4 PShader( float2 texCoord: TEXCOORD0 ) : COLOR
{
    float4 color = tex2D( samplerState, texCoord );
	float4 gris = (0.3*color.r) + (0.55*color.g) + (0.15*color.b);
	color.r = gris;
	color.g = gris;
	color.b = gris;
	
	return color;
	
}

technique RenderScene
{
    pass p0
    {
        PixelShader = compile ps_4_0 PShader();
    }
}

