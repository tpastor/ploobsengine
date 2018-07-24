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

sampler colorSampler : register(s0);
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
	float2 TexCoord1 : TEXCOORD1;    
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)  
{  
    VertexShaderOutput output;
	input.Position.x =  input.Position.x - 2*halfPixel.x;
	input.Position.y =  input.Position.y + 2*halfPixel.y;
    output.Position = float4(input.Position,1);

    //align texture coordinates
    output.TexCoord = input.TexCoord ;
	output.TexCoord1 = input.TexCoord  - halfPixel;
    return output;
}  
 
 
float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0  
{  

    //read depth
    float depthVal = 1 - tex2D(depthSampler,input.TexCoord).r;

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



technique Technique1
{
    pass Pass1
    {
        VertexShader = compile vs_4_0 VertexShaderFunction();
        PixelShader = compile ps_4_0 PixelShaderFunction();
    }
}
