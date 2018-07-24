
float2 halfPixel;

sampler ColorSampler : register(s0) ;
sampler DepthSampler : register(s1) ;

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;
};



VertexShaderOutput Vshader( float4 Pos: POSITION, float2 Tex : TEXCOORD)
{
	VertexShaderOutput output;
	Pos.x =  Pos.x - 2*halfPixel.x;
	Pos.y =  Pos.y + 2*halfPixel.y;
    output.Position = float4(Pos);        
    output.TexCoord = Tex ;
    return output;
}

void RestoreBuffersPixelShader2(VertexShaderOutput input ,
								out float4 color: COLOR0,
								out float depth : DEPTH)
{	//write the color
	color = tex2D(ColorSampler, input.TexCoord);
	
	//write the depth
	depth = 1-tex2D(DepthSampler, input.TexCoord).r;
	
}

technique Technique2
{
   pass Pass1
    { 
	VertexShader = compile vs_4_0 Vshader();
	PixelShader = compile ps_4_0 RestoreBuffersPixelShader2();	
	}
}