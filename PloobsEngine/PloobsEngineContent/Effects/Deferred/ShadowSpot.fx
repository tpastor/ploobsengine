float BIAS;
float shadowBufferSize;
Texture xShadowMap;
sampler ShadowMapSampler = sampler_state
 {
   texture = <xShadowMap> ;
    MinFilter = POINT;
    MagFilter = POINT;
    MipFilter = NONE;
    AddressU = Clamp;
    AddressV = Clamp;
 }; 

float4x4 xLightViewProjection;
float4x4 World;
float4x4 View;
float4x4 Projection;
float4x4 InvertViewProjection;
float2 halfPixel;
float3 cameraPosition;

float3 lightPosition;
float lightRadius;
float3 lightDirection;
float4 lightDecayExponent;
float3 Color;
float lightAngleCosine;
float lightIntensity;
bool shadown;

// diffuse color, and specularIntensity in the alpha channel
texture colorMap; 
// normals, and specularPower in the alpha channel
texture normalMap;
//depth
texture depthMap;

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
    MagFilter = LINEAR;
    MinFilter = LINEAR;
    Mipfilter = LINEAR;
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
 
 float inShadowCondition(float lightScreenPos, float2 lightSamplePos,float2 offset)
{	
	double distanceStoredInDepthMap = tex2D(ShadowMapSampler, lightSamplePos + offset * 1/(shadowBufferSize / 2)).r;		
	bool shadowCondition = distanceStoredInDepthMap <= lightScreenPos - BIAS;
	return shadowCondition ? 0.0f : 1.0f;
}
 
float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0  
{  
	//read depth
    float depthVal = tex2D(depthSampler,input.TexCoord).r;

    //compute screen-space position
    float4 position;
    position.x = input.TexCoord.x * 2.0f - 1.0f;
    position.y = -(input.TexCoord.y * 2.0f - 1.0f);
    position.z = depthVal;
    position.w = 1.0f;
    //transform to world space
    position = mul(position, InvertViewProjection);
    position /= position.w;
    
 
    //surface-to-light vector  
    float3 lightVector = lightPosition - position;  
 
    //compute attenuation based on distance - linear attenuation  
    float attenuation = saturate(1.0f - length(lightVector)/lightRadius);   
 
    //normalize light vector  
    lightVector = normalize(lightVector);       
    
    //find screen position as seen by the light
	float4 lightScreenPos = mul(position, xLightViewProjection);
	lightScreenPos /= lightScreenPos.w;
	
	//find sample position in shadow map
	float2 lightSamplePos;
	lightSamplePos.x = lightScreenPos.x/2.0f+0.5f;
	lightSamplePos.y = (-lightScreenPos.y/2.0f+0.5f);
	
	//determine shadowing criteria
	double realDistanceToLight = lightScreenPos.z;	
	double distanceStoredInDepthMap = tex2D(ShadowMapSampler, lightSamplePos).r;	
	
	bool shadowCondition =  distanceStoredInDepthMap <= realDistanceToLight - BIAS;
	//bool shadowCondition =  distanceStoredInDepthMap <= realDistanceToLight - 1.0f/10000.0f;				
	//bool shadowCondition =  distanceStoredInDepthMap <= realDistanceToLight - 1.0f/1000.0f;		
	//bool shadowCondition =  distanceStoredInDepthMap <= realDistanceToLight + 1.0f/100.0f;		
 
    float SdL = dot(lightDirection, -lightVector);   

 
    bool s = shadowCondition && shadown;
 
    if(SdL > lightAngleCosine && !s)  
    {  
        float spotIntensity = pow(SdL, lightDecayExponent);  
 
        //get normal data from the normalMap  
        float4 normalData = tex2D(normalSampler,input.TexCoord);  
        //tranform normal back into [-1,1] range  
        float3 normal = 2.0f * normalData.xyz - 1.0f;  
        //get specular power  
        float specularPower = normalData.a * 255;  
        //get specular intensity from the colorMap  
        float specularIntensity = tex2D(colorSampler, input.TexCoord).a;  
 
        //compute diffuse light  
        float NdL = max(0,dot(normal,lightVector));  
        float3 diffuseLight = NdL * Color.rgb;  
 
        //reflection vector  
        float3 reflectionVector = normalize(reflect(-lightVector, normal));  
        //camera-to-surface vector  
        float3 directionToCamera = normalize(cameraPosition - position);  
        //compute specular light  
        float specularLight = specularIntensity * pow( saturate(dot(reflectionVector, directionToCamera)), specularPower);  
 
        attenuation *= spotIntensity;  
 
        //take into account attenuation and lightIntensity.  
        return attenuation * lightIntensity * float4(diffuseLight.rgb,specularLight);          
    
    }     
    else
    {    
	return float4(0,0,0,0);
	}
	
    
} 

