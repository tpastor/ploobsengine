texture SceneTexture;
//sampler SceneTextureSampler = sampler_state
//{
    //Texture = (SceneTexture);
    //AddressU = WRAP;
    //AddressV = WRAP;
    //MagFilter = POINT;
    //MinFilter = POINT;
    //Mipfilter = POINT;
//};
//
texture DistortionMap;
//sampler DistortionMapSampler = sampler_state
//{
    //Texture = (DistortionMap);
    //AddressU = CLAMP;
    //AddressV = CLAMP;
    //MagFilter = POINT;
    //MinFilter = POINT;
    //Mipfilter = POINT;
//};
//
//struct VertexShaderInput
//{
    //float3 Position : POSITION0;
    //float2 TexCoord : TEXCOORD0;
//};
//
//struct VertexShaderOutput
//{
    //float4 Position : POSITION0;
    //float2 TexCoord : TEXCOORD0;
//};
//
//float2 halfPixel;
//VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
//{
    //VertexShaderOutput output;
	//input.Position.x =  input.Position.x - 2*halfPixel.x;
	//input.Position.y =  input.Position.y + 2*halfPixel.y;
    //output.Position = float4(input.Position,1);
    //output.TexCoord = input.TexCoord ;    
    //return output;
//}
//
//#define SAMPLE_COUNT 15
//float2 SampleOffsets[SAMPLE_COUNT];
//float SampleWeights[SAMPLE_COUNT];
//
//// The Distortion map represents zero displacement as 0.5, but in an 8 bit color
//// channel there is no exact value for 0.5. ZeroOffset adjusts for this error.
//const float ZeroOffset = 0.5f / 255.0f;
//
//float4 Distort_PixelShader(VertexShaderOutput input, 
    //uniform bool distortionBlur) : COLOR0
//{
    //// Look up the displacement
    //float2 displacement = tex2D(DistortionMapSampler, input.TexCoord).rg;		
    //
    //float4 finalColor = 0;
    //// We need to constrain the area potentially subjected to the gaussian blur to the
    //// distorted parts of the scene texture.  Therefore, we can sample for the color
    //// we used to clear the distortion map (black).  We used 0 to avoid any potential
    //// rounding errors.
    //if ((displacement.x == 0) && (displacement.y == 0))
    //{
        //finalColor = tex2D(SceneTextureSampler, input.TexCoord);
    //}
    //else
    //{
        //// Convert from [0,1] to [-.5, .5) 
        //// .5 is excluded by adjustment for zero
        //displacement -= .5 + ZeroOffset;
//
        //if (distortionBlur)
        //{
            //// Combine a number of weighted displaced-image filter taps
            //for (int i = 0; i < SAMPLE_COUNT; i++)
            //{
                //finalColor += tex2D(SceneTextureSampler, input.TexCoord + displacement + 
                    //SampleOffsets[i]) * SampleWeights[i];
            //}
        //}
        //else
        //{
            //// Look up the displaced color, without multisampling
            //finalColor = tex2D(SceneTextureSampler, input.TexCoord + displacement);  
        //}
    //}
//
    //return finalColor;
//}
//
//
//technique DistortBlur
//{
    //pass
    //{
	    //VertexShader = compile vs_2_0 VertexShaderFunction();
        //PixelShader = compile ps_2_0 Distort_PixelShader(false);
    //}
//}

//-----------------------------------------------------------------------------
// Distort.fx
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

//sampler SceneTexture : register(s0);
//sampler DistortionMap : register(s1);
//
sampler SceneTextureSampler = sampler_state
{
    Texture = (SceneTexture);
    AddressU = WRAP;
    AddressV = WRAP;
    MagFilter = POINT;
    MinFilter = POINT;
    Mipfilter = POINT;
};
sampler DistortionMapSampler = sampler_state
{
    Texture = (DistortionMap);
    AddressU = CLAMP;
    AddressV = CLAMP;
    MagFilter = POINT;
    MinFilter = POINT;
    Mipfilter = POINT;
};


#define SAMPLE_COUNT 15
float2 SampleOffsets[SAMPLE_COUNT];
float SampleWeights[SAMPLE_COUNT];

// The Distortion map represents zero displacement as 0.5, but in an 8 bit color
// channel there is no exact value for 0.5. ZeroOffset adjusts for this error.
const float ZeroOffset = 0.5f / 255.0f;

float4 Distort_PixelShader(float2 TexCoord : TEXCOORD0, 
    uniform bool distortionBlur) : COLOR0
{
    // Look up the displacement
    float2 displacement = tex2D(DistortionMapSampler, TexCoord).rg;
    
    float4 finalColor = 0;
    // We need to constrain the area potentially subjected to the gaussian blur to the
    // distorted parts of the scene texture.  Therefore, we can sample for the color
    // we used to clear the distortion map (black).  We used 0 to avoid any potential
    // rounding errors.
    if ((displacement.x == 0) && (displacement.y == 0))
    {
        finalColor = tex2D(SceneTextureSampler, TexCoord);
    }
    else
    {
        // Convert from [0,1] to [-.5, .5) 
        // .5 is excluded by adjustment for zero
        displacement -= .5 + ZeroOffset;

        if (distortionBlur)
        {
            // Combine a number of weighted displaced-image filter taps
            for (int i = 0; i < SAMPLE_COUNT; i++)
            {
                finalColor += tex2D(SceneTextureSampler, TexCoord.xy + displacement + 
                    SampleOffsets[i]) * SampleWeights[i];
            }
        }
        else
        {
            // Look up the displaced color, without multisampling
            finalColor = tex2D(SceneTextureSampler, TexCoord.xy + displacement);  			
        }
    }

    return finalColor;
}

technique Distort
{
    pass
    {
        PixelShader = compile ps_2_0 Distort_PixelShader(true);
    }
}
