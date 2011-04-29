float2 halfPixel;
float4x4 View;
float4x4 WorldViewProjection;
float3 LightPosition;
float3 CameraPos;
int numSamples;
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
    output.TexCoord = input.TexCoord - halfPixel;    
    return output;
}

float4 PixelShaderFunctionNormal(VertexShaderOutput input) : COLOR0
{	
	half4 screenPos = mul(LightPosition, WorldViewProjection);         
    half2 ssPos = screenPos.xy / screenPos.w * float2(0.5,-0.5) + 0.5;
	ssPos.y = 1.0f - ssPos.y;
    
	half2 texCoord = input.TexCoord;
	half2 oriTexCoord = texCoord;			
	half2 deltaTexCoord = (texCoord - ssPos);    
    deltaTexCoord *= 1.0f / numSamples * Density;    
    half3 color = tex2D(baseTexture, texCoord);    
    half illuminationDecay = 1.0f;    
	for (int i=0; i < numSamples; i++)
	{		
		texCoord -= deltaTexCoord;			
		half3 sample = tex2D(baseTexture, texCoord);
		sample *= illuminationDecay * Weight;
		color += sample;			
		illuminationDecay *= Decay;
	}
	
	half amount = dot(mul(LightDirection,View), half3(0.0f,0.0f,-1.f));
	half4 sampleFrame = tex2D(frameSampler, oriTexCoord);    
    ///-0.5 seria o arcos(abertura do sol ne screenspace --> tamanho dele)
    return saturate(amount -abertura) * half4(color * Exposition, 1) + sampleFrame;
}

technique NormalTechnich
{
    pass Pass1
    {
        VertexShader = compile vs_3_0 VertexShaderFunction();
        PixelShader = compile ps_3_0 PixelShaderFunctionNormal();
    }
}
