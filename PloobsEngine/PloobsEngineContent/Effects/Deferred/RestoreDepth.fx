texture ColorTexture;
texture DepthTexture;
float2 halfPixel;

sampler ColorSampler : register(s0) = sampler_state
{
    Texture = (ColorTexture);
    MinFilter = POINT;
    MagFilter = POINT;
    MipFilter = POINT;
    AddressU = CLAMP;
    AddressV = CLAMP;
};

sampler DepthSampler : register(s1) = sampler_state
{
    Texture = (DepthTexture);
    MinFilter = POINT;
    MagFilter = POINT;
    MipFilter = POINT;
    AddressU = CLAMP;
    AddressV = CLAMP;
};


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


void RestoreBuffersPixelShader(in float2 texCoord : TEXCOORD0, 
								 out float4 color: COLOR0,
								 out float depth : DEPTH)
{
	
	//write the color
	color = tex2D(ColorSampler, texCoord);
	
	//write the depth
	depth = tex2D(DepthSampler, texCoord).r;
}

void RestoreBuffersPixelShader2(VertexShaderOutput input ,
								out float4 color: COLOR0,
								out float depth : DEPTH)
{	//write the color
	color = tex2D(ColorSampler, input.TexCoord);
	
	//write the depth
	depth = tex2D(DepthSampler, input.TexCoord).r;
	
}

technique Technique2
{
   pass Pass1
    { 
	VertexShader = compile vs_2_0 Vshader();
	PixelShader = compile ps_2_0 RestoreBuffersPixelShader2();	
	}
}