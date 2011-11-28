// GaussianBlur.fx 

// #######################
// ##### PARAMENTERS #####

sampler TextureSampler : register(s0);

#define SAMPLE_COUNT 7

float2 SampleOffsets[SAMPLE_COUNT];
float SampleWeights[SAMPLE_COUNT];

// #######################
// ##### PIXELSHADER #####

float4 Pshader(float2 texCoord : TEXCOORD0) : COLOR0
{
    float4 c = 0;
    
    // Combine a number of weighted image filter taps.
    for (int i = 0; i < SAMPLE_COUNT; i++)
    {
        c += tex2D(TextureSampler, texCoord + SampleOffsets[i]) * SampleWeights[i];
    }
    
    return c;
}

// #######################
// ##### TECHNIQUES ######

technique GaussianBlur
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 Pshader();
    }
}
