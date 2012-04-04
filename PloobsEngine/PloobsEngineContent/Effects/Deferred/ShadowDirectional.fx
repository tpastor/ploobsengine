float shadowBufferSize = 2048;
float BIAS;

float4x4 xLightViewProjection;
float4x4 World;
float4x4 View;
float4x4 Projection;
float4x4 InvertViewProjection;
float2 halfPixel;
float3 cameraPosition;

float3 lightDirection;
float3 Color;

float lightIntensity;
bool shadown;

sampler colorSampler : register(s0);
sampler normalSampler : register(s1);
sampler depthSampler : register(s2);
sampler ShadowMapSampler : register(s3);



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
 
float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0  
{  
	//get normal data from the normalMap
    float4 normalData = tex2D(normalSampler,input.TexCoord);
    //tranform normal back into [-1,1] range
    float3 normal = 2.0f * normalData.xyz - 1.0f;
    //get specular power, and get it into [0,255] range]
    float specularPower = normalData.a * 255;
    //get specular intensity from the colorMap
    float specularIntensity = tex2D(colorSampler, input.TexCoord).a;

	//read depth
    float depthVal = 1-tex2D(depthSampler,input.TexCoord).r;

    //compute screen-space position
    float4 position;
    position.x = input.TexCoord.x * 2.0f - 1.0f;
    position.y = -(input.TexCoord.y * 2.0f - 1.0f);
    position.z = depthVal;
    position.w = 1.0f;
    //transform to world space
    position = mul(position, InvertViewProjection);
    position /= position.w;
     
    
    //find screen position as seen by the light
	float4 lightScreenPos = mul(position, xLightViewProjection);
	lightScreenPos /= lightScreenPos.w;
	
	//find sample position in shadow map
	float2 lightSamplePos;
	lightSamplePos.x = lightScreenPos.x/2.0f + 0.5f;
	lightSamplePos.y = (-lightScreenPos.y/2.0f + 0.5f);		
	
	//determine shadowing criteria
	float realDistanceToLight = lightScreenPos.z;	
	float distanceStoredInDepthMap = tex2D(ShadowMapSampler, lightSamplePos).r;		
	bool shadowCondition =  distanceStoredInDepthMap <= realDistanceToLight - BIAS;
	bool s = shadowCondition && shadown;
	
    if(!s)  
    {  
        //surface-to-light vector
		float3 lightVector = -normalize(lightDirection);

		//compute diffuse light
		float NdL = max(0,dot(normal,lightVector));
		float3 diffuseLight = NdL * Color.rgb;

		//reflexion vector
		float3 reflectionVector = normalize(reflect(-lightVector, normal));
		//camera-to-surface vector
		float3 directionToCamera = normalize(cameraPosition - position);
		//compute specular light
		float specularLight = specularIntensity * pow( saturate(dot(reflectionVector, directionToCamera)), specularPower);

		//output the two lights
		return float4(diffuseLight.rgb * lightIntensity , specularLight) ;
    
    }     
    else
    {    
	return float4(0,0,0,0);
	}
} 


float PS_ShadowMapLookup(float2 lightSamplePos, float2 offset, float realDistanceToLight)
{	
	double distanceStoredInDepthMap = tex2D(ShadowMapSampler, lightSamplePos + offset * 1/shadowBufferSize).r;		
	bool shadowCondition =  distanceStoredInDepthMap <= realDistanceToLight - BIAS;	
	return shadowCondition ? 0.0f : 1.0f;
}


