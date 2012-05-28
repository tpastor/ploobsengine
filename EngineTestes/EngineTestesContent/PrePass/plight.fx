float3 cameraPos;
float4 LightColor;
float3 LightPosition;
float lightRadius;
float lightIntensity;
float3 FrustumCorners[4];

float2 GBufferPixelSize;
float4x4 WorldViewProjection;

texture DepthBuffer;
sampler2D depthSampler = sampler_state
{
	Texture = <DepthBuffer>;
	MipFilter = NONE;
	MagFilter = POINT;
	MinFilter = POINT;
	AddressU = Clamp;
	AddressV = Clamp;
};

texture NormalBuffer;
sampler2D normalSampler = sampler_state
{
	Texture = <NormalBuffer>;
	MipFilter = NONE;
	MagFilter = LINEAR;
	MinFilter = LINEAR;
	AddressU = Clamp;
	AddressV = Clamp;
};



struct PixelShaderOutput
{
    float4 Diffuse : COLOR0;
    float4 Specular : COLOR1;
};


struct VertexShaderInput
{
    float4 Position : POSITION0;
	float2 TexCoord : TEXCOORD0;
};

struct VertexShaderOutputMeshBased
{
    float4 Position : POSITION0;
	float4 TexCoordScreenSpace : TEXCOORD0;	
};

float2 PostProjectionSpaceToScreenSpace(float4 pos)
{
	float2 screenPos = pos.xy / pos.w;
	return (0.5f * (float2(screenPos.x, -screenPos.y) + 1));
}


VertexShaderOutputMeshBased PointLightMeshVS(VertexShaderInput input)
{
    VertexShaderOutputMeshBased output = (VertexShaderOutputMeshBased)0;	
    output.Position = mul(input.Position, WorldViewProjection);

	//we will compute our texture coords based on pixel position further
	output.TexCoordScreenSpace = output.Position;

	return output;
}


float4 PointLightMeshPS(VertexShaderOutputMeshBased input) : COLOR0
{	
	//as we are using a sphere mesh, we need to recompute each pixel position into texture space coords
	float2 screenPos = PostProjectionSpaceToScreenSpace(input.TexCoordScreenSpace) + GBufferPixelSize;
	//read the depth value
	float depthValue = tex2D(depthSampler, screenPos).r;	

	float3 FrustumRay = FrustumCorners[screenPos.x + (screenPos.y * 2)];	

	//if depth value == 1, we can assume its a background value, so skip it
	clip(-depthValue + 0.9999f);
	
    // Reconstruct position from the depth value, the FOV, aspect and pixel position
	float3 pos  = FrustumRay * depthValue;
	
	// Convert normal back with the decoding function
	float3 normalMap = normalize(tex2D(normalSampler, screenPos));
	float3 normal = 2.0f * normalMap  - 1.0f;	
			
	//surface-to-light vector
    float3 lightVector = LightPosition - pos ;	
	
	//float norm = saturate(length(lightVector)/(lightRadius ));	
	//float attenuation=  pow(1.0f -  norm , 2)  ;    	
	float attenuation = saturate(1.0f - (length(lightVector) )/(lightRadius )); 		
	
    //normalize light vector
    lightVector = normalize(lightVector); 

    //compute diffuse light
    float NdL = max(0,dot(normal,lightVector));
    float3 diffuseLight = NdL * float3(1,0,0);
	
	//reflection vector
    float3 reflectionVector = normalize(reflect(-lightVector, normal));
    //camera-to-surface vector
    float3 directionToCamera = normalize(cameraPos - pos);
    //compute specular light
    float specularLight = 1 * pow( saturate(dot(reflectionVector, directionToCamera)), 50);
	
    //take into account attenuation and lightIntensity.
	//return float4(diffuseLight.rgb,specularLight) * attenuation * lightIntensity;

	return float4(diffuseLight.rgb,1) * attenuation ;
}

technique PointMeshTechnique
{
    pass PointLight
    {
        VertexShader = compile vs_3_0 PointLightMeshVS();
        PixelShader = compile ps_3_0 PointLightMeshPS();
    }
}
