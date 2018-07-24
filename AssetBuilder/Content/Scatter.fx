float2 floatPixel;
float4x4 View;
float4x4 WorldViewProjection;
float3 LightPosition;
float3 CameraPos;
float Density = 0.1f;
float Weight = 0.4f;
float Decay = 1.0f;
float Exposition = 0.5f;
float3 LightDirection = {1.0f,1.0f,1.0f};
float abertura = 0.85f;

texture pb;
sampler baseTexture = sampler_state
{
    Texture = (pb);
    AddressU = Clamp;
    AddressV = Clamp;
    MagFilter = Point;
    MinFilter = Point;
    Mipfilter = Point;
};

texture normal;
sampler frameSampler = sampler_state
{
    Texture = (normal);
    AddressU = Clamp;
    AddressV = Clamp;
    MagFilter = Point;
    MinFilter = Point;
    Mipfilter = Point;
};


struct VertexShaderInput
{
    float3 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;
    output.Position = float4(input.Position,1);
    output.TexCoord = input.TexCoord - floatPixel;    
    return output;
}

float4 PixelShaderFunctionNormal(VertexShaderOutput input) : COLOR0
{	
	float4 screenPos = mul(LightPosition, WorldViewProjection);         
    float2 ssPos = screenPos.xy / screenPos.w * float2(0.5,-0.5) + 0.5;
	ssPos.y = 1.0f - ssPos.y;
    
	float2 texCoord = input.TexCoord;
	float2 oriTexCoord = texCoord;			
	float2 deltaTexCoord = (texCoord - ssPos);    
    deltaTexCoord *= 1.0f / 64 * Density;    
    float3 color = tex2D(baseTexture, texCoord);    
    float illuminationDecay = 1.0f;    
	for (int i=0; i < 64; i++)
	{		
		texCoord -= deltaTexCoord;			
		float3 sample = tex2D(baseTexture, texCoord);
		sample *= illuminationDecay * Weight;
		color += sample;			
		illuminationDecay *= Decay;
	}
	
	float amount = dot(mul(LightDirection,View), float3(0.0f,0.0f,-1.f));
	float4 sampleFrame = tex2D(frameSampler, oriTexCoord);    
    ///-0.5 seria o arcos(abertura do sol ne screenspace --> tamanho dele)		
	///only god knows the reason of the above line (cit just work this way, probably a hlsl bug )
	#if DEBUG
    return saturate(amount - abertura) * float4(color * Exposition, 1) + sampleFrame;
	#else
	return clamp(amount - abertura,0,0.999999999999f) * float4(color * Exposition, 1) + sampleFrame;	
	#endif
	
}

technique NormalTechnich
{
    pass Pass1
    {
        VertexShader = compile vs_4_0 VertexShaderFunction();
        PixelShader = compile ps_4_0 PixelShaderFunctionNormal();
    }
}
