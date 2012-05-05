float2 halfPixel;
float4x4 InvertViewProjection;
float4x4 oldViewProjection;
float numSamples = 6;
float attenuation = 0.4;

texture cena;
sampler cenaSampler = sampler_state
{
   Texture = <cena>;   
   AddressU  = Clamp;
   AddressV  = Clamp;
};

texture depth;
sampler depthSampler = sampler_state
{
   Texture = <depth>;
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
    float depthVal = tex2D(depthSampler,input.TexCoord).r;
    float3 cen = tex2D(cenaSampler ,input.TexCoord );

    //compute screen-space position
    float4 position;
    position.x = input.TexCoord.x * 2.0f - 1.0f;
    position.y = -(input.TexCoord.y * 2.0f - 1.0f);
    position.z = depthVal;
    position.w = 1.0f;
	float4 H = position;
    //transform to world space
    position = mul(position, InvertViewProjection);
    position /= position.w;    
	
	   // Current viewport position   
	   float4 currentPos = H;   
	   // Use the world position, and transform by the previous view-   
	   // projection matrix.   
	   float4 previousPos = mul(position, oldViewProjection);   
	   // Convert to nonhomogeneous points [-1,1] by dividing by w.   
	   previousPos /= previousPos.w;   
	   // Use this frame's position and last frame's to compute the pixel   
	   // velocity.   
	   float2 velocity = (currentPos - previousPos)/2.f;  
	   velocity = velocity * attenuation;

	    // Get the initial color at this pixel.   
		float4 color = tex2D(cenaSampler, input.TexCoord);   
		input.TexCoord += velocity;   
		for(int i = 1; i < numSamples && i < 25 ; ++i, input.TexCoord += velocity)   
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