struct PS_INPUT
{
   float2 texcoord0:     TEXCOORD0;
};

sampler samplerState; 

float4 PShader( float2 texCoord: TEXCOORD0 ) : COLOR
{
    float4 color = tex2D( samplerState, texCoord );	
	color = 1- color;
	return color;
	
}

technique RenderScene
{
    pass p0
    {
        PixelShader = compile ps_4_0 PShader();
    }
}

