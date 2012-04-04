float4x4 World;
float4x4 View;
float4x4 Projection;
float2 halfPixel;
float3 cameraPosition;

float3 lightPosition;
float lightRadius;
float3 lightDirection;
float4 lightDecayExponent;
float3 Color;
float lightAngleCosine;
float lightIntensity;

float3 FrustumCorners[4];

sampler normalSampler : register(s1);
sampler depthSampler : register(s2);

struct VertexShaderInput
{
    float3 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;    
	float3 PosView  : TEXCOORD1;    	
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)  
{  
    VertexShaderOutput output;
	input.Position.x =  input.Position.x - 2*halfPixel.x;
	input.Position.y =  input.Position.y + 2*halfPixel.y;
    output.Position = float4(input.Position,1);

	float index = input.TexCoord.x + (input.TexCoord.y * 2);
	output.PosView = FrustumCorners[index];

    //align texture coordinates
    output.TexCoord = input.TexCoord ;	
    return output;
}  
 
 
float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0  
{  
     //read depth
    float depthVal = tex2D(depthSampler,input.TexCoord).r;
	    
    float3 position = input.PosView * depthVal;   		
 
    //surface-to-light vector  
    float3 lightVector = lightPosition - position;  
 
    //compute attenuation based on distance - linear attenuation  
    float attenuation = saturate(1.0f - length(lightVector)/lightRadius);   
 
    //normalize light vector  
    lightVector = normalize(lightVector);       
 
    float SdL = dot(lightDirection, -lightVector);  
 
    if(SdL > lightAngleCosine)  
    {  
        float spotIntensity = pow(SdL, lightDecayExponent);  
 
        //get normal data from the normalMap  
        float4 normalData = tex2D(normalSampler,input.TexCoord);  
        //tranform normal back into [-1,1] range  
        float3 normal = 2.0f * normalData.xyz - 1.0f;  
        //get specular power  
        float specularPower = normalData.a * 255;          
 
        //compute diffuse light  
        float NdL = max(0,dot(normal,lightVector));  
        float3 diffuseLight = NdL * Color.rgb;  
 
        //reflection vector  
        float3 reflectionVector = normalize(reflect(-lightVector, normal));  
        //camera-to-surface vector  
        float3 directionToCamera = normalize(cameraPosition - position);  
        //compute specular light  
        float specularLight = saturate(dot(reflectionVector, directionToCamera));  
 
        attenuation *= spotIntensity;  
 
        //take into account attenuation and lightIntensity.  
        return attenuation * lightIntensity * float4(diffuseLight.rgb,specularLight);          
    
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
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