float4 PixelShaderFunctionPCF3x3(VertexShaderOutput input) : COLOR0  
{  
	
	//get normal data from the normalMap
    float4 normalData = tex2D(normalSampler,input.TexCoord);
    //tranform normal back into [-1,1] range
    float3 normal = 2.0f * normalData.xyz - 1.0f;
    //get specular power, and get it into [0,255] range]
    float specularPower = normalData.a * 255;
    //get specular intensity from the colorMap
    float specularIntensity = tex2D(colorSampler, input.TexCoord).a;

	//read depth
    float depthVal = 1-tex2D(depthSampler,input.TexCoord).r;

    //compute screen-space position
    float4 position;
    position.x = input.TexCoord.x * 2.0f - 1.0f;
    position.y = -(input.TexCoord.y * 2.0f - 1.0f);
    position.z = depthVal;
    position.w = 1.0f;
    //transform to world space
    position = mul(position, InvertViewProjection);
    position /= position.w;
     
    
    //find screen position as seen by the light
	float4 lightScreenPos = mul(position, xLightViewProjection);
	lightScreenPos /= lightScreenPos.w;
	
	//find sample position in shadow map
	float2 lightSamplePos;
	lightSamplePos.x = lightScreenPos.x/2.0f + 0.5f;
	lightSamplePos.y = (-lightScreenPos.y/2.0f + 0.5f);			
	
	float shadowOcclusion = 0.0f;	
	float depth = lightScreenPos.z;
	
    shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(0.0f, 0.0f), depth);
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(1.0f, 0.0f), depth);
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(2.0f, 0.0f), depth);
	
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(0.0f, 1.0f), depth);
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(1.0f, 1.0f), depth);
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(2.0f, 1.0f), depth);
	
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(0.0f, 2.0f), depth);
    shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(1.0f, 2.0f), depth);
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(2.0f, 2.0f), depth);
	
	shadowOcclusion /= 9.0f;
		
		
	
        //surface-to-light vector
		float3 lightVector = -normalize(lightDirection);

		//compute diffuse light
		float NdL = max(0,dot(normal,lightVector));
		float3 diffuseLight = NdL * Color.rgb * shadowOcclusion ;

		//reflexion vector
		float3 reflectionVector = normalize(reflect(-lightVector, normal));
		//camera-to-surface vector
		float3 directionToCamera = normalize(cameraPosition - position);
		//compute specular light
		float specularLight = shadowOcclusion * specularIntensity * pow( saturate(dot(reflectionVector, directionToCamera)), specularPower);

		//output the two lights
		return float4(diffuseLight.rgb * lightIntensity , specularLight) ;    
} 

float4 PixelShaderFunctionPCF2x2(VertexShaderOutput input) : COLOR0  
{  
	
	//get normal data from the normalMap
    float4 normalData = tex2D(normalSampler,input.TexCoord);
    //tranform normal back into [-1,1] range
    float3 normal = 2.0f * normalData.xyz - 1.0f;
    //get specular power, and get it into [0,255] range]
    float specularPower = normalData.a * 255;
    //get specular intensity from the colorMap
    float specularIntensity = tex2D(colorSampler, input.TexCoord).a;

	//read depth
    float depthVal = 1-tex2D(depthSampler,input.TexCoord).r;

    //compute screen-space position
    float4 position;
    position.x = input.TexCoord.x * 2.0f - 1.0f;
    position.y = -(input.TexCoord.y * 2.0f - 1.0f);
    position.z = depthVal;
    position.w = 1.0f;
    //transform to world space
    position = mul(position, InvertViewProjection);
    position /= position.w;
     
    
    //find screen position as seen by the light
	float4 lightScreenPos = mul(position, xLightViewProjection);
	lightScreenPos /= lightScreenPos.w;
	
	//find sample position in shadow map
	float2 lightSamplePos;
	lightSamplePos.x = lightScreenPos.x/2.0f + 0.5f;
	lightSamplePos.y = (-lightScreenPos.y/2.0f + 0.5f);			
	
	float shadowOcclusion = 0.0f;	
	float depth = lightScreenPos.z;	
    
    shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(0.0f, 0.0f), depth);
    shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(1.0f, 0.0f), depth);
    
    shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(0.0f, 1.0f), depth);
    shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(1.0f, 1.0f), depth);   
    
    shadowOcclusion /= 4.0f;
		
		
	
        //surface-to-light vector
		float3 lightVector = -normalize(lightDirection);

		//compute diffuse light
		float NdL = max(0,dot(normal,lightVector));
		float3 diffuseLight = NdL * Color.rgb * shadowOcclusion ;

		//reflexion vector
		float3 reflectionVector = normalize(reflect(-lightVector, normal));
		//camera-to-surface vector
		float3 directionToCamera = normalize(cameraPosition - position);
		//compute specular light
		float specularLight = shadowOcclusion * specularIntensity * pow( saturate(dot(reflectionVector, directionToCamera)), specularPower);

		//output the two lights
		return float4(diffuseLight.rgb * lightIntensity , specularLight) ;    
} 

