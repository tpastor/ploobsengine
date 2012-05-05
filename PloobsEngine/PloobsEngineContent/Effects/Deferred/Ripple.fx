// Ripple.fx 

// ###################
// ##### GLOBALS #####

float GlobalAlpha
<
    string UIWidget = "slider";
    string UIMin = "0.0";
    string UIMax = "1.0";
    string UIStep = "0.1";
    string UIName = "Global Alpha";
> = 1.0;

float2 Point
<
    string UIWidget = "slider";
    string UIMin = "-2.0,-2.0";
    string UIMax = "2.0,2.0";
    string UIStep = "0.1,0.1";
    string UIName = "Point";
> = 0.5f;

float RippleSize
<
    string UIWidget = "slider";
    string UIMin = "0.0";
    string UIMax = "1.0";
    string UIStep = "0.1";
    string UIName = "Ripple Size";
> = 0.25f;

float Intensity
<
    string UIWidget = "slider";
    string UIMin = "0.0";
    string UIMax = "1.0";
    string UIStep = "0.1";
    string UIName = "Intensity";
> = 0.25f;


// #######################
// ##### PARAMENTERS #####

sampler samplerState;

// Main pass pixel shader
struct PixelShaderInput
{
	float2 TexCoord        : TEXCOORD0; // interpolated texture coordinates
};


// #######################
// ##### PIXELSHADER #####

// Main pass pixel shader
float4 Pshader(PixelShaderInput input) : COLOR0
{
  	float2 vec = input.TexCoord - Point.xy;
  	float dist = length(vec);
		
  	if (dist > 0.0f)
    	vec /= dist;
		
  	float w = 0.0f;
		
  	if (dist < RippleSize)
  	{
  	    float rippleRadius = (1.0f - dist / RippleSize);
    	w = rippleRadius * Intensity * cos(rippleRadius * 7.853981634);
    }
	
  	float3 pixelColor = tex2D(samplerState, input.TexCoord + (vec * w));
  	
  	// return final pixel color
  	return float4(pixelColor, GlobalAlpha);
}

technique Ripple {
		pass P0{ 
			PixelShader = compile ps_2_0 Pshader(); 
		}
}