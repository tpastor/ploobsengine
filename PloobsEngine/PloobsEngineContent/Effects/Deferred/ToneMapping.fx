
float Luminance = 0.08f;
static const float fMiddleGray = 0.18f;
static const float fWhiteCutoff = 0.8f;

sampler baseSampler : register(s0);

//-----------------------------------------------------------------------------
// Pixel Shader: ToneMapFilter
// Desc: Perform a tone map filter on the source texture
//-----------------------------------------------------------------------------
float4 ToneMapFilter( float2 Tex : TEXCOORD ) : COLOR0
{
    float4 Color;
    Color = tex2D( baseSampler , Tex ) * fMiddleGray / ( Luminance + 0.001f );
    Color *= ( 1.0f + ( Color / ( fWhiteCutoff * fWhiteCutoff ) ) );
    Color /= ( 1.0f + Color );
    return Color;    
}




//-----------------------------------------------------------------------------
// Technique: PostProcess
// Desc: Performs post-processing effect that converts a colored image to
//       black and white.
//-----------------------------------------------------------------------------
technique PostProcess
<
    string Parameter0 = "Luminance";
    float4 Parameter0Def = float4( 0.08f, 0, 0, 0 );
    int Parameter0Size = 1;
    string Parameter0Desc = " (float)";
>
{
    pass p0
    {     
        PixelShader = compile ps_2_0 ToneMapFilter();     
    }
}
