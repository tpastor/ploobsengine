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
    //processing geometry coordinates
    //float4 worldPosition = mul(float4(input.Position,1), World);
    //float4 viewPosition = mul(worldPosition, View);
    output.Position = float4(input.Position,1);
    output.ScreenPosition = output.Position;
    return output;
}  
 
 
float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0  
{  

	//obtain screen position  
    input.ScreenPosition.xy /= input.ScreenPosition.w;  
 
    float2 texCoord = 0.5f * (float2(input.ScreenPosition.x,-input.ScreenPosition.y) + 1);      
    texCoord -=halfPixel;  
 
    //read depth  
    float depthVal = tex2D(depthSampler,texCoord).r;  
 
    //compute screen-space position  
    float4 position;  
    position.xy = input.ScreenPosition.xy;  
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
        float4 normalData = tex2D(normalSampler,texCoord);  
        //tranform normal back into [-1,1] range  
        float3 normal = 2.0f * normalData.xyz - 1.0f;  
        //get specular power  
        float specularPower = normalData.a * 255;  
        //get specular intensity from the colorMap  
        float specularIntensity = tex2D(colorSampler, texCoord).a;  
 
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
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
