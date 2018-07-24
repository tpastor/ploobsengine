struct PS_INPUT
{
   float2 texcoord0:     TEXCOORD0;
};


texture opacityMap;
sampler opacitySampler = sampler_state
{
   Texture = <opacityMap>;
   MinFilter = Linear;
   MagFilter = Linear;
   MipFilter = Linear;   
   AddressU  = Clamp;
   AddressV  = Clamp;
};

sampler samplerState: register(s0);

float4 PShader( float2 texCoord: TEXCOORD0 ) : COLOR
{
    float4 op = tex2D( opacitySampler, texCoord );
    float4 color = tex2D( samplerState, texCoord );
	return color * op;
	
}

technique RenderScene
{
    pass p0
    {
        PixelShader = compile ps_4_0 PShader();
    }
}

