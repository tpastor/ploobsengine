float4x4 worldViewProjection;
float4 AmbientColor;
const static float LightBufferScaleInv = 10.0f;
float2 LightBufferPixelSize;

texture DiffuseMap;
sampler diffuseMapSampler = sampler_state
{
	Texture = (DiffuseMap);
	MAGFILTER = LINEAR;
	MINFILTER = LINEAR;
	MIPFILTER = LINEAR;
	AddressU = Wrap;
	AddressV = Wrap;
};

texture LightSpecularBuffer;
sampler2D lightSpecularSampler = sampler_state
{
	Texture = <LightSpecularBuffer>;
	MipFilter = NONE;
	MagFilter = POINT;
	MinFilter = POINT;
	AddressU = Clamp;
	AddressV = Clamp;
};


texture lightSamplerBuffer;
sampler2D lightSampler = sampler_state
{
	Texture = <lightSamplerBuffer>;
	MipFilter = NONE;
	MagFilter = POINT;
	MinFilter = POINT;
	AddressU = Clamp;
	AddressV = Clamp;
};


float2 PostProjectionSpaceToScreenSpace(float4 pos)
{
	float2 screenPos = pos.xy / pos.w;
	return (0.5f * (float2(screenPos.x, -screenPos.y) + 1));
}


struct ReconstructVertexShaderInput
{
    float4 Position  : POSITION0;
    float2 TexCoord  : TEXCOORD0;
};

struct ReconstructVertexShaderOutput
{
    float4 Position			: POSITION0;
    float2 TexCoord			: TEXCOORD0;
	float4 TexCoordScreenSpace : TEXCOORD1;
};

ReconstructVertexShaderOutput ReconstructVertexShader(ReconstructVertexShaderInput input)
{
    ReconstructVertexShaderOutput output=(ReconstructVertexShaderOutput)0;			
    output.Position = mul(input.Position, worldViewProjection);
    output.TexCoord = input.TexCoord; 
	output.TexCoordScreenSpace = output.Position;
    return output;
}


float4 ReconstructPixelShader(ReconstructVertexShaderOutput input):COLOR0
{	
	//read from our diffuse, specular and emissive maps
	float4 diffuseMap = tex2D(diffuseMapSampler, input.TexCoord);

	// Find the screen space texture coordinate and offset it
	float2 screenPos = PostProjectionSpaceToScreenSpace(input.TexCoordScreenSpace) + LightBufferPixelSize;
	
	//half3 emissiveMap = tex2D(emissiveMapSampler, input.TexCoord).rgb;
	//half3 specularMap = tex2D(specularMapSampler, input.TexCoord).rgb;
		
	float4 lightColor =  tex2D(lightSampler, screenPos) ;	
	
	//our specular intensity is stored in a separate texture
	//float4 specular =  tex2D(lightSpecularSampler, screenPos) * LightBufferScaleInv;
	
	//float4 finalColor = float4(diffuseMap*lightColor.rgb + specular*specularMap + emissiveMap,1);
	float4 finalColor = float4(lightColor.rgb , 1);
	//float4 finalColor = float4(lightColor.a,0,0,1);
	return finalColor;
}


technique ReconstructShading
{
	pass ReconstructShadingPass
    {
        VertexShader = compile vs_3_0 ReconstructVertexShader();
        PixelShader = compile ps_3_0 ReconstructPixelShader();
    }
}
