float4x4 World;
float4x4 View;
float4x4 Projection;
float4x4 ReflectionView;
float specularIntensity = 0.8f;
float specularPower = 0.5f; 
float FarClip = 2000;
float interpolatorColorFactor = 0.15f;
float id;

Texture normalMap;
sampler NormalSampler = sampler_state 
{ 
	texture = <normalMap> ; 
	magfilter = LINEAR; 
	minfilter = LINEAR; 
	mipfilter = LINEAR; 
	AddressU = mirror; 
	AddressV = mirror;
};

Texture FloorMap;
sampler FloorSampler = sampler_state 
{ 
	texture = <FloorMap> ; 
	magfilter = LINEAR; 
	minfilter = LINEAR; 
	mipfilter = LINEAR; 
	AddressU = mirror; 
	AddressV = mirror;
};

Texture ReflectionMap;
sampler ReflectionSampler = sampler_state 
{ 
	texture = <ReflectionMap> ; 
	magfilter = LINEAR; 
	minfilter = LINEAR; 
	mipfilter = LINEAR; 
	AddressU = mirror; 
	AddressV = mirror;
};


struct VertexShaderInput
{
    float4 Position : POSITION0;
    float3 Normal : NORMAL0;
    float2 TexCoord : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;
    float3 Normal : TEXCOORD1;
    float2 Depth : TEXCOORD2;    
    float4 ReflectionMapSamplingPos : TEXCOORD3;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);

    output.TexCoord = input.TexCoord;                             //pass the texture coordinates further
    output.Normal =mul(input.Normal,World);                       //get normal into world space    
    output.Depth.x = output.Position.z;
    output.Depth.y = output.Position.w;   
    
  	float4x4 preReflectionViewProjection = mul (ReflectionView, Projection);
    float4x4 preWorldReflectionViewProjection = mul (World, preReflectionViewProjection);
    
	output.ReflectionMapSamplingPos = mul(input.Position, preWorldReflectionViewProjection);
	
    return output;
}

struct PixelShaderOutput
{
    half4 Color : COLOR0;
    half4 Normal : COLOR1;
    half4 Depth : COLOR2;
    half4 LightOcclusion : COLOR3;
};


PixelShaderOutput PixelShaderFunctionReflexiveSurfacePerfect(VertexShaderOutput input)
{
	float2 ProjectedTexCoords;
    ProjectedTexCoords.x = input.ReflectionMapSamplingPos.x/input.ReflectionMapSamplingPos.w/2.0f + 0.5f;
    ProjectedTexCoords.y = -input.ReflectionMapSamplingPos.y/input.ReflectionMapSamplingPos.w/2.0f + 0.5f;

    PixelShaderOutput output;   
    output.Normal.rgb = 0.5f * (normalize(input.Normal) + 1.0f);               //transform normal domain
    output.Normal.a = specularPower;                                           //output SpecularPower
    output.Depth = 1-input.Depth.x / input.Depth.y;                              //output Depth  
  
	output.LightOcclusion =  0; 
	output.LightOcclusion.a =  id/ 255.0f; 
	
    float4 reflectiveColor = tex2D(ReflectionSampler, ProjectedTexCoords);
    float4 color = tex2D(FloorSampler, input.TexCoord);
    output.Color = lerp(color, reflectiveColor, interpolatorColorFactor ) ;
    output.Color.a = specularIntensity;  
    return output;
        
}

technique ReflexiveSurfacePerfect
{
    pass Pass1
    {
        VertexShader = compile vs_4_0 VertexShaderFunction();
        PixelShader = compile ps_4_0 PixelShaderFunctionReflexiveSurfacePerfect();
    }
}