float4 PixelShaderFunctionPCF2x2Variance(VertexShaderOutput input) : COLOR0  
{  
	
	//get normal data from the normalMap
    float4 normalData = tex2D(normalSampler,input.TexCoord);
    //tranform normal back into [-1,1] range
    float3 normal = 2.0f * normalData.xyz - 1.0f;
    //get specular power, and get it into [0,255] range]
    float specularPower = normalData.a * 255;
    //get specular intensity from the colorMap
    float specularIntensity = tex2D(colorSampler, input.TexCoord).a;

	//read depth
    float depthVal = 1-tex2D(depthSampler,input.TexCoord).r;

    //compute screen-space position
    float4 position;
    position.x = input.TexCoord.x * 2.0f - 1.0f;
    position.y = -(input.TexCoord.y * 2.0f - 1.0f);
    position.z = depthVal;
    position.w = 1.0f;
    //transform to world space
    position = mul(position, InvertViewProjection);
    position /= position.w;
     
    
    //find screen position as seen by the light
	float4 lightScreenPos = mul(position, xLightViewProjection);
	lightScreenPos /= lightScreenPos.w;
	
	//find sample position in shadow map
	float2 lightSamplePos;
	lightSamplePos.x = lightScreenPos.x/2.0f + 0.5f;
	lightSamplePos.y = (-lightScreenPos.y/2.0f + 0.5f);			
	
	float shadowOcclusion = 0.0f;	
	float depth = lightScreenPos.z;	
    
    
		 // transform to texel space
		float2 vShadowMapCoord = shadowBufferSize * lightSamplePos;
	    
		// Determine the lerp amounts           
		float2 vLerps = frac(vShadowMapCoord);

		float s1,s2,s3,s4;    
	    
		s1 = PS_ShadowMapLookup(lightSamplePos, float2(0.0f, 0.0f), depth);
		s2 = PS_ShadowMapLookup(lightSamplePos, float2(1.0f, 0.0f), depth);
	    
		s3 = PS_ShadowMapLookup(lightSamplePos, float2(0.0f, 1.0f), depth);
		s4 = PS_ShadowMapLookup(lightSamplePos, float2(1.0f, 1.0f), depth);   
    
		
    
		shadowOcclusion = lerp( lerp( s1, s2, vLerps.x ),
                              lerp( s3, s4, vLerps.x ),
                              vLerps.y ); 
		
		
	
        //surface-to-light vector
		float3 lightVector = -normalize(lightDirection);

		//compute diffuse light
		float NdL = max(0,dot(normal,lightVector));
		float3 diffuseLight = NdL * Color.rgb * shadowOcclusion ;

		//reflexion vector
		float3 reflectionVector = normalize(reflect(-lightVector, normal));
		//camera-to-surface vector
		float3 directionToCamera = normalize(cameraPosition - position);
		//compute specular light
		float specularLight = shadowOcclusion * specularIntensity * pow( saturate(dot(reflectionVector, directionToCamera)), specularPower);

		//output the two lights
		return float4(diffuseLight.rgb * lightIntensity , specularLight) ;    
} 



