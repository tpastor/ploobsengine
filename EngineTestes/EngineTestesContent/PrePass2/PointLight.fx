#include <..\PrePass2\helper.fxh>
float4x4 wvp;
uniform  bool quadratic = true;
float2 TanAspect;
float farPlane;

//color of the light 
float3 Color; 

//position of the camera, for specular light
float3 cameraPosition; 

//this is the position of the light
float3 lightPosition;

//how far does this light reach
float lightRadius;

//control the brightness of the light
float lightIntensity = 1.0f;

sampler normalSampler : register(s1);
sampler depthSampler : register(s2);

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
    //processing geometry coordinates
    float4 worldPosition = mul(float4(input.Position,1), wvp);    
    output.Position = worldPosition;
    output.ScreenPosition = output.Position;
    return output;
}

float2 halfPixel;
float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{	
   //obtain screen position
    input.ScreenPosition.xy /= input.ScreenPosition.w;

    //obtain textureCoordinates corresponding to the current pixel
    //the screen coordinates are in [-1,1]*[1,-1]
    //the texture coordinates need to be in [0,1]*[0,1]
    float2 texCoord = 0.5f * (float2(input.ScreenPosition.x,-input.ScreenPosition.y) + 1);
    //allign texels to pixels
    texCoord +=halfPixel;

    //get normal data from the normalMap
    float3 normal = SampleNormal(texCoord,normalSampler);		
    
    //read depth
    float depthVal = tex2D(depthSampler,texCoord).r;
	depthVal*=farPlane;
    //compute screen-space position
    float3 position = float3(TanAspect*(texCoord*2 - 1)*depthVal, -depthVal);
	
    //surface-to-light vector
    float3 lightVector = lightPosition - position;

	float attenuation;
	if(quadratic)
	{    	
	float norm = saturate(length(lightVector)/(lightRadius ));	
	attenuation =  pow(1.0f - norm , 2)  ;    	
	}
	else
	{	
    attenuation = saturate(1.0f - (length(lightVector) )/(lightRadius )); 		
	}
	
	
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
    float specularLight = saturate(dot(reflectionVector, directionToCamera));

    //take into account attenuation and lightIntensity.
    return attenuation * lightIntensity * float4(diffuseLight.rgb,specularLight);
	//return float4(diffuseLight.rgb,0);
}

technique Technique1
{
    pass Pass1
    {
        VertexShader = compile vs_3_0 VertexShaderFunction();
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}