float4 PixelShaderFunctionPCF3x3(VertexShaderOutput input) : COLOR0  
{  
	//read depth
    float depthVal = tex2D(depthSampler,input.TexCoord).r;

    //compute screen-space position
    float4 position;
    position.x = input.TexCoord.x * 2.0f - 1.0f;
    position.y = -(input.TexCoord.y * 2.0f - 1.0f);
    position.z = depthVal;
    position.w = 1.0f;
    //transform to world space
    position = mul(position, InvertViewProjection);
    position /= position.w;
    
 
    //surface-to-light vector  
    float3 lightVector = lightPosition - position;  
 
    //compute attenuation based on distance - linear attenuation  
    float attenuation = saturate(1.0f - length(lightVector)/lightRadius);   
 
    //normalize light vector  
    lightVector = normalize(lightVector);       
    
    //find screen position as seen by the light
	float4 lightScreenPos = mul(position, xLightViewProjection);
	lightScreenPos /= lightScreenPos.w;
	
	//find sample position in shadow map
	float2 lightSamplePos;
	lightSamplePos.x = lightScreenPos.x/2.0f+0.5f;
	lightSamplePos.y = (-lightScreenPos.y/2.0f+0.5f);
	
	//determine shadowing criteria
	//double realDistanceToLight = lightScreenPos.z;	
	//double distanceStoredInDepthMap = tex2D(ShadowMapSampler, lightSamplePos).r;		
	//bool shadowCondition =  distanceStoredInDepthMap <= realDistanceToLight - BIAS;	
	
    float SdL = dot(lightDirection, -lightVector);   
 
    if(SdL > lightAngleCosine && shadown )  
    {  
		
		float shadowOcclusion  = 0;
		shadowOcclusion +=  inShadowCondition(lightScreenPos.z,lightSamplePos,float2(0.0f, 0.0f));	
		shadowOcclusion +=  inShadowCondition(lightScreenPos.z,lightSamplePos,float2(1.0f, 0.0f));	
		shadowOcclusion +=  inShadowCondition(lightScreenPos.z,lightSamplePos,float2(2.0f, 0.0f));	
		
		shadowOcclusion +=  inShadowCondition(lightScreenPos.z,lightSamplePos,float2(0.0f, 1.0f));		
		shadowOcclusion +=  inShadowCondition(lightScreenPos.z,lightSamplePos,float2(1.0f, 1.0f));	
		shadowOcclusion +=  inShadowCondition(lightScreenPos.z,lightSamplePos,float2(2.0f, 1.0f));	
		
		shadowOcclusion +=  inShadowCondition(lightScreenPos.z,lightSamplePos,float2(0.0f, 2.0f));	
		shadowOcclusion +=  inShadowCondition(lightScreenPos.z,lightSamplePos,float2(1.0f, 2.0f));	
		shadowOcclusion +=  inShadowCondition(lightScreenPos.z,lightSamplePos,float2(2.0f, 2.0f));		 
		
		shadowOcclusion /= 9.0f;

		
    
        float spotIntensity = pow(SdL, lightDecayExponent);  
 
        //get normal data from the normalMap  
        float4 normalData = tex2D(normalSampler,input.TexCoord);  
        //tranform normal back into [-1,1] range  
        float3 normal = 2.0f * normalData.xyz - 1.0f;  
        //get specular power  
        float specularPower = normalData.a * 255;  
        //get specular intensity from the colorMap  
        float specularIntensity = tex2D(colorSampler, input.TexCoord).a;  
 
        //compute diffuse light  
        float NdL = max(0,dot(normal,lightVector));  
        float3 diffuseLight = NdL * Color.rgb;  
 
        //reflection vector  
        float3 reflectionVector = normalize(reflect(-lightVector, normal));  
        //camera-to-surface vector  
        float3 directionToCamera = normalize(cameraPosition - position);  
        //compute specular light  
        float specularLight = specularIntensity * pow( saturate(dot(reflectionVector, directionToCamera)), specularPower);  
 
        attenuation *= spotIntensity;  
 
        //take into account attenuation and lightIntensity.  
        return shadowOcclusion * attenuation * lightIntensity * float4(diffuseLight.rgb,specularLight);          
    
    }     
    else
    {    
	return float4(0,0,0,0);
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


technique Technique2
{
    pass Pass1
    {
        VertexShader = compile vs_3_0 VertexShaderFunction();
        PixelShader = compile ps_3_0 PixelShaderFunctionPCF3x3();
    }
}


