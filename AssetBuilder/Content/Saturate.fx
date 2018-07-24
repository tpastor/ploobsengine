float2 halfPixel;
float BloomThreshold;
texture current;
sampler2D currentSampler = 
sampler_state
{
	Texture = <current>;
	AddressU = CLAMP;
	AddressV = CLAMP;
};


struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;    
};
VertexShaderOutput VShader( float4 Pos: POSITION, float2 Tex : TEXCOORD)
{
	VertexShaderOutput output;
    output.Position = float4(Pos);    
    output.TexCoord = Tex - halfPixel;    
    return output;
}


float4 PShader(float2 texCoord : TEXCOORD0) : COLOR0
{
    // Look up the original image color.
    float4 c = tex2D(currentSampler , texCoord);

    // Adjust it to keep only values brighter than the specified threshold.
    return saturate((c - BloomThreshold) / (1 - BloomThreshold));
}


technique BloomExtract
{
    pass Pass1
    {
          VertexShader = compile vs_4_0 VShader();
        PixelShader = compile ps_4_0 PShader();
    }
}
