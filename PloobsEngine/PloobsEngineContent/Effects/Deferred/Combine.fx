// Pixel shader combines the bloom image with the original
// scene, using tweakable intensity levels and saturation.
// This is the final step in applying a bloom postprocess.

float2 halfPixel;

texture base;
texture last;

sampler2D baseSampler = 
sampler_state
{
	Texture = <base>;
	MinFilter = LINEAR;	
	MagFilter = LINEAR;	
	AddressU = CLAMP;
	AddressV = CLAMP;
};

sampler2D lastSampler = 
sampler_state
{
	Texture = <last>;
	MinFilter = LINEAR;	
	MagFilter = LINEAR;	
	AddressU = CLAMP;
	AddressV = CLAMP;
};



////////////////////////////////////////////////////////
// Suma
////////////////////////////////////////////////////////



struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;    
};
VertexShaderOutput Vshader( float4 Pos: POSITION, float2 Tex : TEXCOORD)
{
	VertexShaderOutput output;
    output.Position = float4(Pos);    
    output.TexCoord = Tex - halfPixel;    
    return output;
}


float4 SUMPS(VertexShaderOutput input) : COLOR0
{
    // Look up the bloom and original base image colors.
    float4 base = tex2D(baseSampler, input.TexCoord);
    float4 last = tex2D(lastSampler, input.TexCoord);
    
    // Combine the two images.
    return base + last;
}


technique SUM
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 Vshader();
        PixelShader = compile ps_2_0 SUMPS();
    }
}

