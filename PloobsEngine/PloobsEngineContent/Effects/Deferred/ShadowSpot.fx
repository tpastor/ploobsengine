float BIAS;
float shadowBufferSize;
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
	float2 TexCoord1 : TEXCOORD1;    
};



VertexShaderOutput VertexShaderFunction(VertexShaderInput input)  
{  
    VertexShaderOutput output;   
	input.Position.x =  input.Position.x - 2*halfPixel.x;
	input.Position.y =  input.Position.y + 2*halfPixel.y;
    output.Position = float4(input.Position,1);
    output.TexCoord = input.TexCoord ;
	output.TexCoord1 = input.TexCoord  - halfPixel;
    return output;
}   
 
 float inShadowCondition(float lightScreenPos, float2 lightSamplePos,float2 offset)
{	
	double distanceStoredInDepthMap = 1 - tex2D(ShadowMapSampler, lightSamplePos + offset * 1/(shadowBufferSize * 2 )).r;		
	bool shadowCondition = distanceStoredInDepthMap <= lightScreenPos - BIAS;
	return shadowCondition ? 0.0f : 1.0f;
}

// Calculates the shadow term using PCF soft-shadowing
float CalcShadowTermSoftPCF(float fLightDepth, float2 vShadowTexCoord, int iSqrtSamples)
{
	float fShadowTerm = 0.0f;  
		
	float fRadius = (iSqrtSamples - 1.0f) / 2;
	float fWeightAccum = 0.0f;
	
	for (float y = -fRadius; y <= fRadius; y++)
	{
		for (float x = -fRadius; x <= fRadius; x++)
		{
			float2 vOffset = 0;
			vOffset = float2(x, y);				
			vOffset /= shadowBufferSize;
			float2 vSamplePoint = vShadowTexCoord + vOffset;			
			float fDepth = 1-tex2D(ShadowMapSampler, vSamplePoint).x;			
			float fSample = (fLightDepth <= fDepth + BIAS);
			
			// Edge tap smoothing
			float xWeight = 1;
			float yWeight = 1;
			
			if (x == -fRadius)
				xWeight = 1 - frac(vShadowTexCoord.x * shadowBufferSize);
			else if (x == fRadius)
				xWeight = frac(vShadowTexCoord.x * shadowBufferSize);
				
			if (y == -fRadius)
				yWeight = 1 - frac(vShadowTexCoord.y * shadowBufferSize);
			else if (y == fRadius)
				yWeight = frac(vShadowTexCoord.y * shadowBufferSize);
				
			fShadowTerm += fSample * xWeight * yWeight;
			fWeightAccum = xWeight * yWeight;
		}											
	}		
	
	fShadowTerm /= (iSqrtSamples * iSqrtSamples);
	fShadowTerm *= 1.55f;	
	
	return fShadowTerm;
}
 
float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0  
{  
	//read depth
    float depthVal = 1-tex2D(depthSampler,input.TexCoord).r;

    //compute screen-space position
    float4 position;
    position.x = input.TexCoord1.x * 2.0f - 1.0f;
    position.y = -(input.TexCoord1.y * 2.0f - 1.0f);
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

	lightSamplePos += (0.5f / shadowBufferSize);
	
	//determine shadowing criteria
	double realDistanceToLight = lightScreenPos.z;	
	double distanceStoredInDepthMap = 1-tex2D(ShadowMapSampler, lightSamplePos).r;	
	
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
    float depthVal = 1-tex2D(depthSampler,input.TexCoord).r;

    //compute screen-space position
    float4 position;
    position.x = input.TexCoord1.x * 2.0f - 1.0f;
    position.y = -(input.TexCoord1.y * 2.0f - 1.0f);
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
	    
    // Offset the coordinate by half a texel so we sample it correctly
    lightSamplePos += (0.5f / shadowBufferSize);
	
	//determine shadowing criteria
	//double realDistanceToLight = lightScreenPos.z;	
	//double distanceStoredInDepthMap = tex2D(ShadowMapSampler, lightSamplePos).r;		
	//bool shadowCondition =  distanceStoredInDepthMap <= realDistanceToLight - BIAS;	
	
    float SdL = dot(lightDirection, -lightVector);   	
 
    if(SdL > lightAngleCosine)  
    {  		

		float shadowOcclusion  = 0;
		if(shadown)
		{
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
		}
		else
		{
		shadowOcclusion = 1;
		}
    
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
/////////////////////////////////////////////////////////

float4 PixelShaderFunctionPCFAttenuation(VertexShaderOutput input) : COLOR0  
{  
	//read depth
    float depthVal = 1-tex2D(depthSampler,input.TexCoord).r;

    //compute screen-space position
    float4 position;
    position.x = input.TexCoord1.x * 2.0f - 1.0f;
    position.y = -(input.TexCoord1.y * 2.0f - 1.0f);
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
	    
    // Offset the coordinate by half a texel so we sample it correctly
    lightSamplePos += (0.5f / shadowBufferSize);
	
	//determine shadowing criteria
	//double realDistanceToLight = lightScreenPos.z;	
	//double distanceStoredInDepthMap = tex2D(ShadowMapSampler, lightSamplePos).r;		
	//bool shadowCondition =  distanceStoredInDepthMap <= realDistanceToLight - BIAS;	
	
    float SdL = dot(lightDirection, -lightVector);   	
 
    if(SdL > lightAngleCosine)  
    {  		

		float shadowOcclusion  = 0;
		if(shadown)
		{
			shadowOcclusion = CalcShadowTermSoftPCF(lightScreenPos.z, lightSamplePos, 7);
		}
		else
		{
			shadowOcclusion = 1;
		}
    
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


/////////////////////////////////////////////////////////


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

technique Technique3
{
    pass Pass1
    {
        VertexShader = compile vs_3_0 VertexShaderFunction();
        PixelShader = compile ps_3_0 PixelShaderFunctionPCFAttenuation();
    }
}

