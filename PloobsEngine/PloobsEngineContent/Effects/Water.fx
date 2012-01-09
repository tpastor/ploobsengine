float4x4 World;
float4x4 View;
float4x4 Projection;
float4x4 ReflectionView;
float waveLength;
float waveHeight;
float3 CamPos;
float4 dullColor;
float dullBlendFactor;
float Time;
float4x4 windDirection;

Texture ReflectionMap;
sampler ReflectionSampler = sampler_state 
{ 
	texture = <ReflectionMap> ; 
	magfilter = LINEAR; 
	minfilter = LINEAR; 
	mipfilter=LINEAR; 
	AddressU = mirror; 
	AddressV = mirror;
};

Texture RefractionMap;
sampler RefractionSampler = sampler_state 
{ 
	texture = <RefractionMap> ; 
	magfilter = LINEAR; 
	minfilter = LINEAR; 
	mipfilter = LINEAR; 
	AddressU = mirror; 
	AddressV = mirror;
};

Texture WaterBumpMap;
sampler WaterBumpMapSampler = sampler_state 
{ 
	texture = <WaterBumpMap> ; 
	magfilter = LINEAR; 
	minfilter = LINEAR; 
	mipfilter = LINEAR; 
	AddressU = mirror; 
	AddressV = mirror;
};

struct VertexShaderInput
{
    float4 Position		: POSITION0;
    float2 TexCoord		: TEXCOORD0;
};

struct VertexShaderOutput
{
	float4 Position						: POSITION0;
	float4 ReflectionMapSamplingPos		: TEXCOORD1;
	float2 BumpMapSamplingPos			: TEXCOORD2;
	float4 RefractionMapSamplingPos		: TEXCOORD3;
    float4 Position3D					: TEXCOORD4;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);
    
    float4x4 preReflectionViewProjection = mul (ReflectionView, Projection);
    float4x4 preWorldReflectionViewProjection = mul (World, preReflectionViewProjection);
	output.ReflectionMapSamplingPos = mul(input.Position, preWorldReflectionViewProjection);
	output.RefractionMapSamplingPos = output.Position;
    output.Position3D = worldPosition;
    
	float4 absoluteTexCoords = float4(input.TexCoord, 0, 1);
	float4 rotatedTexCoords = mul(absoluteTexCoords, windDirection);
	float2 moveVector = float2(0, 1);
	output.BumpMapSamplingPos = rotatedTexCoords.xy / waveLength + Time * moveVector.xy;
	
    return output;
}

float4 WaterPS(VertexShaderOutput input) : COLOR0
{
	float2 ProjectedTexCoords;
    ProjectedTexCoords.x = input.ReflectionMapSamplingPos.x/input.ReflectionMapSamplingPos.w/2.0f + 0.5f;
    ProjectedTexCoords.y = -input.ReflectionMapSamplingPos.y/input.ReflectionMapSamplingPos.w/2.0f + 0.5f;
    
    float4 bumpColor = tex2D(WaterBumpMapSampler, input.BumpMapSamplingPos);
    float2 perturbation = waveHeight * (bumpColor.rg - 0.5f);
    float2 perturbatedTexCoords = ProjectedTexCoords + perturbation;
    
    float4 reflectiveColor = tex2D(ReflectionSampler, perturbatedTexCoords);
 
	float2 ProjectedRefrTexCoords;
	ProjectedRefrTexCoords.x = input.RefractionMapSamplingPos.x/input.RefractionMapSamplingPos.w/2.0f + 0.5f;
	ProjectedRefrTexCoords.y = -input.RefractionMapSamplingPos.y/input.RefractionMapSamplingPos.w/2.0f + 0.5f;
	float2 perturbatedRefrTexCoords = ProjectedRefrTexCoords + perturbation;
	float4 refractiveColor = tex2D(RefractionSampler, perturbatedRefrTexCoords);

	float3 eyeVector = normalize(CamPos - input.Position3D);
	float3 normalVector = float3(0,1,0);
	float fNdotV = dot(eyeVector, normalVector); // Fresnel term
	float fFresnel = 1.0 - fNdotV;
	float4 combinedColor = lerp(refractiveColor, reflectiveColor, fFresnel);
			
	float4 Color = dullBlendFactor * dullColor + (1 - dullBlendFactor) * combinedColor;
	return Color;
}


technique Water
{
    pass P0
    {
        VertexShader = compile vs_1_1 VertexShaderFunction();
        PixelShader = compile ps_2_0 WaterPS();
    }
}
