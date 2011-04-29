bool useLight;

texture TexColor;
sampler TexColorSampler = sampler_state
{
   Texture = <TexColor>;   
   AddressU  = Clamp;
   AddressV  = Clamp;
};


texture ColorMap;
sampler ColorMapSampler = sampler_state
{
   Texture = <ColorMap>;
   MinFilter = point;
   MagFilter = point;
   MipFilter = point;      
   AddressU  = Clamp;
   AddressV  = Clamp;
};

// The texture that contains the celmap
texture CelMap;
sampler2D CelMapSampler = sampler_state
{
	Texture	  = <CelMap>;
	MIPFILTER = LINEAR;
	MAGFILTER = LINEAR;
	MINFILTER = LINEAR;
};

// The texture that contains the the light Map !!!
// Tenso !!!
texture LightMap;
sampler2D LightMapSampler = sampler_state
{
	Texture	  = <LightMap>;
	MIPFILTER = LINEAR;
	MAGFILTER = LINEAR;
	MINFILTER = LINEAR;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;
};

float2 halfPixel;

VertexShaderOutput VShader( float4 Pos: POSITION, float2 Tex : TEXCOORD)
{
	VertexShaderOutput output;
    output.Position = float4(Pos);
    
    output.TexCoord = Tex - halfPixel;
    return output;
}

float4 PShader(VertexShaderOutput input) : COLOR
{	
	
	float2 cor = input.TexCoord;	
	float4 color = tex2D(ColorMapSampler, cor);
	float4 light = tex2D(LightMapSampler, cor);	
	float4 texcolor = tex2D(TexColorSampler, cor);	
	
	
	cor.y = 0.0f;
	cor.x = light.x / texcolor.x ;		
	float4 CelColor = tex2D(CelMapSampler, cor);		
	
	if(useLight == true)
	{
	return (color*CelColor);
	}
	else
	{	
	return (texcolor * CelColor);
	}
}

technique ToonShader
{
	pass P0
	{	
		VertexShader = compile vs_2_0 VShader();
		PixelShader = compile ps_2_0 PShader();
	}
}