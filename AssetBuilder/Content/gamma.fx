sampler baseSampler : register(s0);

float4 PixelShaderv( float2 Tex : TEXCOORD ) : COLOR0
{
	float3 color =  tex2D(baseSampler, Tex); 
    return float4(pow(color, 1/2.2f),0);
}

float4 PixelShaderx( float2 Tex : TEXCOORD ) : COLOR0
{
	float3 color =  tex2D(baseSampler, Tex); 
    return float4(pow(color, 2.2f),0);

}

float4 PixelShader1( float2 Tex : TEXCOORD ) : COLOR0
{
	float3 Color =  tex2D(baseSampler, Tex); 
	Color = (Color <= 0.00304) ? Color * 12.92 : (1.055 * pow(Color, 1.0/2.4) - 0.055);
	return float4(Color,0);
}

float4 PixelShader2( float2 Tex : TEXCOORD ) : COLOR0
{
	float3 Color =  tex2D(baseSampler, Tex); 
	Color = pow(1.055 * Color, 1.0/2.4)- 0.055;
	return float4(Color,0);
}


float4 PixelShader3( float2 Tex : TEXCOORD ) : COLOR0
{
	float3 Color =  tex2D(baseSampler, Tex); 	
	Color = ((Color <= 0.03928) ? Color / 12.92 : pow((Color + 0.055) / 1.055, 2.4));
	return float4(Color,0);
}


float4 PixelShader4( float2 Tex : TEXCOORD ) : COLOR0
{
	float3 Color =  tex2D(baseSampler, Tex); 
	Color = pow((Color + 0.055)/ 1.055, 2.4);
	return float4(Color,0);
}




//-----------------------------------------------------------------------------
// Technique: PostProcess
// Desc: Performs post-processing effect that converts a colored image to
//       black and white.
//-----------------------------------------------------------------------------
technique Gamma1
{
    pass p0
    {     
        PixelShader = compile ps_4_0 PixelShader1();     
    }
}

technique Gamma2
{
    pass p0
    {     
        PixelShader = compile ps_4_0 PixelShader2();     
    }
}

technique Gamma3
{
    pass p0
    {     
        PixelShader = compile ps_4_0 PixelShader3();     
    }
}

technique Gamma4
{
    pass p0
    {     
        PixelShader = compile ps_4_0 PixelShader4();     
    }
}

technique Gamma5
{
    pass p0
    {     
        PixelShader = compile ps_4_0 PixelShaderv();     
    }
}

technique Gamma6
{
    pass p0
    {     
        PixelShader = compile ps_4_0 PixelShaderx();     
    }
}