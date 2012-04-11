//-----------------------------------------------------------------------------
// DownsampleDepth.fx
//
// Jorge Adriano Luna 2011
// http://jcoluna.wordpress.com
//-----------------------------------------------------------------------------


float2 PixelSize;
float2 HalfPixel;

texture DepthBuffer;
sampler2D depthSampler : register(s0);

struct VertexShaderInput
{
    float4 Position : POSITION0;
	float2 TexCoord : TEXCOORD0;
};


struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float2 TexCoord : TEXCOORD0;
};


VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output = (VertexShaderOutput)0;
	
	output.Position = input.Position;
	output.TexCoord.xy = input.TexCoord + HalfPixel;
	return output;
}
float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    float depth0 = tex2D(depthSampler,input.TexCoord).r;
    float depth1 = tex2D(depthSampler,input.TexCoord + float2(PixelSize.x,0)).r;
    float depth2 = tex2D(depthSampler,input.TexCoord + float2(0,PixelSize.y)).r;
    float depth3 = tex2D(depthSampler,input.TexCoord + PixelSize).r;

	return max(max(depth0,depth1),max(depth2,depth3));
}

technique DownSample
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}


//===============================================================================================
//= These methods perform texture lookups at the four nearest neighbors of the position s and 
//= bilinearly interpolate them. 
//===============================================================================================
float4 bilerp(float2 s)
{
  float4 st;
  st.x = -PixelSize;
  st.y = -PixelSize;
  st.z = PixelSize;
  st.w = PixelSize;
  st.xy+=s;
  st.zw+=s;
  
  float2 t = float2(0.5,0.5);
    
  float4 tex11 = tex2D(depthSampler, st.xy);
  float4 tex21 = tex2D(depthSampler, st.zy);
  float4 tex12 = tex2D(depthSampler, st.xw);
  float4 tex22 = tex2D(depthSampler, st.zw);

  // bilinear interpolation
  return lerp(lerp(tex11, tex21, t.x), lerp(tex12, tex22, t.x), t.y);
}

float4 PixelShaderFunctionColorBilinear(VertexShaderOutput input) : COLOR0
{
	return bilerp(input.TexCoord);
}


technique DownSampleBilinear
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunctionColorBilinear();
    }
}