float4 PixelShaderFunctionPCF3x3DEFERRED(VertexShaderOutput input) : COLOR0  
{  	
	//read depth
    float depthVal = 1-tex2D(depthSampler,input.TexCoord).r;

    //compute screen-space position
    float4 position;
    position.x = input.TexCoord.x * 2.0f - 1.0f;
    position.y = -(input.TexCoord.y * 2.0f - 1.0f);
    position.z = depthVal;
    position.w = 1.0f;
    //transform to world space
    position = mul(position, InvertViewProjection);
    position /= position.w;
     
    
    //find screen position as seen by the light
	float4 lightScreenPos = mul(position, xLightViewProjection);
	lightScreenPos /= lightScreenPos.w;
	
	//find sample position in shadow map
	float2 lightSamplePos;
	lightSamplePos.x = lightScreenPos.x/2.0f + 0.5f;
	lightSamplePos.y = (-lightScreenPos.y/2.0f + 0.5f);			
	
	float shadowOcclusion = 0.0f;	
	float depth = lightScreenPos.z;
	
    shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(0.0f, 0.0f), depth);
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(1.0f, 0.0f), depth);
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(2.0f, 0.0f), depth);
	
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(0.0f, 1.0f), depth);
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(1.0f, 1.0f), depth);
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(2.0f, 1.0f), depth);
	
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(0.0f, 2.0f), depth);
    shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(1.0f, 2.0f), depth);
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(2.0f, 2.0f), depth);
	
	shadowOcclusion /= 9.0f;
	return shadowOcclusion ;    
} 


float4 PixelShaderFunctionPCF3x3DEFERREDAFTER(VertexShaderOutput input) : COLOR0  
{  
	
	//get normal data from the normalMap
    float4 normalData = tex2D(normalSampler,input.TexCoord);
    //tranform normal back into [-1,1] range
    float3 normal = 2.0f * normalData.xyz - 1.0f;
    //get specular power, and get it into [0,255] range]
    float specularPower = normalData.a * 255;
    //get specular intensity from the colorMap
    float specularIntensity = tex2D(colorSampler, input.TexCoord).a;

	//read depth
    float depthVal = 1-tex2D(depthSampler,input.TexCoord).r;

    //compute screen-space position
    float4 position;
    position.x = input.TexCoord.x * 2.0f - 1.0f;
    position.y = -(input.TexCoord.y * 2.0f - 1.0f);
    position.z = depthVal;
    position.w = 1.0f;
    //transform to world space
    position = mul(position, InvertViewProjection);
    position /= position.w;
     
    
		//find screen position as seen by the light
		float4 lightScreenPos = mul(position, xLightViewProjection);
		lightScreenPos /= lightScreenPos.w;	
	
		float shadowOcclusion = tex2D(ShadowMapSampler,input.TexCoord).r;
	
		//surface-to-light vector
		float3 lightVector = -normalize(lightDirection);

		//compute diffuse light
		float NdL = max(0,dot(normal,lightVector));
		float3 diffuseLight = NdL * Color.rgb * shadowOcclusion ;

		//reflexion vector
		float3 reflectionVector = normalize(reflect(-lightVector, normal));
		//camera-to-surface vector
		float3 directionToCamera = normalize(cameraPosition - position);
		//compute specular light
		float specularLight = shadowOcclusion * specularIntensity * pow( saturate(dot(reflectionVector, directionToCamera)), specularPower);

		//output the two lights
		return float4(diffuseLight.rgb * lightIntensity , specularLight) ;    
	
	
} 

