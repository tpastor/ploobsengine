float2 halfPixel;

texture cena;
sampler cenaSampler = sampler_state
{
   Texture = <cena>;   
   MipFilter = POINT;   
   AddressU  = Clamp;
   AddressV  = Clamp;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;    
};

VertexShaderOutput VShader( float4 Pos: POSITION, float2 Tex : TEXCOORD)
{
	VertexShaderOutput output;
    output.Position = float4(Pos);    
	output.Position.x =  output.Position.x - 2*halfPixel.x;
	output.Position.y =  output.Position.y + 2*halfPixel.y;
    output.TexCoord = Tex ;//- halfPixel;    
    return output;
}

float4 PShader(VertexShaderOutput input) : COLOR
{	
    return tex2D(cenaSampler ,input.TexCoord );        
}



technique Normal
{
	pass P0
	{	
		VertexShader = compile vs_3_0 VShader();
		PixelShader = compile ps_3_0 PShader();
	}
}


