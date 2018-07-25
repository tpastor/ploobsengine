
#include "TextureMacros.fxh"


float2 halfPixel;

DECLARE_TEXTURE(Color,0);
DECLARE_TEXTURE(Depth,1);


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
	color = SAMPLE_TEXTURE(Color, input.TexCoord);
	
	//write the depth
	depth = 1-SAMPLE_TEXTURE(Depth, input.TexCoord).r;
	
}

technique Technique2
{
   pass Pass1
    { 
	VertexShader = compile vs_4_0 Vshader();
	PixelShader = compile ps_4_0 RestoreBuffersPixelShader2();	
	}
}