float4 PixelShaderFunctionPCF4x4DEFERRED(VertexShaderOutput input) : COLOR0  
{  	
	//read depth
    float depthVal = 1-tex2D(depthSampler,input.TexCoord).r;

    //compute screen-space position
    float4 position;
    position.x = input.TexCoord.x * 2.0f - 1.0f;
    position.y = -(input.TexCoord.y * 2.0f - 1.0f);
    position.z = depthVal;
    position.w = 1.0f;
    //transform to world space
    position = mul(position, InvertViewProjection);
    position /= position.w;
     
    
    //find screen position as seen by the light
	float4 lightScreenPos = mul(position, xLightViewProjection);
	lightScreenPos /= lightScreenPos.w;
	
	//find sample position in shadow map
	float2 lightSamplePos;
	lightSamplePos.x = lightScreenPos.x/2.0f + 0.5f;
	lightSamplePos.y = (-lightScreenPos.y/2.0f + 0.5f);			
	
	float shadowOcclusion = 0.0f;	
	float depth = lightScreenPos.z;
	
    shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(0.0f, 0.0f), depth);
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(1.0f, 0.0f), depth);
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(2.0f, 0.0f), depth);
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(3.0f, 0.0f), depth);
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(4.0f, 0.0f), depth);
	
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(0.0f, 1.0f), depth);
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(1.0f, 1.0f), depth);
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(2.0f, 1.0f), depth);
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(3.0f, 1.0f), depth);
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(4.0f, 1.0f), depth);
	
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(0.0f, 2.0f), depth);
    shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(1.0f, 2.0f), depth);
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(2.0f, 2.0f), depth);
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(3.0f, 2.0f), depth);
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(4.0f, 2.0f), depth);
	
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(0.0f, 3.0f), depth);
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(1.0f, 3.0f), depth);
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(2.0f, 3.0f), depth);
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(3.0f, 3.0f), depth);
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(4.0f, 3.0f), depth);
	
	
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(0.0f, 4.0f), depth);
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(1.0f, 4.0f), depth);
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(2.0f, 4.0f), depth);
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(3.0f, 4.0f), depth);
	shadowOcclusion += PS_ShadowMapLookup(lightSamplePos, float2(4.0f, 4.0f), depth);
	
	
	shadowOcclusion /= 25.0f;
	return shadowOcclusion ;    
} 




technique NORMAL
{
    pass Pass1
    {
        VertexShader = compile vs_3_0 VertexShaderFunction();
        //PixelShader = compile ps_3_0 PixelShaderFunctionPCF3x3();
        //PixelShader = compile ps_3_0 PixelShaderFunctionPCF2x2Variance();        
        PixelShader = compile ps_3_0 PixelShaderFunction();
        //PixelShader = compile ps_3_0 PixelShaderFunctionPCFNxN();        
        
        
    }
}
technique PCF2x2
{
    pass Pass1
    {
        VertexShader = compile vs_3_0 VertexShaderFunction();        
        PixelShader = compile ps_3_0 PixelShaderFunctionPCF2x2();        
        
        
    }
}

technique PCF2x2Variance
{
    pass Pass1
    {
        VertexShader = compile vs_3_0 VertexShaderFunction();        
        PixelShader = compile ps_3_0 PixelShaderFunctionPCF2x2Variance();                
        
    }
}

technique PCF3x3
{
    pass Pass1
    {
        VertexShader = compile vs_3_0 VertexShaderFunction();
        PixelShader = compile ps_3_0 PixelShaderFunctionPCF3x3();
    }
}

technique DEFERREDPCF3x3
{
    pass Pass1
    {
        VertexShader = compile vs_3_0 VertexShaderFunction();
        PixelShader = compile ps_3_0 PixelShaderFunctionPCF3x3DEFERRED();
    }
}

technique DEFERREDPCF4x4
{
    pass Pass1
    {
        VertexShader = compile vs_3_0 VertexShaderFunction();
        PixelShader = compile ps_3_0 PixelShaderFunctionPCF3x3DEFERRED();
    }
}


technique DEFERREDPCF3x3AFTER
{
    pass Pass1
    {
        VertexShader = compile vs_3_0 VertexShaderFunction();
        PixelShader = compile ps_3_0 PixelShaderFunctionPCF3x3DEFERREDAFTER();
    }
}


