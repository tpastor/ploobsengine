float2 halfPixel;
float dz;
float4x4 InvertViewProjection;
float3 cameraPosition;
float far;
float near;
float3 fcolor = {0.5, 0.5, 0.5};

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
    float depthVal = 1-tex2D(depthSampler,input.TexCoord).r;
    float3 cen = tex2D(cenaSampler ,input.TexCoord );

    //compute screen-space position
    float4 position;
    position.x = input.TexCoord.x * 2.0f - 1.0f;
    position.y = -(input.TexCoord.x * 2.0f - 1.0f);
    position.z = depthVal;
    position.w = 1.0f;
    //transform to world space
    position = mul(position, InvertViewProjection);
    position /= position.w;
    
	float d = length(position - cameraPosition);
	float l = saturate((d-near)/(far-near));    
    return float4(lerp(cen,fcolor, l),1);    	
}

float4 PixelShaderExponencial(VertexShaderOutput input) : COLOR
{	    
    float depthVal = tex2D(depthSampler,input.TexCoord).r;
    float3 cen = tex2D(cenaSampler ,input.TexCoord );

    //compute screen-space position
    float4 position;
    position.x = input.TexCoord.x * 2.0f - 1.0f;
    position.y = -(input.TexCoord.y * 2.0f - 1.0f);
    position.z = depthVal;
    position.w = 1.0f;
    //transform to world space
    position = mul(position, InvertViewProjection);
    position /= position.w;
    
	float d = length(position - cameraPosition);
	float l = exp(-d * dz);
	l = saturate(1 - l);
	
    return float4(lerp(cen,fcolor, l),1);    	
}

float4 PixelShaderExponencialSquared(VertexShaderOutput input) : COLOR
{	    
    float depthVal = tex2D(depthSampler,input.TexCoord).r;
    float3 cen = tex2D(cenaSampler ,input.TexCoord );

    //compute screen-space position
    float4 position;
    position.x = input.TexCoord.x * 2.0f - 1.0f;
    position.y = -(input.TexCoord.y * 2.0f - 1.0f);
    position.z = depthVal;
    position.w = 1.0f;
    //transform to world space
    position = mul(position, InvertViewProjection);
    position /= position.w;
    
	float d = length(position - cameraPosition);
	float l = exp( - pow( d * dz , 2 ) );
	l = saturate(1 - l);
	
    return float4(lerp(cen,fcolor, l),1);    	
}


technique FogShader
{
	pass P0
	{	
		VertexShader = compile vs_4_0 VShader();
		PixelShader = compile ps_4_0 Pshader();
	}
}

technique FogExponencialSquaredShader
{
	pass P0
	{	
		VertexShader = compile vs_4_0 VShader();
		PixelShader = compile ps_4_0 PixelShaderExponencialSquared();
	}
}


technique FogExponencialShader
{
	pass P0
	{	
		VertexShader = compile vs_4_0 VShader();
		PixelShader = compile ps_4_0 PixelShaderExponencial();
	}
}


