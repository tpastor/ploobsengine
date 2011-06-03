// RadialBlur.fx 

// ###################
// ##### GLOBALS #####

float2 TexelSize = {0.0009, 0.0013};

float2 Center
<
    string UIWidget = "slider";
    string UIMin = "0.0,0.0";
    string UIMax = "1.0,1.0";
    string UIStep = "0.1,0.1";
    string UIName = "Center";
> = 0.5f;

// absolute alpha value returned for the pixel color
float GlobalAlpha
<
    string UIWidget = "slider";
    string UIMin = "0.0";
    string UIMax = "1.0";
    string UIStep = "0.1";
    string UIName = "Global Alpha";
> = 1.0;

float PixelDistance
<
    string UIWidget = "slider";
    string UIMin = "0.0";
    string UIMax = "0.1";
    string UIStep = "0.01";
    string UIName = "Pixel Distance";
> = 0.01f;

// #######################
// ##### PARAMENTERS #####

sampler samplerState;

struct PixelShaderInput
{
	float2 TexCoord        : TEXCOORD0; // interpolated texture coordinates
};


// #######################
// ##### PIXELSHADER #####

// Main pass pixel shader
float4 PShader(PixelShaderInput input) : COLOR0
{
	float2 s = input.TexCoord + TexelSize * 0.5;
      
    float2 coords[16];
    for ( int i=0; i<16; i++ )
    {
        float scale = 1.0f + PixelDistance*(i * 0.66666f);
        coords[i] = (s - Center) * scale + Center;
    }
    
    float4 final = 0;
    
    for ( int i=0; i<16; i++ )
    {
      final += tex2D(samplerState, coords[i]);
    }
	
    final *= 0.0625f;
      
  	// return final pixel color
  	return float4(final.xyz, GlobalAlpha);
} 

technique RadialBlur {
		pass P0{ 
			PixelShader = compile ps_2_0 PShader(); 
		}
}