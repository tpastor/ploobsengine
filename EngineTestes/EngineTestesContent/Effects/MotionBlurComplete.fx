float2 halfPixel;
float numSamples = 6;

texture cena;
sampler cenaSampler = sampler_state
{
   Texture = <cena>;   
   AddressU  = Clamp;
   AddressV  = Clamp;
};

texture velocity;
sampler depthSampler = sampler_state
{
   Texture = <velocity>;
   MinFilter = POINT;
   MagFilter = POINT;
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
	Pos.x =  Pos.x - 2*halfPixel.x;
	Pos.y =  Pos.y + 2*halfPixel.y;
    output.Position = float4(Pos);    
    output.TexCoord = Tex;    
    return output;
}

float4 Pshader(VertexShaderOutput input) : COLOR
{	    
		float2 velocity = tex2D(depthSampler,input.TexCoord).rg;
		velocity = velocity * 2.0f - 1.0f;		   

	    // Get the initial color at this pixel.   
		float4 color = tex2D(cenaSampler, input.TexCoord);   
		input.TexCoord += velocity;   
		for(int i = 1; i < numSamples && i < 50 ; ++i, input.TexCoord += velocity)   
		{   
		  // Sample the color buffer along the velocity vector.   
		   float4 currentColor = tex2D(cenaSampler, input.TexCoord);   
		  // Add the current color to our color sum.   
		  color += currentColor;   
		}   
		// Average all of the samples to get the final blur color.   
		return color / numSamples ;  
}


technique Shader
{
	pass P0
	{	
		VertexShader = compile vs_3_0 VShader();
		PixelShader = compile ps_3_0 Pshader();
	}
}