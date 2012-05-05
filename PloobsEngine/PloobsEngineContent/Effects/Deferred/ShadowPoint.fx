float4x4 World;
float4x4 View;
float4x4 Projection;
texture colorMap; 
texture normalMap;
texture depthMap;

Texture xPreviousShadingContents;
sampler PreviousSampler = sampler_state { texture = <xPreviousShadingContents> ; magfilter = LINEAR; minfilter = LINEAR; mipfilter=LINEAR; AddressU = mirror; AddressV = mirror;};//color of the light 
float3 Color; 

//position of the camera, for specular light
float3 cameraPosition; 

//this is used to compute the world-position
float4x4 InvertViewProjection; 

//this is the position of the light
float3 lightPosition;

//how far does this light reach
float lightRadius;

//control the brightness of the light
float lightIntensity = 1.0f;

sampler colorSampler = sampler_state
{
    Texture = (colorMap);
    AddressU = CLAMP;
    AddressV = CLAMP;
    MagFilter = LINEAR;
    MinFilter = LINEAR;
    Mipfilter = LINEAR;
};
sampler depthSampler = sampler_state
{
    Texture = (depthMap);
    AddressU = CLAMP;
    AddressV = CLAMP;
    MagFilter = POINT;
    MinFilter = POINT;
    Mipfilter = POINT;
};
sampler normalSampler = sampler_state
{
    Texture = (normalMap);
    AddressU = CLAMP;
    AddressV = CLAMP;
    MagFilter = POINT;
    MinFilter = POINT;
    Mipfilter = POINT;
};


struct VertexShaderInput
{
    float3 Position : POSITION0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float4 ScreenPosition : TEXCOORD0;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;            
    output.Position = float4(input.Position,1);
    output.ScreenPosition = output.Position;
    return output;
}

float2 halfPixel;
float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
   //obtain screen position
    input.ScreenPosition.xy /= input.ScreenPosition.w;

    float2 texCoord = 0.5f * (float2(input.ScreenPosition.x,-input.ScreenPosition.y) + 1);    
    texCoord -=halfPixel;

    //get normal data from the normalMap
    float4 normalData = tex2D(normalSampler,texCoord);
    //tranform normal back into [-1,1] range
    float3 normal = 2.0f * normalData.xyz - 1.0f;
    //get specular power
    float specularPower = normalData.a * 500;
    //get specular intensity from the colorMap
    float specularIntensity = tex2D(colorSampler, texCoord).a;

    //read depth
    float depthVal = 1-tex2D(depthSampler,texCoord).r;

    //compute screen-space position
    float4 position;
    position.xy = input.ScreenPosition.xy;
    position.z = depthVal;
    position.w = 1.0f;
        
    position = mul(position, InvertViewProjection);
    position /= position.w;

    //surface-to-light vector
    float3 lightVector = lightPosition - position;

    //compute attenuation based on distance - linear attenuation
    float attenuation = saturate(1.0f - length(lightVector)/lightRadius); 
    
	float4 previous = tex2D(PreviousSampler, texCoord);
    
    if( length(lightVector) < lightRadius)
    {
		//normalize light vector
		lightVector = normalize(lightVector); 

		//compute diffuse light
		float NdL = max(0,dot(normal,lightVector));
		float3 diffuseLight = NdL * Color.rgb;

		//reflection vector
		float3 reflectionVector = normalize(reflect(-lightVector, normal));
		//camera-to-surface vector
		float3 directionToCamera = normalize(cameraPosition - position);
		//compute specular light
		float specularLight = specularIntensity * pow( saturate(dot(reflectionVector, directionToCamera)), specularPower);

		//take into account attenuation and lightIntensity.
		return previous + attenuation * lightIntensity * float4(diffuseLight.rgb,specularLight);
    }
    else
    {
    		return previous ;
    }
}

technique Technique1
{
    pass Pass1
    {
        VertexShader = compile vs_3_0 VertexShaderFunction();
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}
