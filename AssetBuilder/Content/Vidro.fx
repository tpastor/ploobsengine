float4x4 World;
float4x4 View;
float4x4 Projection;
float4x4 ReflectionView;
float4x4 RefractionView;
float specularIntensity = 0.8f;
float specularPower = 0.5f; 
float xWaveLength;
float xWaveHeight;
float3 camPos;
float colorInterpolator = 0.5f;

Texture difuse;
sampler difuseSampler = sampler_state 
{ 
	texture = <difuse> ; 
	magfilter = LINEAR; 
	minfilter = LINEAR; 
	mipfilter = LINEAR; 
	AddressU = Clamp; 
	AddressV = Clamp;
};


Texture normalMap;
sampler NormalSampler = sampler_state 
{ 
	texture = <normalMap> ; 
	magfilter = LINEAR; 
	minfilter = LINEAR; 
	mipfilter = LINEAR; 
	AddressU = Clamp; 
	AddressV = Clamp;
};

Texture RefractionMap;
sampler RefractionSampler = sampler_state 
{ 
	texture = <RefractionMap> ; 
	magfilter = LINEAR; 
	minfilter = LINEAR; 
	mipfilter = LINEAR; 
	AddressU = Clamp; 
	AddressV = Clamp;
};

Texture ReflectionMap;
sampler ReflectionSampler = sampler_state 
{ 
	texture = <ReflectionMap> ; 
	magfilter = LINEAR; 
	minfilter = LINEAR; 
	mipfilter = LINEAR; 
	AddressU = Clamp; 
	AddressV = Clamp;
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
    float4 RefractionMapSamplingPos : TEXCOORD4;
    float3 Pos3D : TEXCOORD5;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);

    output.TexCoord = input.TexCoord;                             
    output.Normal = mul(input.Normal,World);                       
    output.Depth.x = output.Position.z;
    output.Depth.y = output.Position.w;   
    output.Pos3D = worldPosition;
    
  	float4x4 preReflectionViewProjection = mul (ReflectionView, Projection);
    float4x4 preWorldReflectionViewProjection = mul (World, preReflectionViewProjection);    
	output.ReflectionMapSamplingPos = mul(input.Position, preWorldReflectionViewProjection);
	
	float4x4 preRefractionViewProjection = mul (RefractionView, Projection);
    float4x4 preWorldRefractionViewProjection = mul (World, preRefractionViewProjection);    
	output.RefractionMapSamplingPos = mul(input.Position, preWorldRefractionViewProjection);	
	
    return output;
}

struct PixelShaderOutput
{
    half4 Color : COLOR0;
    half4 Normal : COLOR1;
    half4 Depth : COLOR2;
    half4 LightOcclusion : COLOR3;
};


PixelShaderOutput PixelShaderFunctionReflexiveSurface(VertexShaderOutput input)
{
	float4 color = tex2D(difuseSampler, input.TexCoord ) ;

	float2 ProjectedTexCoordsReflection;
    ProjectedTexCoordsReflection.x = input.ReflectionMapSamplingPos.x/input.ReflectionMapSamplingPos.w/2.0f + 0.5f;
    ProjectedTexCoordsReflection.y = -input.ReflectionMapSamplingPos.y/input.ReflectionMapSamplingPos.w/2.0f + 0.5f;

	float2 ProjectedTexCoordsRefraction;
    ProjectedTexCoordsRefraction.x = input.RefractionMapSamplingPos.x/input.RefractionMapSamplingPos.w/2.0f + 0.5f;
    ProjectedTexCoordsRefraction.y = -input.RefractionMapSamplingPos.y/input.RefractionMapSamplingPos.w/2.0f + 0.5f;
    
    PixelShaderOutput output;   
    input.Normal = normalize(input.Normal);
    output.Normal.rgb = 0.5f * (input.Normal + 1.0f);               
    output.Normal.a = specularPower;                                
    output.Depth = 1-input.Depth.x / input.Depth.y;                   
	output.LightOcclusion =  0;           
                            
    float2 normal = (tex2D(NormalSampler, input.TexCoord / xWaveLength).rg - 0.5f) * 0.2f * xWaveHeight;
    
    float4 reflectiveColor = tex2D(ReflectionSampler, ProjectedTexCoordsReflection + normal);
    float4 refractiveColor = tex2D(RefractionSampler, ProjectedTexCoordsRefraction + normal);
    
    float3 eyeVector = normalize(camPos - input.Pos3D);
    float fresnelTerm = dot(eyeVector, input.Normal);    
    
    float4 combinedColor  = lerp(reflectiveColor, refractiveColor, fresnelTerm);	
    //float4 dullColor = float4(0.3f, 0.3f, 0.5f, 1.0f);
    output.Color = lerp(combinedColor, color , colorInterpolator);
    output.Color.a = specularIntensity;  
    return output;        
}


technique ReflexiveSurface
{
    pass Pass1
    {
        VertexShader = compile vs_4_0 VertexShaderFunction();
        PixelShader = compile ps_4_0 PixelShaderFunctionReflexiveSurface();
    }
}
