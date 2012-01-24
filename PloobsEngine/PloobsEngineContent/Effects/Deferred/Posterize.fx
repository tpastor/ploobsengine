// Posterize.fx 

// #######################
// ##### PARAMENTERS #####

sampler RT;

// Main pass pixel shader
struct PixelShaderInput
{
	float2 TexCoord        : TEXCOORD0; 
};


// #######################
// ##### PIXELSHADER #####

// Main pass pixel shader
float4 PShader(PixelShaderInput input) : COLOR0
{
float nColors = 8;
	float gamma = 0.6;

	float4 texCol = tex2D(RT, input.TexCoord);
	float3 tc = texCol.xyz;
	tc = pow(tc, gamma);
	tc = tc * nColors;
	tc = floor(tc);
	tc = tc / nColors;
	tc = pow(tc,1.0/gamma);
	return float4(tc,texCol.w);
}

technique Posterize {
		pass P0{ 
			PixelShader = compile ps_2_0 PShader(); 
		}
}