float3 lightDirection;
float  lightIntensity = 1;
float3 Color; 
float3 cameraPosition; 
float4x4 InvertViewProjection; 

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

float2 halfPixel;
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
	//get normal data from the normalMap
    float4 normalData = tex2D(normalSampler,input.TexCoord);
    //tranform normal back into [-1,1] range
    float3 normal = 2.0f * normalData.xyz - 1.0f;
    //get specular power, and get it into [0,255] range]
    float specularPower = normalData.a * 255;
    //get specular intensity from the colorMap
    float specularIntensity = tex2D(colorSampler, input.TexCoord).a;
    
    //read depth
    float depthVal = 1- tex2D(depthSampler,input.TexCoord).r;

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

technique Technique0
{
    pass Pass0
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}