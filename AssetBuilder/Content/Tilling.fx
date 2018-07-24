// Tiling.fx 

// #######################
// ##### PARAMENTERS #####

sampler samplerState;

struct PixelShaderInput
{
	float2 TexCoord        : TEXCOORD0; // interpolated texture coordinates
};

float NumTiles = 75.0;
float Threshhold = 0.15;

// #######################
// ##### PIXELSHADER #####

// Main pass pixel shader
float4 PShader(PixelShaderInput input) : COLOR0
{
	half3 EdgeColor = {0.7, 0.7, 0.7};

    half size = 1.0/NumTiles;
    half2 Pbase = input.TexCoord - fmod(input.TexCoord, size.xx);
    half2 PCenter = Pbase + (size/2.0).xx;
    half2 st = (input.TexCoord - Pbase)/size;
    half4 c1 = (half4)0;
    half4 c2 = (half4)0;
    half4 invOff = half4((1-EdgeColor),1);
    if (st.x > st.y) { c1 = invOff; }
    half threshholdB =  1.0 - Threshhold;
    if (st.x > threshholdB) { c2 = c1; }
    if (st.y > threshholdB) { c2 = c1; }
    half4 cBottom = c2;
    c1 = (half4)0;
    c2 = (half4)0;
    if (st.x > st.y) { c1 = invOff; }
    if (st.x < Threshhold) { c2 = c1; }
    if (st.y < Threshhold) { c2 = c1; }
    half4 cTop = c2;
    half4 tileColor = tex2D(samplerState, PCenter);
    half4 result = tileColor + cTop - cBottom;
    return result;
} 

technique Tiling {
		pass P0{ 
			PixelShader = compile ps_4_0 PShader(); 
		}
}